
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
    [HttpGet("GetAllEvents")]
    public async Task<IActionResult> GetAllEvents()
    {
        return Ok(await eventService.GetAllEventsAsync());
    }
    [HttpPost("InsertEvent")]
    public async Task<IActionResult> InsertEvent(EventInsertViewModel inputEntity)
    {
        return Ok(await eventService.InsertEventAsync(inputEntity));
    }
    [HttpPut("EditEventAsync")]
    public async Task<IActionResult> EditEvent([FromForm] EventEditViewModel inputEntity)
    {
        return Ok(await eventService.EditEventAsync(inputEntity));
    }
    [HttpPut("ApproveEvent/{eventId}/{isApproved}")]
    [Authorize(Roles = "Varich,King,Divan")]
    public async Task<IActionResult> ApproveEvent(int eventId, bool isApproved)
    {
        return Ok(await eventService.ApproveEvent(eventId, isApproved));
    }
    [HttpGet("GetAllNotApprovedEventsForAdmins")]
    [Authorize(Roles = "Varich,King")]
    public async Task<IActionResult> GetALlNotApprovedEventsForAdmins()
    {
        return Ok(await eventService.GetAllNotApprovedEventsForAdmins());
    }
    [HttpGet("GetAllEventsForAdmins")]
    [Authorize(Roles = "Varich,King,Divan")]
    public async Task<IActionResult> GetALlEventsForAdmins()
    {
        return Ok(await eventService.GetAllEventsForAdmins());
    }
    [HttpGet("SearchEvent/{keyWord}")]
    public async Task<IActionResult> SearchEvent(string keyWord)
    {
        return Ok(await eventService.SearchEventAsync(keyWord));
    }
    [HttpDelete("DeleteEvent")]
    [Authorize(Roles = "Varich,King")]
    public async Task<IActionResult> DeleteEvent(int eventId)
    {
        return Ok(await eventService.DeleteEventAsync(eventId));
    }
}
