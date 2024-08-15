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


    public async Task<string> GenerateJwtToken(LoginDto user)
    {
        var userIdentity = await userManager.FindByNameAsync(user.Username);
        var getUser = await userManager.FindByIdAsync(userIdentity.Id);
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Username),
        };

        var roles = await userManager.GetRolesAsync(getUser);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetSection("Jwt:Key").Value));

        var signInCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

        var securityToken = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            issuer: configuration.GetSection("Jwt:Issuer").Value,
            audience: configuration.GetSection("Jwt:Audience").Value,
            signingCredentials: signInCred
        );

        string tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
        return tokenString;
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
                var userName = user.FindFirstValue(ClaimTypes.Email);
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
        throw new Exception("user not found");
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
