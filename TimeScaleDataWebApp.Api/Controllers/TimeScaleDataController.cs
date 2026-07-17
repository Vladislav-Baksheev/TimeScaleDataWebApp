using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TimeScaleDataWebApp.Application.DTO;
using TimeScaleDataWebApp.Application.Services;
using TimeScaleDataWebApp.Domain.Entities;
using TimeScaleDataWebApp.Infrastructure;
using Results = TimeScaleDataWebApp.Domain.Entities.Results;

namespace TimeScaleDataWebApp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TimeScaleDataController(FileService fileService, FilterService filterService ,ApplicationDbContext dbContext) : ControllerBase
{
    [HttpPost("upload")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadFile([FromForm] UploadFileModelRequest fileModel)
    {
        await fileService.ProcessFileAsync(fileModel);

        return Ok();
    }

    [HttpGet("getResults")]
    public async Task<List<Results>> GetResultsWithFilter([FromQuery] FilterRequest request)
    {
        return await filterService.GetResultsAsync(request);
    }
}