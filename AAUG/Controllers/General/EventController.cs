
using AAUG.DomainModels.ViewModels;
using AAUG.Service.Interfaces.General;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AAUG.Api;

[Route("api/Events/")]
public class EventController : ControllerBase
{
    private readonly IEventService eventService;
    public EventController(IEventService eventService)
    {
        this.eventService = eventService;
    }
    [HttpGet("GetAllEvents/{pageNumber}/{pageSize}")]
    public async Task<IActionResult> GetAllEvents(int pageNumber, int pageSize = 4)
    {
        return Ok(await eventService.GetAllEventsAsync(pageNumber, pageSize));
    }
    [HttpGet("GetReservedEventDates")]
    public async Task<IActionResult> GetReservedEventDates()
    {
        return Ok(await eventService.GetReservedEventDatesAsync());
    }
    [HttpPost("InsertEvent")]
    public async Task<IActionResult> InsertEvent([FromForm] EventInsertViewModel inputEntity)
    {
        return Ok(await eventService.InsertEventAsync(inputEntity));
    }
    [HttpPut("EditEventAsync")]
    [Authorize]
    public async Task<IActionResult> EditEvent([FromForm] EventEditViewModel inputEntity)
    {
        return Ok(await eventService.EditEventAsync(inputEntity));
    }
    [HttpPut("ApproveEvent/{eventId}/{isApproved}")]
    [Authorize(Roles = "Varich,King,Hanxnakhumb")]
    public async Task<IActionResult> ApproveEvent(int eventId, bool isApproved)
    {
        return Ok(await eventService.ApproveEvent(eventId, isApproved));
    }
    [HttpGet("GetAllNotApprovedEventsForAdmins")]
    [Authorize(Roles = "Varich,King, Hanxnakhumb")]
    public async Task<IActionResult> GetALlNotApprovedEventsForAdmins()
    {
        return Ok(await eventService.GetAllNotApprovedEventsForAdmins());
    }
    [HttpGet("GetAllEventsForAdmins")]
    [Authorize(Roles = "Varich,King,Hanxnakhumb")]
    public async Task<IActionResult> GetALlEventsForAdmins()
    {
        return Ok(await eventService.GetAllEventsForAdmins());
    }
    [HttpGet("SearchEvent/{keyWord}")]
    public async Task<IActionResult> SearchEvent(string keyWord)
    {
        return Ok(await eventService.SearchEventAsync(keyWord));
    }
    [HttpDelete("DeleteEvent/{eventId}")]
    [Authorize(Roles = "Varich,King, Hanxnakhumb")]
    public async Task<IActionResult> DeleteEvent(int eventId)
    {
        return Ok(await eventService.DeleteEventAsync(eventId));
    }
    #region Likes
    [HttpGet("GetEventLikes")]
    public async Task<IActionResult> GetEventLikes(int eventId)
    {
        return Ok(await eventService.GetEventLikesAsync(eventId));
    }
    [HttpPost("LikeEvent/{eventId}")]
    [Authorize]
    public async Task<IActionResult> LikeEvent(int eventId)
    {
        return Ok(await eventService.LikeEventAsync(eventId));
    }
    [HttpGet("CheckIfLiked/{eventId}")]
    [Authorize]
    public async Task<IActionResult> CheckIfLiked(int eventId)
    {
        return Ok(await eventService.CheckIfLiked(eventId));
    }
    #endregion

    #region evnet details
    [HttpGet("GetEventDetails/{eventId}")]
    public async Task<IActionResult> GetEventDetails(int eventId)
    {
        return Ok(await eventService.GetEventDetailsByIdAsync(eventId));
    }
    [HttpGet("InsertEventDetailsText")]
    [Authorize(Roles = "Varich,King, Hanxnakhumb")]
    public async Task<IActionResult> InsertEventDetailsText(IEnumerable<EventDetailsTextInsertViewModel> insertEntity)
    {
        return Ok(await eventService.InsertEventDetailTextsAsync(insertEntity));
    }
    [HttpDelete("DeleteEventDatailFile/{expandEventTextId}/{expandEventFileId}")]
    [Authorize(Roles = "Varich,King, Hanxnakhumb")]
    public async Task<IActionResult> DeleteEventDetailFile(int expandEventTextId, int expandEventFileId)
    {
        return Ok(await eventService.DeleteEventDetailFileAsync(expandEventTextId, expandEventFileId));
    }
    #endregion
}
