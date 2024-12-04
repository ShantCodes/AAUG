using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using AAUG.Service.Interfaces;
using Microsoft.AspNetCore.Identity;
using AAUG.DomainModels.Dtos;
using AAUG.DomainModels.Dtos.Authentication;
using Microsoft.AspNetCore.Authorization;
using AAUG.Service.Interfaces.EmailSender;

namespace AAUG.Api.Controllers.Authentication
{
    [Microsoft.AspNetCore.Mvc.Route("api/Authentication/")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly ITokenService tokenService;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly UserManager<IdentityUser> userManager;
        public AuthenticationController(ITokenService tokenService,
         SignInManager<IdentityUser> signInManager,
         UserManager<IdentityUser> userManager,
         IAuthService authService,
         IEmailSenderService emailSenderService)
        {
            this.tokenService = tokenService;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.authService = authService;

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] LoginDto loginModel)
        {
            var user = await authService.Login(loginModel);
            if (user != null)
            {
                var tokenString = await tokenService.GenerateJwtToken(user); // Pass `IdentityUser` object
                return Ok(tokenString);
            }
            return BadRequest("Invalid username or password");
        }


        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromForm] RegisterDto registerEntity)
        {
            return Ok(await authService.RegisterUserAsync(registerEntity));
        }

        [HttpPost("Forgotpassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            return Ok(await authService.ForgotPasswordAsync(forgotPasswordDto));
        }

        [HttpPost("resetpassword")]
        [Authorize]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            return Ok(await authService.ResetPasswordAsync(resetPasswordDto));
        }

    }

}

