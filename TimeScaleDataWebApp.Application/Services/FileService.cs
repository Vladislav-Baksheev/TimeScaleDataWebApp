using System.Globalization;
using TimeScaleDataWebApp.Application.DTO;
using TimeScaleDataWebApp.Domain.Entities;

namespace TimeScaleDataWebApp.Application.Services;

public class FileService
{
    private string[] _lines = [];
    
    public Values ParseFile(UploadFileModelRequest fileModel)
    {
        var value = new Values();
        using var reader = new StreamReader(fileModel.File.OpenReadStream());
        string content = reader.ReadToEnd();
        
        _lines = content.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        foreach (var line in _lines)
        {
            var parts = line.Split(';');

            if (parts.Length >= 3)
            {
                if (DateTime.TryParseExact(parts[0].Trim(), "yyyy-MM-ddHH-mm-ss.fff", 
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                {
                    value.Date = date.ToUniversalTime();
                }
                
                if (float.TryParse(parts[1].Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, out float executionTime))
                {
                    value.ExecutionTime = executionTime;
                }
                
                if (float.TryParse(parts[2].Trim().Replace(',', '.'), NumberStyles.Float, CultureInfo.InvariantCulture, out float val))
                {
                    value.Value = val;
                }
            }
        }

        return value;
    }

    public Values ValidateFile(UploadFileModelRequest fileModel)
    {
        var startDate = new DateTime(2000, 1, 1);
        var value = ParseFile(fileModel);

        if (value.Date < startDate || value.Date > DateTime.UtcNow)
        {
            throw new Exception("Введена некорректная дата.");
        }

        if (value.ExecutionTime < 0)
        {
            throw new Exception("Время выполнения не может быть меньше 0.");
        }

        if (value.Value < 0)
        {
            throw new Exception("Значение показателя не может быть меньше 0.");
        }

        if (_lines.Length < 1 || _lines.Length > 10000)
        {
            throw new Exception("Количество строк не может быть меньше 1 и больше 10 000.");
        }

        if (value.Date == null)
        {
            throw new Exception("В файле отсутствует время начала.");
        }
        if (value.ExecutionTime == null)
        {
            throw new Exception("В файле отсутствует время выполнения.");
        }
        if (value.Value == null)
        {
            throw new Exception("В файле отсутствует показатель.");
        }
        
        return value;
    }
}