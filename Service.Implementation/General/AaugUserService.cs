using System.Runtime.CompilerServices;
using AAUG.DataAccess.Implementations.UnitOfWork;
using AAUG.DomainModels.Dtos;
using AAUG.DomainModels.ViewModels;
using AAUG.Service.Interfaces;
using AAUG.Service.Interfaces.General;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace AAUG.Service.Implementations.General;

public class AaugUserService : IAaugUserService
{
    private readonly IMapper mapper;
    private IAaugUnitOfWork unitOfWork;
    private readonly UserManager<IdentityUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly IUserService userService;
    public AaugUserService(IAaugUnitOfWork unitOfWork, IMapper mapper, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, IUserService userService)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
        this.userManager = userManager;
        this.roleManager = roleManager;
        this.userService = userService;
    }

    public async Task<IdentityUser> GetUserByIdAsync(string userId)
    {
        return await userManager.FindByIdAsync(userId);
    }
    public async Task<IList<string>> GetUserRolesAsync(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return null;
        }
        return await userManager.GetRolesAsync(user);
    }

    public async Task<AaugUserInsertViewModel> InsertUserInfoAsync(AaugUserInsertViewModel inputEntity)
    {
        var entity = mapper.Map<AaugUsersInsertDto>(inputEntity);
        await unitOfWork.AaugUserRepository.AddAsync(entity);

        await unitOfWork.SaveChangesAsync();
        await unitOfWork.CommitTransactionAsync();
        return inputEntity;
    }

    public async Task<IEnumerable<AaugUserGetViewModel>> GetAllUsersAsync()
    {
        var entity = await unitOfWork.AaugUserRepository.GetUsersAsync();
        var data = mapper.Map<IEnumerable<AaugUserGetViewModel>>(
            entity
        );
        return data;
    }
}
