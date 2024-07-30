using AAUG.Service.Interfaces.Media;
using Microsoft.AspNetCore.Mvc;

namespace AAUG.Api.Controllers.Media;

[Route("api/Media/")]
public class MediaFileController : ControllerBase
{
    private readonly IMediaFileService mediaFileService;
    public MediaFileController(IMediaFileService mediaFileService)
    {
        this.mediaFileService = mediaFileService;
    }
    [HttpGet("DownloadFile/{fileId}")]
    public async Task<IActionResult> DownloadFile(int fileId)
    {
        var fileResult = await mediaFileService.DownloadMediaFileAsync(fileId);
        return File(fileResult.FileBytes, fileResult.ContentType, fileResult.FileName);
    }

}