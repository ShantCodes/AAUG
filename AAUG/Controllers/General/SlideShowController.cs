using AAUG.DomainModels.ViewModels;
using AAUG.Service.Interfaces.General;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "King,Varich")]
    public async Task<IActionResult> GetSlideShows()
    {
        return Ok(await slideShowService.GetSlideShowsAsync());
    }
    [HttpGet("GetSlideShowsFrAdmins")]
    [Authorize(Roles = "King,Varich")]
    public async Task<IActionResult> GetSlideShowsForAdmins()
    {
        return Ok(await slideShowService.GetAllSlideShowsForAdminsAsync());
    }
    [HttpPost("InsertSlideShows")]
    [Authorize(Roles = "King,Varich")]
    public async Task<IActionResult> InsertSlideShows([FromForm] SlideShowInsertViewModel inputEntity)
    {
        return Ok(await slideShowService.InsertSlideShowsAsync(inputEntity));
    }
    [HttpPost("InsertSlideShowTitle")]
    [Authorize(Roles = "King,Varich")]
    public async Task<IActionResult> InsertSlideShowTitle(SlideShowTitleInsertViewModel inputEntity)
    {
        return Ok(await slideShowService.InsertSlideShowTitleAsync(inputEntity));
    }
    [HttpGet("GetSlideShowWithTitle")]
    public async Task<IActionResult> GetSlideShowWithTitle()
    {
        return Ok(await slideShowService.GetSlideShowsWithTitleAsync());
    }
    [HttpPut("SelectSlides")]
    [Authorize(Roles = "King,Varich")]
    public async Task<IActionResult> SelectSlides(List<int> slideIds)
    {
        return Ok(await slideShowService.SelectSlidesAsync(slideIds));
    }
    [HttpDelete("DeleteSlide/{slideId}")]
    public async Task<IActionResult> DeleteSlide(int slideId)
    {
        return Ok(await slideShowService.DeleteSlidesAsync(slideId));
    }
}