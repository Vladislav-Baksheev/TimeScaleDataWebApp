using TimeScaleDataWebApp.Application.DTO;
using TimeScaleDataWebApp.Application.Services;
using TimeScaleDataWebApp.Domain.Entities;
using TimeScaleDataWebApp.Infrastructure;
using TimeScaleDataWebApp.Tests.Helpers;

namespace TimeScaleDataWebApp.Tests.Services;

public class FilterServiceTests
{
    [Fact]
    public async Task GetResultsAsync_ShouldFilterByFileName()
    {
        // Arrange
        await using var context = DbContextFactory.Create();
        
        SeedResults(context);
        
        await context.SaveChangesAsync();
        
        var service = new FilterService(context);

        var request = new FilterRequest
        {
            FileName = "1.csv"
        };

        // Act
        var result = await service.GetResultsAsync(request);
        
        // Assert
        Assert.Equal(2, result.Count);
        Assert.All(result, x =>
        {
            Assert.Equal("1.csv", x.FileName);
        });
    }

    [Fact]
    public async Task GetResultsAsync_ShouldFilterByDate()
    {
        // Arrange
        await using var context = DbContextFactory.Create();
        
        SeedResults(context);
        
        await context.SaveChangesAsync();
        
        var service = new FilterService(context);

        var request = new FilterRequest
        {
            DateFrom = new DateTime(2025,07,15),
            DateTo = new DateTime(2025, 07, 15, 23, 59, 59)
        };

        // Act
        var result = await service.GetResultsAsync(request);
        
        // Assert
        Assert.Single(result);
        Assert.Equal(new DateTime(2025, 7, 15), result[0].StartDate);
    }

    private void SeedResults(ApplicationDbContext context)
    {
        context.Results.Add(new Results
        {
            FileName = "1.csv",
            AvgValue = 10,
            AvgExecutionTime = 1.5,
            StartDate = new DateTime(2025, 7, 15)
        });

        context.Results.Add(new Results
        {
            FileName = "1.csv",
            AvgValue = 20,
            AvgExecutionTime = 2.5,
            StartDate = new DateTime(2025, 7, 16)
        });

        context.Results.Add(new Results
        {
            FileName = "2.csv",
            AvgValue = 30,
            AvgExecutionTime = 3.5,
            StartDate = new DateTime(2025, 7, 17)
        });
    }
}