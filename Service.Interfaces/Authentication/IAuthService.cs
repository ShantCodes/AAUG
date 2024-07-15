using AAUG.DomainModels.Dtos;
using AAUG.DomainModels.Dtos.Authentication;

namespace AAUG.Service.Interfaces;

public interface IAuthService
{
    Task<bool> Login(LoginDto user);
    Task<bool> RegisterUserAsync(RegisterDto registerEntity);
    Task<bool> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto);
    Task<bool> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
}
