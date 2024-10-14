using AAUG.DomainModels.Dtos;
using AAUG.DomainModels.Models.Tables.General;
using Microsoft.AspNetCore.Identity;

namespace AAUG.Service.Interfaces;

public interface ITokenService
{
    Task<string> GenerateJwtToken(IdentityUser user);
    string GetUserFromToken();
    Task<string> GeneratePasswordResetTokenAsync(string email);
    Task<AaugUserGetDto> GetAaugUserFromToken();
    string GetUserRoleFromToken();

}
