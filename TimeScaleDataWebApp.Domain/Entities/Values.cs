namespace TimeScaleDataWebApp.Domain.Entities;

public class Values
{
    public int Id { get; set; }
    public DateTime? Date { get; set; }
    public float? ExecutionTime { get; set; }
    public float? Value { get; set; }
}