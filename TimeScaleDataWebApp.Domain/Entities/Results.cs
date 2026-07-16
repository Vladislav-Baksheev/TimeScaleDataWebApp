namespace TimeScaleDataWebApp.Domain.Entities;

public class Results
{
    public int Id { get; set; }
    public string FileName { get; set; } = null!;
    public double DeltaTime { get; set; }
    public DateTime StartDate { get; set; }
    public double AvgExecutionTime { get; set; }
    public double AvgValue { get; set; }
    public double MedianValue { get; set; }
    public double MaxValue { get; set; }
    public double MinValue { get; set; }
}