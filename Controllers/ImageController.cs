namespace ThreadsBackend.Controllers;

using Microsoft.AspNetCore.Mvc;
using ThreadsBackend.Services;

[Route("api/images")]
[ApiController]
public class ImageController : ControllerBase
{
    private readonly IManageImageService _manageImageService;

    public ImageController(IManageImageService manageImageService)
    {
        this._manageImageService = manageImageService;
    }

    [HttpGet("{filename}")]
    public ActionResult GetFile([FromRoute] string filename)
    {
        var filePath = this._manageImageService.GetFile(filename);
        var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        var contentType = GetContentType(filePath);
        return File(fileStream, contentType, filename);
    }

    private static string GetContentType(string filePath)
    {
        var ext = Path.GetExtension(filePath).ToLowerInvariant();
        return ext switch
        {
            ".jpg" => "image/jpg",
            ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            _ => "application/octet-stream",
        };
    }
}