using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TimeScaleDataWebApp.Application.DTO;
using TimeScaleDataWebApp.Application.Services;
using TimeScaleDataWebApp.Domain.Entities;
using TimeScaleDataWebApp.Infrastructure;

namespace TimeScaleDataWebApp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TimeScaleDataController(FileService fileService, ApplicationDbContext dbContext) : ControllerBase
{
    [HttpPost("upload")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadFile([FromForm] UploadFileModelRequest fileModel)
    {
        var parseFile= fileService.ValidateFile(fileModel);

        try
        {
            dbContext.Values.Add(parseFile);
            await dbContext.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
        
        return Ok($"Файл: {parseFile.Date}, {parseFile.ExecutionTime}, {parseFile.Value}");
    }

    [HttpGet("get")]
    public List<Values> GetValues()
    {
        return dbContext.Values.ToList();
    }
}