using AAUG.DomainModels.ViewModels;
using AAUG.Service.Interfaces.General;
using Microsoft.AspNetCore.Mvc;

namespace AAUG.Api.Controllers.General;

[Route("api/SlideShow/")]
public class SlideShowController : ControllerBase
{
    private readonly ISlideShowService slideShowService;
    public SlideShowController(ISlideShowService slideShowService)
    {
        this.slideShowService = slideShowService;
    }

    [HttpGet("GetSlideShows")]
    public async Task<IActionResult> GetSlideShows()
    {
        return Ok(await slideShowService.GetSlideShowsAsync());
    }
    [HttpPost("InsertSlideShows")]
    public async Task<IActionResult> InsertSlideShows([FromForm] SlideShowInsertViewModel inputEntity)
    {
        return Ok(await slideShowService.InsertSlideShowsAsync(inputEntity));
    }
    [HttpPost("InsertSlideShowTitle")]
    public async Task<IActionResult> InsertSlideShowTitle(SlideShowTitleInsertViewModel inputEntity)
    {
        return Ok(await slideShowService.InsertSlideShowTitleAsync(inputEntity));
    }
}