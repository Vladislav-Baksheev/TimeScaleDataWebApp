using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using TimeScaleDataWebApp.Application.DTO;
using TimeScaleDataWebApp.Application.Services;
using TimeScaleDataWebApp.Tests.Helpers;

namespace TimeScaleDataWebApp.Tests.Services;

public class FileServiceTests
{
    [Fact]
    public async Task ProcessFileAsync_IsCorrect()
    {
        // Arrange
        await using var context = DbContextFactory.Create();

        var service = new FileService(context);

        var csv = """
                  Date;ExecutionTime;Value
                  2025-07-15T10-00-00.0000Z;0.5;10.2
                  2025-07-15T10-00-01.0000Z;1.2;15.4
                  """;

        var bytes = Encoding.UTF8.GetBytes(csv);

        var stream = new MemoryStream(bytes);

        IFormFile formFile = new FormFile(
            stream,
            0,
            bytes.Length,
            "File",
            "1.csv");

        var request = new UploadFileModelRequest
        {
            File = formFile
        };

        // Act
        await service.ProcessFileAsync(request);
        
        // Assert
        Assert.Contains(context.Values, x => x.FileName == "1.csv");
        Assert.Equal(2, context.Values.Count());
        Assert.Single(context.Results);
    }
}