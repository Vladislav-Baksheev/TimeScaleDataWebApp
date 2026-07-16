using System.Globalization;
using TimeScaleDataWebApp.Application.DTO;
using TimeScaleDataWebApp.Domain.Entities;
using TimeScaleDataWebApp.Infrastructure;

namespace TimeScaleDataWebApp.Application.Services;

public class FileService(ApplicationDbContext context)
{
    public async Task ProcessFileAsync(UploadFileModelRequest fileModel)
    {
        var records = ParseFile(fileModel);

        ValidateFile(records);

        var result = CalculateIntegralResults(records);

        await SaveToDatabase(records, result, fileModel.File.FileName);
    }

    private async Task SaveToDatabase(List<Values> records, Results result, string fileName)
    {
        await using var transaction = await context.Database.BeginTransactionAsync();

        try
        {
            var oldValues = context.Values
                .Where(x => x.FileName == fileName);

            context.Values.RemoveRange(oldValues);

            var oldResults = context.Results
                .Where(x => x.FileName == fileName);

            context.Results.RemoveRange(oldResults);
            
            await context.SaveChangesAsync();
            
            result.FileName = fileName;
            await context.Results.AddAsync(result);
            
            await context.Values.AddRangeAsync(records);
            

            await context.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    private void ValidateFile(List<Values> records)
    {
        if (records.Count < 1 || records.Count > 10000)
        {
            throw new Exception("Количество записей должно быть от 1 до 10000.");
        }

        var minDate = new DateTime(2000, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var now = DateTime.UtcNow;

        foreach (var record in records)
        {
            if (record.Date < minDate || record.Date > now)
            {
                throw new Exception("Дата не может быть раньше 01.01.2000 и позже текущего времени.");
            }

            if (record.ExecutionTime < 0)
            {
                throw new Exception("Время выполнения не может быть меньше 0.");
            }

            if (record.Value < 0)
            {
                throw new Exception("Значение показателя не может быть меньше 0.");
            }
        }
    }

    private Results CalculateIntegralResults(List<Values> records)
    {
        if (records == null || records.Count == 0)
        {
            throw new Exception("Нет данных для расчета интегральных результатов.");
        }

        var minDate = records.Min(x => x.Date);
        var maxDate = records.Max(x => x.Date);

        var orderedValues = records
            .Select(x => x.Value)
            .OrderBy(x => x)
            .ToList();

        double median;

        if (orderedValues.Count % 2 == 0)
        {
            median = (orderedValues[orderedValues.Count / 2 - 1] + orderedValues[orderedValues.Count / 2]) / 2.0;
        }
        else
        {
            median = orderedValues[orderedValues.Count / 2];
        }

        return new Results
        {
            DeltaTime = (maxDate - minDate).TotalSeconds,
            StartDate = minDate,
            AvgExecutionTime = records.Average(x => x.ExecutionTime),
            AvgValue = records.Average(x => x.Value),
            MedianValue = median,
            MaxValue = records.Max(x => x.Value),
            MinValue = records.Min(x => x.Value)
        };
    }

    private List<Values> ParseFile(UploadFileModelRequest fileModel)
    {
        using var reader = new StreamReader(fileModel.File.OpenReadStream());

        var lines = reader
            .ReadToEnd()
            .Split('\n', StringSplitOptions.RemoveEmptyEntries);

        if (lines.Length < 2)
        {
            throw new Exception("Файл не содержит данных.");
        }

        var records = new List<Values>();

        for (int i = 1; i < lines.Length; i++)
        {
            var line = lines[i].Trim();

            if (string.IsNullOrWhiteSpace(line))
                continue;

            var parts = line.Split(';');

            if (parts.Length != 3)
            {
                throw new Exception($"Строка {i + 1}: должно быть ровно 3 значения.");
            }

            if (string.IsNullOrWhiteSpace(parts[0]) ||
                string.IsNullOrWhiteSpace(parts[1]) ||
                string.IsNullOrWhiteSpace(parts[2]))
            {
                throw new Exception($"Строка {i + 1}: отсутствует одно из обязательных значений.");
            }

            if (!DateTime.TryParseExact(
                    parts[0].Trim(),
                    "yyyy-MM-ddTHH-mm-ss.ffffZ",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal,
                    out var date))
            {
                throw new Exception($"Строка {i + 1}: некорректный формат даты.");
            }

            if (!double.TryParse(
                    parts[1].Trim().Replace(',', '.'),
                    NumberStyles.Float,
                    CultureInfo.InvariantCulture,
                    out var executionTime))
            {
                throw new Exception($"Строка {i + 1}: некорректное значение ExecutionTime.");
            }

            if (!double.TryParse(
                    parts[2].Trim().Replace(',', '.'),
                    NumberStyles.Float,
                    CultureInfo.InvariantCulture,
                    out var value))
            {
                throw new Exception($"Строка {i + 1}: некорректное значение Value.");
            }

            records.Add(new Values
            {
                FileName = fileModel.File.FileName,
                Date = date,
                ExecutionTime = executionTime,
                Value = value
            });
        }

        return records;
    }
}