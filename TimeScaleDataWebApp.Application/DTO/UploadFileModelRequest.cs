using Microsoft.AspNetCore.Http;

namespace TimeScaleDataWebApp.Application.DTO;

public class UploadFileModelRequest 
{
    public IFormFile File { get; set; }
}