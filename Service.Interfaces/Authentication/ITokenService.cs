using AAUG.DomainModels.Dtos;

namespace AAUG.Service.Interfaces;

public interface ITokenService
{
    Task<string> GenerateJwtToken(LoginDto user);

    Task<string> GeneratePasswordResetTokenAsync(string email);

}
