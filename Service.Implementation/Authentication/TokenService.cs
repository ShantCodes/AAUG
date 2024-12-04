using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AAUG.DataAccess.Implementations.UnitOfWork;
using AAUG.DomainModels.Dtos;
using AAUG.Service.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AAUG.Service.Implementations;

public class TokenService : ITokenService
{
    private IConfiguration configuration;
    private readonly UserManager<IdentityUser> userManager;
    private readonly IHttpContextAccessor httpContextAccessor;
    private IAaugUnitOfWork unitOfWork;
    public TokenService(IConfiguration configuration, UserManager<IdentityUser> userManager, IHttpContextAccessor httpContextAccessor, IAaugUnitOfWork unitOfWork)
    {
        this.configuration = configuration;
        this.userManager = userManager;
        this.httpContextAccessor = httpContextAccessor;
        this.unitOfWork = unitOfWork;
    }


    public async Task<string> GenerateJwtToken(IdentityUser user)
    {
        // Retrieve user details
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(ClaimTypes.Email, user.Email),
        new Claim("Username", user.UserName),
        new Claim("UserId", user.Id),
        new Claim("PhoneNumber", user.PhoneNumber ?? string.Empty) // Add phone number as custom claim
    };

        // Add roles as claims
        var roles = await userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        // Create signing credentials
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        var signInCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        // Generate the JWT token
        var securityToken = new JwtSecurityToken(
            issuer: configuration["Jwt:Issuer"],
            audience: configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: signInCred
        );

        // Return the token as a string
        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }



    public async Task<string> GeneratePasswordResetTokenAsync(string email)
    {
        var user = await userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return null;
        }

        var token = await userManager.GeneratePasswordResetTokenAsync(user);
        return token;
    }
    public async Task<AaugUserGetDto> GetAaugUserFromToken()
    {
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext != null)
        {
            var user = httpContext.User;
            if (user != null)
            {
                var userName = user.FindFirst(ClaimTypes.Email)?.Value;
                var userId = user.FindFirst("UserId")?.Value;
                var email = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (userId != null)
                {
                    var userGuId = await userManager.FindByIdAsync(userId);
                     if (userGuId == null)
                        throw new Exception("user not found");
                    var aaugUser = await unitOfWork.AaugUserRepository.GetUserByGuId(userGuId.Id).FirstAsync();
                    return aaugUser;
                }
                if (userName != null)
                {
                    var userGuId = await userManager.FindByNameAsync(userName);
                    if (userGuId == null)
                        throw new Exception("user not found");
                    var aaugUser = await unitOfWork.AaugUserRepository.GetUserByGuId(userGuId.Id).FirstAsync();
                    return aaugUser;
                }
            }
        }
        // throw new Exception("user not found");
        return null;
    }

    public string GetUserRoleFromToken()
    {
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext != null)
        {
            var user = httpContext.User;
            if (user != null)
            {
                var userRole = user.FindFirstValue(ClaimTypes.Role);
                if (userRole == null)
                    return null;
                return userRole;
            }
        }
        return null;
    }

    public string GetUserFromToken()
    {
        return null;
    }

}
