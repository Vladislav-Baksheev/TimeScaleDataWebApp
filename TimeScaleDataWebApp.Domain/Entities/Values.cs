namespace TimeScaleDataWebApp.Domain.Entities;

public class Values
{
    public int Id { get; set; }
    public string FileName { get; set; } = null!;
    public DateTime Date { get; set; }
    public double ExecutionTime { get; set; }
    public double Value { get; set; }
}