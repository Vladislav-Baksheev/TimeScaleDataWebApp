namespace TimeScaleDataWebApp.Domain.Entities;

public class Results
{
    public double DeltaSeconds { get; private set; }
    public DateTime StartDate { get; private set; }
    public double AvgExecutionTime { get; private set; }
    public double AvgValue { get; private set; }
    public double MedianValue { get; private set; }
    public double MaxValue { get; private set; }
    public double MinValue { get; private set; }
}