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


    public async Task<AaugUserInsertViewModel> InsertUserInfoAsync(AaugUserInsertViewModel inputEntity)
    {
        var entity = mapper.Map<AaugUsersInsertDto>(inputEntity);
        var existingUser = await unitOfWork.AaugUserRepository.GetByUserName(inputEntity.Name).FirstOrDefaultAsync();
        if (existingUser != null)
        {
            throw new Exception("UserName already exists, try another");
        }
        await unitOfWork.AaugUserRepository.AddAsync(entity);

        await unitOfWork.SaveChangesAsync();
        await unitOfWork.CommitTransactionAsync();
        return inputEntity;
    }

    public async Task<AaugUserFullGetViewModel> InsertFullUserInfoAsync(AaugUserFullInsertViewModel inputEntity)
    {
        var aaugUser = await tokenService.GetAaugUserFromToken();
        var existingEntity = await unitOfWork.AaugUserRepository.GetFullUserInfoByUserIdWithTracking(aaugUser.Id).FirstOrDefaultAsync();
;
        var nationalCardFile = await mediaFileService.InsertUserMediaFileAsync(inputEntity.NationalCardFile);
        var universityCardFile = await mediaFileService.InsertUserMediaFileAsync(inputEntity.UniversityCardFile);
        var receiptFile = await mediaFileService.InsertUserMediaFileAsync(inputEntity.ReceiptFile);

        existingEntity.NationalCardFileId = nationalCardFile.Id;
        existingEntity.UniversityCardFileId = universityCardFile.Id;
        existingEntity.ReceiptFileId = receiptFile.Id;

        existingEntity.SubscribeDate = DateTime.UtcNow;

        await unitOfWork.SaveChangesAsync();
        await unitOfWork.CommitTransactionAsync();

        return mapper.Map<AaugUserFullGetViewModel>(existingEntity);

    }

    public async Task<AaugUserFullGetViewModel> UpdateSubscribtion(IFormFile receiptFile)
    {
        var aaugUser = await tokenService.GetAaugUserFromToken();

        var entity = await unitOfWork.AaugUserRepository.GetFullUserInfoByUserIdWithTracking(aaugUser.Id).FirstOrDefaultAsync();
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

    public async Task<AaugUserWithProfilePicureGetViewModel> InsertProfilePictureAsync(IFormFile profilePicture)
    {
        var aaugUser = await tokenService.GetAaugUserFromToken();
        var existingRecord = await unitOfWork.AaugUserRepository.GetFullUserInfoByUserIdWithTracking(aaugUser.Id).FirstOrDefaultAsync();

        var profilePictureFile = await mediaFileService.InsertUserMediaFileAsync(profilePicture);
        existingRecord.ProfilePictureFileId = profilePictureFile.Id;

        await unitOfWork.SaveChangesAsync();
        await unitOfWork.CommitTransactionAsync();

        return mapper.Map<AaugUserWithProfilePicureGetViewModel>(existingRecord);
    }

    public async Task<AaugUserFullEditViewModel> EditAaugUserFullAsync(AaugUserFullEditViewModel inputEntity)
    {
        var aaugUser = await tokenService.GetAaugUserFromToken();
        var userRole = tokenService.GetUserRoleFromToken();
        var existingRecord = await unitOfWork.AaugUserRepository.GetFullUserInfoByUserIdWithTracking(aaugUser.Id).FirstOrDefaultAsync();
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
    public async Task<AaugUserGetDto> GetCurrentUserInfo()
    {
        var userContext = httpContextAccessor.HttpContext.User;

        if (userContext == null)
            return null;
        var userEmail = userContext.FindFirst(ClaimTypes.Email)?.Value;
        var user = await userManager.FindByNameAsync(userEmail);

        return mapper.Map<AaugUserGetDto>(
            await unitOfWork.AaugUserRepository.GetUserByGuId(user.Id).FirstOrDefaultAsync()
        );
    }

    #region Admins

    public async Task<IList<string>> GetUserRolesAsync(string userId)
    {
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return null;
        }
        return await userManager.GetRolesAsync(user);
    }
    public async Task<IEnumerable<AaugUserGetViewModel>> GetAllUsersAsync()
    {
        var entity = await unitOfWork.AaugUserRepository.GetUsersAsync();
        var data = mapper.Map<IEnumerable<AaugUserGetViewModel>>(
            entity
        );
        return data;
    }

    public async Task<bool> DeleteUserAsync(int aaugUserId)
    {
        var aaugUser = await unitOfWork.AaugUserRepository.GetFullUserInfoByUserId(aaugUserId).FirstOrDefaultAsync();
        if (aaugUser == null)
            throw new Exception("user not found");

        var userId = aaugUser.UserId;
        await unitOfWork.AaugUserRepository.DeleteUserAsync(aaugUserId);
        await unitOfWork.SaveChangesAsync();

        await userService.DeleteUserAsync(userId);

        await unitOfWork.SaveChangesAsync();
        await unitOfWork.CommitTransactionAsync();

        return true;
    }

    public async Task<bool> ApproveAaugUserAsync(int aaugUserId, bool approveState)
    {
        var entity = await unitOfWork.AaugUserRepository.GetFullUserInfoByUserIdWithTracking(aaugUserId).FirstOrDefaultAsync();
        if (entity == null)
            throw new Exception("user not found");

        entity.IsApproved = approveState;

        await unitOfWork.SaveChangesAsync();
        await unitOfWork.CommitTransactionAsync();

        return true;
    }

    public async Task<IEnumerable<AaugUserGetViewModel>> GetNotApprovedAaugUsersAsync()
    {
        return mapper.Map<IEnumerable<AaugUserGetViewModel>>(
            await unitOfWork.AaugUserRepository.GetNotApprovedAaugUsers().ToListAsync()
        );
    }
    #endregion

}
