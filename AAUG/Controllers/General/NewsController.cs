
using AAUG.DomainModels.ViewModels;
using AAUG.Service.Interfaces.General;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AAUG.Api.Controllers.General;

[Route("api/News/")]
public class NewsController : ControllerBase
{
    private readonly INewsService newsService;
    public NewsController(INewsService newsService)
    {
        this.newsService = newsService;
    }
    [HttpGet("SearchNewsByTitle/{newsTitle}")]
    public async Task<IActionResult> SearchNewsByTitle(string newsTitle)
    {
        return Ok(await newsService.GetNewsByTitleAsync(newsTitle));
    }
    [HttpGet("GetNewsData")]
    public async Task<IActionResult> GetNewsData()
    {
        return Ok(await newsService.GetNewsAsync());
    }

    [HttpPost("InsertNews")]
    [Authorize(Roles = "King,Varich,Hanxnakhumb")]
    public async Task<IActionResult> InsertNews(NewsForInsertViewModel inputEntity)
    {
        return Ok(await newsService.InsertNewsAsync(inputEntity));
    }

    [HttpDelete("DeleteNews/{id}")]
    [Authorize(Roles = "King,Varich,Hanxnakhumb")]
    public async Task<IActionResult> DeleteNews(int id)
    {
        return Ok(await newsService.DeleteNewsByIdAsync(id));
    }
    [HttpPut("EditNews")]
    [Authorize(Roles = "King,Varich,Hanxnakhumb")]
    public async Task<IActionResult> EditNews(NewsForEditViewModel inputEntity)
    {
        return Ok(await newsService.EditNewsAsync(inputEntity));
    }

}
