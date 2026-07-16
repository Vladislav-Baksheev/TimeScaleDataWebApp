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
        await fileService.ProcessFileAsync(fileModel);

        return Ok();
    }

    [HttpGet("get")]
    public List<Values> GetValues()
    {
        return dbContext.Values.ToList();
    }
}