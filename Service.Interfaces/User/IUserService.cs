using Microsoft.AspNetCore.Identity;
using static AAUG.DomainModels.Enums.AaugRoles;

namespace AAUG.Service.Interfaces;

public interface IUserService
{
    string GetCurrentUserId();
    string GetCurrentUserName();
    Task<string> AssignUserRolesAsync(string userId, short roleId);
    Task<IdentityResult> UnassignRoleFromUserAsync(string userId, short roleId);
    Task<IEnumerable<RoleDto>> GetAllRolesAsync();
    
}
