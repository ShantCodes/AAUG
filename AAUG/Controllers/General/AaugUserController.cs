
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
    [Authorize(Roles = "King,Varich")]
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

    [HttpPost("AssignRolesToUser")]
    [Authorize(Roles = "King,Varich")]
    public async Task<IActionResult> AssignRolesToUser(string userId, short roleId)
    {
        return Ok(await userService.AssignUserRolesAsync(userId, roleId));
    }

    [HttpDelete("UnAssignRolesToUser")]
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
    [HttpPost("updateSubscribtion")]
    [Authorize]
    public async Task<IActionResult> updateSubscribtion(IFormFile file)
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
    public async Task<IActionResult> EditAaugUserFull(AaugUserFullEditViewModel inputEntity)
    {
        return Ok(await AaugUserService.EditAaugUserFullAsync(inputEntity));
    }
    [HttpDelete("DeleteUser")]
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
    [HttpPut("ApproveAaugUser")]
    [Authorize(Roles = "King,Varich")]
    public async Task<IActionResult> ApproveAaugUser(int aaugUserId, bool IsApproved)
    {
        return Ok(await AaugUserService.ApproveAaugUserAsync(aaugUserId, IsApproved));
    }

}
