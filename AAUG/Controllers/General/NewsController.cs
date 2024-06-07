
using AAUG.DomainModels.ViewModels;
using AAUG.Service.Interfaces.General;
using Microsoft.AspNetCore.Mvc;

namespace AAUG.Api;

[Route("api/")]
public class NewsController : ControllerBase
{
    private readonly INewsService newsService;
    public NewsController(INewsService newsService)
    {
        this.newsService = newsService;
    }

    [HttpGet("GetNewsData")]
    public async Task<IActionResult> GetNewsData()
    {
        return Ok(await newsService.GetNewsAsync());
    }

    [HttpPost("InsertNews")]
    public async Task<IActionResult> InsertNews(NewsForInsertViewModel inputEntity)
    {
        return Ok(await newsService.InsertNewsAsync(inputEntity));
    }

}
