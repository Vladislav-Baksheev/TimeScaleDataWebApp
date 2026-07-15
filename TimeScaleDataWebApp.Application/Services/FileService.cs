using System.Globalization;
using TimeScaleDataWebApp.Application.DTO;
using TimeScaleDataWebApp.Domain.Entities;

namespace TimeScaleDataWebApp.Application.Services;

public class FileService
{
    public Values ParseFile(UploadFileModelRequest fileModel)
    {
        var value = new Values();
        using var reader = new StreamReader(fileModel.File.OpenReadStream());
        string content = reader.ReadToEnd();
        
        var lines = content.Split('\n', StringSplitOptions.RemoveEmptyEntries);
        foreach (var line in lines)
        {
            var parts = line.Split(';');

            if (parts.Length >= 3)
            {
                if (DateTime.TryParseExact(parts[0].Trim(), "yyyy-MM-ddHH-mm-ss.fff", 
                        CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date))
                {
                    value.Date = date;
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

    public bool ValidateFile(UploadFileModelRequest fileModel)
    {
        var value = ParseFile(fileModel);
        
        return true;
    }
}