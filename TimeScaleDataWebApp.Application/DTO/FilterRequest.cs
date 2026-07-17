namespace TimeScaleDataWebApp.Application.DTO;

public class FilterRequest
{
    public string? FileName { get; set; }
    
    public DateTime? DateFrom { get; set; }
    public DateTime? DateTo { get; set; }
    
    public double? AvgValueFrom { get; set; }
    public double? AvgValueTo { get; set; }
    
    public double? AvgExecutionTimeFrom { get; set; }
    public double? AvgExecutionTimeTo { get; set; }
}