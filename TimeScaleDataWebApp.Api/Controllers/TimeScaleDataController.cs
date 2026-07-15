using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TimeScaleDataWebApp.Application.DTO;
using TimeScaleDataWebApp.Application.Services;

namespace TimeScaleDataWebApp.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TimeScaleDataController(FileService fileService) : ControllerBase
{
    [HttpPost("upload")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadFile([FromForm] UploadFileModelRequest fileModel)
    {
        var parseFile=fileService.ValidateFile(fileModel);
        
        return Ok($"Файл: {parseFile.Date}, {parseFile.ExecutionTime}, {parseFile.Value} ");
    }
}