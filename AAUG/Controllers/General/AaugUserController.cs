﻿
using System.Security.Claims;
using AAUG.DomainModels.ViewModels;
using AAUG.Service.Interfaces;
using AAUG.Service.Interfaces.General;
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
    public async Task<IActionResult> GetAllUsers()
    {
        return Ok(await AaugUserService.GetAllUsersAsync());
    }

    [HttpGet("GetAllRoles")]
    public async Task<IActionResult> GetAllUserRoles()
    {
        return Ok(await userService.GetAllRolesAsync());
    }

    [HttpPost("AssignRolesToUser")]
    public async Task<IActionResult> AssignRolesToUser(string userId, short roleId)
    {
        return Ok(await userService.AssignUserRolesAsync(userId, roleId));
    }

    [HttpDelete("UnAssignRolesToUser")]
    public async Task<IActionResult> UnAssignRolesToUser(string userId, short roleId)
    {
        return Ok(await userService.UnassignRoleFromUserAsync(userId, roleId));
    }

}
