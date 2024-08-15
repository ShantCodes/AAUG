using System.Net;
using AAUG.DomainModels.Dtos;
using AAUG.DomainModels.Dtos.Authentication;
using AAUG.DomainModels.ViewModels;
using AAUG.Service.Interfaces;
using AAUG.Service.Interfaces.EmailSender;
using AAUG.Service.Interfaces.General;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace AAUG.Service.Implementations;

public class AuthService : IAuthService
{
    private IConfiguration configuration;
    private readonly UserManager<IdentityUser> userManager;
    private readonly ITokenService tokenService;
    private readonly IEmailSenderService emailSenderService;
    private readonly IAaugUserService aaugUserService;
    private readonly IMapper mapper;
    private readonly IHttpContextAccessor httpContextAccessor;
    public AuthService(IConfiguration configuration,
                        UserManager<IdentityUser> userManager,
                        ITokenService tokenService,
                        IEmailSenderService emailSenderService,
                        IAaugUserService aaugUserService,
                        IMapper mapper,
                        IHttpContextAccessor httpContextAccessor)
    {
        this.configuration = configuration;
        this.userManager = userManager;
        this.tokenService = tokenService;
        this.emailSenderService = emailSenderService;
        this.aaugUserService = aaugUserService;
        this.mapper = mapper;
        this.httpContextAccessor = httpContextAccessor;
    }
    public async Task<bool> Login(LoginDto user)
    {
        var identityUser = await userManager.FindByEmailAsync(user.Username);
        if (identityUser is null)
        {
            return false;
        }

        return await userManager.CheckPasswordAsync(identityUser, user.Password);
    }


    public async Task<bool> RegisterUserAsync(RegisterDto registerEntity)
    {
        if (!object.Equals(registerEntity.Password, registerEntity.ConfirmPassword))
            return false;

        var identityUser = new IdentityUser
        {
            UserName = registerEntity.Username,
            Email = registerEntity.Username
        };

        var result = await userManager.CreateAsync(identityUser, registerEntity.Password);
        var userId = await userManager.FindByNameAsync(registerEntity.Username);
        var userinfoData = mapper.Map<AaugUserInsertViewModel>(registerEntity);
        userinfoData.UserId = userId.Id;
        var aaugUser = await aaugUserService.InsertUserInfoAsync(userinfoData);
        return result.Succeeded;
    }

    public async Task<bool> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto)
    {
        var user = await userManager.FindByEmailAsync(forgotPasswordDto.Email);
        if (user == null)
        {
            // Don't reveal that the user does not exist or is not confirmed
            return false;
        }

        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        var callbackUrl = $"{configuration["AppUrl"]}/api/account/resetpassword?userId={user.Id}&token={WebUtility.UrlEncode(token)}";

        await emailSenderService.SendEmailAsync(forgotPasswordDto.Email, "Reset Password",
            $"Please reset your password by clicking here: <a href='{callbackUrl}'>link</a>");

        return true;
    }

    public async Task<bool> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
    {
        var aaugUser = await tokenService.GetAaugUserFromToken();
        if (aaugUser == null)
        {
            throw new Exception("user not found or wrong inputs");
        }
        var user = await userManager.FindByIdAsync(aaugUser.UserId);
        if (user == null)
        {
            throw new Exception("User not found");
        }

        var result = await userManager.ChangePasswordAsync(user, resetPasswordDto.CurrentPassword, resetPasswordDto.Password);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new Exception($"Password reset failed: {errors}");
        }

        return true;
    }

}
