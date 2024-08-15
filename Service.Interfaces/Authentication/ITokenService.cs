using AAUG.DomainModels.Dtos;

namespace AAUG.Service.Interfaces;

public interface ITokenService
{
    Task<string> GenerateJwtToken(LoginDto user);
    string GetUserFromToken();
    Task<string> GeneratePasswordResetTokenAsync(string email);
    Task<AaugUserGetDto> GetAaugUserFromToken();
    string GetUserRoleFromToken();

}
