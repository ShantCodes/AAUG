using AAUG.DomainModels.Dtos;
using AAUG.DomainModels.Models.Tables.General;

namespace AAUG.Service.Interfaces;

public interface ITokenService
{
    Task<string> GenerateJwtToken(LoginDto user);
    string GetUserFromToken();
    Task<string> GeneratePasswordResetTokenAsync(string email);
    Task<AaugUserGetDto> GetAaugUserFromToken();
    string GetUserRoleFromToken();

}
