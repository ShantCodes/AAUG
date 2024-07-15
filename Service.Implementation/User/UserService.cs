﻿using AAUG.DomainModels.Enums;
using AAUG.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using static AAUG.DomainModels.Enums.AaugRoles;

namespace AAUG.Service.Implementations;

public class UserService : IUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<IdentityUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;

    public UserService(IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _httpContextAccessor = httpContextAccessor;
        this.userManager = userManager;
        this.roleManager = roleManager;
    }

    public string GetCurrentUserId()
    {
        var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return userId;
    }

    public string GetCurrentUserName()
    {
        var userName = _httpContextAccessor.HttpContext?.User?.Identity?.Name;
        return userName;
    }

    public async Task<string> AssignUserRolesAsync(string userId, short roleId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
            return "user not found";
        var roleName = AaugRoles.Mapper(roleId);
        if (roleName == null)
            return "role not found";
        var roleExists = await roleManager.RoleExistsAsync(roleName);
        if (!roleExists)
            return "role does not exist in the database";
        var result = await userManager.AddToRoleAsync(user, roleName);
        if (result.Succeeded)
            return $"the user is now {roleName}";

        return "request failed";
    }

    public async Task<IdentityResult> UnassignRoleFromUserAsync(string userId, short roleId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            throw new Exception($"User with email {userId} not found.");
        }

        var roleName = AaugRoles.Mapper(roleId);
        if (roleName == null)
        {
            throw new Exception($"Role with ID {roleId} not found.");
        }

        return await userManager.RemoveFromRoleAsync(user, roleName);
    }

    public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
    {
        return AaugRoles.GetAllRolesWithIds()
            .Select(r => new RoleDto { Id = r.Id, Name = r.Name })
            .ToList();
    }

}
