
using System.Security.Claims;
using AAUG.DomainModels.Models.Tables.General;
using AAUG.DomainModels.ViewModels;
using AAUG.Service.Interfaces;
using AAUG.Service.Interfaces.General;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AAUG.Api.Controllers.General;

[Route("api/AaugUser/")]
public class AaugUserController : ControllerBase
{
    private readonly IAaugUserService AaugUserService;
    private readonly IUserService userService;
    public AaugUserController(IAaugUserService AaugUserService, IUserService userService)
    {
        this.AaugUserService = AaugUserService;
        this.userService = userService;
    }

    [HttpPost("InsertAaugUserInfo")]
    public async Task<IActionResult> InsertAaugUserInfo(AaugUserInsertViewModel inputEntity)
    {
        return Ok(await AaugUserService.InsertUserInfoAsync(inputEntity));
    }

    [HttpGet("GetAllUsers")]
    [Authorize(Roles = "King,Varich, Hanxnakhumb")]
    public async Task<IActionResult> GetAllUsers()
    {
        return Ok(await AaugUserService.GetAllUsersAsync());
    }
    [HttpGet("SearchUsers/{name}")]
    [Authorize(Roles = "King,Varich")]
    public async Task<IActionResult> SearchUsers(string name)
    {
        return Ok(await AaugUserService.SearchAaugUserAsynv(name));
    }
    [HttpGet("GetAllRoles")]
    [Authorize(Roles = "King,Varich")]
    public async Task<IActionResult> GetAllUserRoles()
    {
        return Ok(await userService.GetAllRolesAsync());
    }
    [HttpGet("GetAaugUserFullByAaugUserId/{aaugUserId}")]
    [Authorize(Roles ="King,Varich")]
    public async Task<IActionResult> GetAaugUserFullByAaugUserId(int aaugUserId)
    {
        return Ok(await AaugUserService.GetAaugUserFullByAaugUserIdAsync(aaugUserId));
    }

    [HttpPost("AssignRolesToUser/{userId}/{roleId}")]
    [Authorize(Roles = "King,Varich")]
    public async Task<IActionResult> AssignRolesToUser(string userId, short roleId)
    {
        return Ok(await userService.AssignUserRolesAsync(userId, roleId));
    }

    [HttpDelete("UnAssignRolesToUser/{userId}/{roleId}")]
    [Authorize(Roles = "King,Varich")]
    public async Task<IActionResult> UnAssignRolesToUser(string userId, short roleId)
    {
        return Ok(await userService.UnassignRoleFromUserAsync(userId, roleId));
    }
    [HttpGet("GetCurrentUserInfo")]
    [Authorize]
    public async Task<IActionResult> GetCurrentUserInfo()
    {
        return Ok(await AaugUserService.GetCurrentUserInfo());
    }
    [HttpPut("SubscribeMembership")]
    [Authorize]
    public async Task<IActionResult> SubscribeMembership(AaugUserFullInsertViewModel insertEntity)
    {
        return Ok(await AaugUserService.InsertFullUserInfoAsync(insertEntity));
    }
    [HttpPost("UpdateSubscribtion")]
    [Authorize]
    public async Task<IActionResult> UpdateSubscribtion(IFormFile file)
    {
        return Ok(await AaugUserService.UpdateSubscribtion(file));
    }
    [HttpPut("InsertProfilePicture")]
    [Authorize]
    public async Task<IActionResult> InsertProfilePicture(IFormFile profilePictureFile)
    {
        return Ok(await AaugUserService.InsertProfilePictureAsync(profilePictureFile));
    }
    [HttpPut("EditAaugUserFull")]
    [Authorize]
    public async Task<IActionResult> EditAaugUserFull([FromForm] AaugUserFullEditViewModel inputEntity)
    {
        return Ok(await AaugUserService.EditAaugUserFullAsync(inputEntity));
    }
    [HttpGet("GetCurrentAaugUserFull")]
    [Authorize]
    public async Task<IActionResult> GetCurrentAaugUserFull()
    {
        return Ok(await AaugUserService.GetCurrentAaugUserFullAsync());
    }
    [HttpDelete("DeleteUser/{aaugUserId}")]
    [Authorize(Roles = "Varich,King")]
    public async Task<IActionResult> DeleteUser(int aaugUserId)
    {
        return Ok(await AaugUserService.DeleteUserAsync(aaugUserId));
    }
    [HttpGet("GetNotApprovedUsers")]
    [Authorize(Roles = "King,Varich")]
    public async Task<IActionResult> GetNotApprovedUsers()
    {
        return Ok(await AaugUserService.GetNotApprovedAaugUsersAsync());
    }
    [HttpGet("GetApprovedUsers")]
    [Authorize(Roles = "King, Varich")]
    public async Task<IActionResult> GetApprovedUsers()
    {
        return Ok(await AaugUserService.GetApprovedUsersAsync());
    }
    [HttpPut("ApproveAaugUser/{aaugUserId}/{IsApproved}")]
    [Authorize(Roles = "King,Varich")]
    public async Task<IActionResult> ApproveAaugUser(int aaugUserId, bool IsApproved)
    {
        return Ok(await AaugUserService.ApproveAaugUserAsync(aaugUserId, IsApproved));
    }
    [HttpPut("ApproveSubscribtion/{aaugUserId}/{approveSub}")]
    [Authorize(Roles ="King,Varich")]
    public async Task<IActionResult> ApproveSubscribtion(int aaugUserId, bool approveSub)
    {
        return Ok(await AaugUserService.ApproveSubscribtionAsync(aaugUserId, approveSub));
    }
    [HttpGet("GetSubscribedNotSubApprovedUsers")]
    [Authorize(Roles ="King,Varich")]
    public async Task<IActionResult> GetSubscribedNotSubApprovedUsers()
    {
        return Ok(await AaugUserService.GetSubscribedNotSubApprovedUsersAsync());
    }
    [HttpGet("GetIsSubApprovedUsers")]
    [Authorize(Roles ="King,Varich")]
    public async Task<IActionResult> GetIsSubApprovedUsers()
    {
        return Ok(await AaugUserService.GetIsSubApprovedUsersAsync());
    }

}
