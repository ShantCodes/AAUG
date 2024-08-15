using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using AAUG.DataAccess.Implementations.UnitOfWork;
using AAUG.DomainModels.Dtos;
using AAUG.DomainModels.Dtos.User;
using AAUG.DomainModels.ViewModels;
using AAUG.Service.Implementations.Media;
using AAUG.Service.Interfaces;
using AAUG.Service.Interfaces.General;
using AAUG.Service.Interfaces.Media;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AAUG.Service.Implementations.General;

public class AaugUserService : IAaugUserService
{
    #region injection
    private readonly IMapper mapper;
    private IAaugUnitOfWork unitOfWork;
    private readonly UserManager<IdentityUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly IUserService userService;
    private readonly IMediaFileService mediaFileService;
    private readonly ITokenService tokenService;
    private readonly IHttpContextAccessor httpContextAccessor;
    public AaugUserService(IAaugUnitOfWork unitOfWork,
                            IMapper mapper,
                            UserManager<IdentityUser> userManager,
                            RoleManager<IdentityRole> roleManager,
                            IUserService userService,
                            IMediaFileService mediaFileService,
                            ITokenService tokenService,
                            IHttpContextAccessor httpContextAccessor)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
        this.userManager = userManager;
        this.roleManager = roleManager;
        this.userService = userService;
        this.mediaFileService = mediaFileService;
        this.tokenService = tokenService;
        this.httpContextAccessor = httpContextAccessor;
    }
    #endregion

    public async Task<IdentityUser> GetUserByIdAsync(string userId)
    {
        var result = await userManager.FindByIdAsync(userId);
        if (result == null)
        {
            throw new KeyNotFoundException();
        }
        return result;
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

    public async Task<AaugUserFullInsertViewModel> InsertFullUserInfoAsync(AaugUserFullInsertViewModel inputEntity)
    {
        var existingEntity = await unitOfWork.AaugUserRepository.GetFullUserInfoByUserId(inputEntity.Id).FirstOrDefaultAsync();

        var profilePictureFile = await mediaFileService.InsertUserMediaFileAsync(inputEntity.ProfilePictureFile);
        var nationalCardFile = await mediaFileService.InsertUserMediaFileAsync(inputEntity.NationalCardFile);
        var universityCardFile = await mediaFileService.InsertUserMediaFileAsync(inputEntity.UniversityCardFile);
        var receiptFile = await mediaFileService.InsertUserMediaFileAsync(inputEntity.ReceiptFile);
        existingEntity.NationalCardFileId = nationalCardFile.Id;
        existingEntity.UniversityCardFileId = universityCardFile.Id;
        existingEntity.ProfilePictureFileId = profilePictureFile.Id;
        existingEntity.ReceiptFileId = receiptFile.Id;

        await unitOfWork.SaveChangesAsync();
        await unitOfWork.CommitTransactionAsync();

        return mapper.Map<AaugUserFullInsertViewModel>(existingEntity);

    }

    public async Task<AaugUserFullGetViewModel> UpdateSubscribtion(int userId, IFormFile receiptFile)
    {
        var entity = await unitOfWork.AaugUserRepository.GetFullUserInfoByUserIdWithTracking(userId).FirstOrDefaultAsync();
        if (entity == null)
            throw new Exception("the user not found");

        var NewMediaFileDto = await mediaFileService.InsertUserMediaFileAsync(receiptFile, entity.ReceiptFileId); 
        entity.ReceiptFileId = NewMediaFileDto.Id;
        entity.IsApproved = false;        
        entity.SubscribeDate = DateTime.UtcNow;

        await unitOfWork.SaveChangesAsync();
        await unitOfWork.CommitTransactionAsync();   

        return mapper.Map<AaugUserFullGetViewModel>(entity);
    }

    public async Task<AaugUserFullEditViewModel> EditAaugUserFullAsync(AaugUserFullEditViewModel inputEntity)
    {
        var existingRecord = await unitOfWork.AaugUserRepository.GetFullUserInfoByUserIdWithTracking(inputEntity.Id).FirstOrDefaultAsync();
        if (existingRecord == null)
            throw new Exception("the user data not found");

        var entity = mapper.Map<AaugUsersEditDto>(inputEntity);
        mapper.Map(entity, existingRecord);

        if (inputEntity.NationalCardFile != null)
        {
            var newMediaFileDto = await mediaFileService.InsertUserMediaFileAsync(inputEntity.NationalCardFile, existingRecord.NationalCardFileId);
            existingRecord.NationalCardFileId = newMediaFileDto.Id;
        }
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

    public async Task<AaugUserGetDto> GetCurrentUserInfo()
    {
        var userContext = httpContextAccessor.HttpContext.User;

        if (userContext == null)
            return null;
        var userEmail = userContext.FindFirst(ClaimTypes.Email)?.Value;
        var user = await userManager.FindByNameAsync(userEmail);

        return await unitOfWork.AaugUserRepository.GetUserByGuId(user.Id).FirstOrDefaultAsync();
    }    

}
