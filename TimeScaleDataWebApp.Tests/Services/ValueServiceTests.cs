using TimeScaleDataWebApp.Application.Services;
using TimeScaleDataWebApp.Domain.Entities;
using TimeScaleDataWebApp.Tests.Helpers;

namespace TimeScaleDataWebApp.Tests.Services;

public class ValueServiceTests
{
    [Fact]
    public async Task GetSortedValues_ShouldReturnSortedValues()
    {
        // Arrange
        await using var context = DbContextFactory.Create();

        context.Values.AddRange(
            new Values
            {
                FileName = "1.csv",
                Date = new DateTime(2025, 7, 15),
                ExecutionTime = 12,
                Value = 1.2
            },
            new Values
            {
                FileName = "1.csv",
                Date = new DateTime(2025, 7, 16),
                ExecutionTime = 12,
                Value = 1.2
            },
            new Values
            {
                FileName = "1.csv",
                Date = new DateTime(2025, 7, 17),
                ExecutionTime = 12,
                Value = 1.2
            },
            new Values
            {
                FileName = "2.csv",
                Date = new DateTime(2025, 7, 18),
                ExecutionTime = 12,
                Value = 5
            }
        );

        await context.SaveChangesAsync();

        var service = new ValuesService(context);

        // Act
        var result = await service.GetSortedValuesAsync("1.csv");
        
        // Assert
        Assert.Equal(3, result.Count);

        Assert.All(result, x =>
        {
            Assert.Equal("1.csv", x.FileName);
        });

        Assert.Equal(
            new DateTime(2025, 7, 17),
            result[0].Date);

        Assert.Equal(
            new DateTime(2025, 7, 16),
            result[1].Date);

        Assert.Equal(
            new DateTime(2025, 7, 15),
            result[2].Date);
    }
}