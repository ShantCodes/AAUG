using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using AAUG.DataAccess.Implementations.General;
using AAUG.DataAccess.Implementations.UnitOfWork;
using AAUG.DomainModels.Dtos;
using AAUG.DomainModels.Dtos.Media;
using AAUG.DomainModels.Dtos.User;
using AAUG.DomainModels.Enums;
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

    public async Task<AaugUserFullGetViewModel> GetAaugUserFullByAaugUserIdAsync(int aaugUserId)
    {
        var entity = await unitOfWork.AaugUserRepository.GetFullUserInfoByUserId(aaugUserId).FirstOrDefaultAsync()
            ?? throw new Exception("user not found");

        return mapper.Map<AaugUserFullGetViewModel>(entity);

    }

    public async Task<AaugUserFullGetViewModel> GetAaugUserByPhoneAsync(string phone)
    {
        var entity = await unitOfWork.AaugUserRepository.GetFullUserInfoByPhone(phone).FirstOrDefaultAsync();
        if (entity == null)
        {
            return null;
        }
        return mapper.Map<AaugUserFullGetViewModel>(entity);
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
        entity.SubscribeDate = DateTime.UtcNow;
        entity.Subscribed = true;
        entity.IsSubApproved = false;

        await unitOfWork.SaveChangesAsync();
        await unitOfWork.CommitTransactionAsync();

        return mapper.Map<AaugUserFullGetViewModel>(entity);
    }

    public async Task<AaugUserFullGetViewModel> UpdateSubWithCodeAsync(int membershipCode)
    {
        var aaugUser = await tokenService.GetAaugUserFromToken();

        var entity = await unitOfWork.AaugUserRepository.GetFullUserInfoByUserIdWithTracking(aaugUser.Id).FirstOrDefaultAsync();
        if (entity == null)
            throw new Exception("the user not found");

        entity.MembershipCode = membershipCode;
        entity.SubscribeDate = DateTime.UtcNow;
        entity.Subscribed = true;
        entity.IsSubApproved = false;

        await unitOfWork.SaveChangesAsync();
        await unitOfWork.CommitTransactionAsync();

        return mapper.Map<AaugUserFullGetViewModel>(entity);
    }

    public async Task<IEnumerable<AaugUserGetViewModel>> GetIsSubApprovedUsersAsync(int pageNumber, int pageSize)
    {
        var skip = (pageNumber - 1) * pageSize;

        var entity = await unitOfWork.AaugUserRepository
                                     .GetIsSubApprovedUsers()
                                     .Skip(skip)
                                     .Take(pageSize)
                                     .ToListAsync();

        var data = mapper.Map<IEnumerable<AaugUserGetViewModel>>(entity);

        foreach (var item in data)
        {
            item.Role = await userManager.GetRolesAsync(
                await userManager.FindByIdAsync(item.UserId));
        }

        return data;
    }

    public async Task<bool> ApproveSubscribtionAsync(int aaugUserId, bool approveSub)
    {
        var aaugUserEntity = await unitOfWork.AaugUserRepository.GetAaugUserById(aaugUserId).FirstOrDefaultAsync()
            ?? throw new Exception("user not found");
        aaugUserEntity.IsSubApproved = approveSub;
        if (approveSub)
            await userService.AssignUserRolesAsync(aaugUserEntity.UserId, AaugRoles.Antam);
        else
        {
            await userService.UnassignRoleFromUserAsync(aaugUserEntity.UserId, AaugRoles.Antam);
            aaugUserEntity.Subscribed = false;
            aaugUserEntity.SubscribeDate = null;
        }

        await unitOfWork.SaveChangesAsync();
        await unitOfWork.CommitTransactionAsync();

        return true;
    }

    public async Task<AaugUserWithProfilePicureGetViewModel> InsertProfilePictureAsync(IFormFile profilePicture)
    {
        var aaugUser = await tokenService.GetAaugUserFromToken();
        var existingRecord = await unitOfWork.AaugUserRepository.GetFullUserInfoByUserIdWithTracking(aaugUser.Id).FirstOrDefaultAsync();
        var profilePictureFile = new MediaFileGetDto();
        if (existingRecord.ProfilePictureFileId != null)
            profilePictureFile = await mediaFileService.InsertUserMediaFileAsync(profilePicture, existingRecord.ProfilePictureFileId);
        else
            profilePictureFile = await mediaFileService.InsertUserMediaFileAsync(profilePicture);

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
        if (inputEntity.BornDate != null)
            existingRecord.BornDate = inputEntity.BornDate;

        if (inputEntity.Name != null)
            existingRecord.Name = inputEntity.Name;

        if (inputEntity.LastName != null)
            if (inputEntity.BornDate != null)
                existingRecord.LastName = inputEntity.LastName;

        if (inputEntity.NameArmenian != null)
            existingRecord.NameArmenian = inputEntity.NameArmenian;

        if (inputEntity.LastNameArmenian != null)
            existingRecord.LastNameArmenian = inputEntity.LastNameArmenian;

        if (inputEntity.Email != null)
            existingRecord.Email = inputEntity.Email;
        if (inputEntity.Phone != null)
        {
            existingRecord.Phone = inputEntity.Phone;
        }

        if (existingRecord == null)
            throw new Exception("the user data not found");

        var entity = mapper.Map<AaugUsersEditDto>(inputEntity);

        if (inputEntity.NationalCardFile != null)
        {
            var newMediaFileDto = await mediaFileService.InsertUserMediaFileAsync(inputEntity.NationalCardFile, existingRecord.NationalCardFileId);
            existingRecord.NationalCardFileId = newMediaFileDto.Id;
        }
        if (inputEntity.ReceiptFile != null)
        {
            var newMediaFileDto = await mediaFileService.InsertUserMediaFileAsync(inputEntity.ReceiptFile, existingRecord.ReceiptFileId);
            existingRecord.ReceiptFileId = newMediaFileDto.Id;
        }
        if (inputEntity.UniversityCardFile != null)
        {
            var newMediaFileDto = await mediaFileService.InsertUserMediaFileAsync(inputEntity.UniversityCardFile, existingRecord.UniversityCardFileId);
            existingRecord.UniversityCardFileId = newMediaFileDto.Id;
        }

        await unitOfWork.SaveChangesAsync();
        await unitOfWork.CommitTransactionAsync();

        return inputEntity;
    }
    public async Task<AaugUserGetDto> GetCurrentUserInfo()
    {
        var userContext = httpContextAccessor.HttpContext.User;
        string aaugUserRole = tokenService.GetUserRoleFromToken();

        if (userContext == null)
            return null;
        //var userEmail = userContext.FindFirst(ClaimTypes.Email)?.Value;
        var userId = userContext.FindFirst("UserId")?.Value;
        if (userId == null)
        {
            return null;
        }
        var user = await userManager.FindByIdAsync(userId);

        var result = mapper.Map<AaugUserGetDto>(
            await unitOfWork.AaugUserRepository.GetUserByGuId(user.Id).FirstOrDefaultAsync()
        );
        result.Role = aaugUserRole;

        return result;
    }

    public async Task<IEnumerable<AaugUserWithProfilePicureGetViewModel>> SearchAaugUserAsynv(string name)
    {
        return mapper.Map<IEnumerable<AaugUserWithProfilePicureGetViewModel>>(
            await unitOfWork.AaugUserRepository.SearchAaugUser(name).ToListAsync()
        );
    }

    public async Task<AaugUserFullGetViewModel> GetCurrentAaugUserFullAsync()
    {
        var aaugUser = await tokenService.GetAaugUserFromToken();
        var data = await unitOfWork.AaugUserRepository.GetFullUserInfoByUserId(aaugUser.Id)
            .FirstOrDefaultAsync()
            ?? throw new Exception("user not found");

        var result = mapper.Map<AaugUserFullGetViewModel>(data);

        return result;
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
        var data = mapper.Map<IEnumerable<AaugUserGetViewModel>>(entity);

        foreach (var item in data)
        {
            var x = await userManager.FindByIdAsync(item.UserId);
            item.Role = await userManager.GetRolesAsync(x);
        }

        return data;

    }

    public async Task<IEnumerable<AaugUserGetViewModel>> GetSubscribedNotSubApprovedUsersAsync(int pageNumber, int pageSize)
    {
        var skip = (pageNumber - 1) * pageSize;

        var entity = await unitOfWork.AaugUserRepository
                                     .GetSubscribedNotSubApprovedUsers()
                                     .Skip(skip)
                                     .Take(pageSize)
                                     .ToListAsync();

        var data = mapper.Map<IEnumerable<AaugUserGetViewModel>>(entity);

        foreach (var item in data)
        {
            var user = await userManager.FindByIdAsync(item.UserId);
            item.Role = await userManager.GetRolesAsync(user);
        }

        return data;
    }


    public async Task<IEnumerable<AaugUserGetViewModel>> GetApprovedUsersAsync(int pageNumber, int pageSize = 4)
    {
        var skip = (pageNumber - 1) * pageSize;

        var entity = await unitOfWork.AaugUserRepository
                                     .GetApprovedAaugUsers()
                                     .Skip(skip)
                                     .Take(pageSize)
                                     .ToListAsync();

        var data = mapper.Map<IEnumerable<AaugUserGetViewModel>>(entity);

        foreach (var item in data)
        {
            item.Role = await userManager.GetRolesAsync(
                await userManager.FindByIdAsync(item.UserId));
        }

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

    public async Task<IEnumerable<AaugUserGetViewModel>> GetNotApprovedAaugUsersAsync(int pageNumber, int pageSize = 4)
    {
        var skip = (pageNumber - 1) * pageSize;

        var entity = await unitOfWork.AaugUserRepository
                                     .GetNotApprovedAaugUsers()
                                     .Skip(skip)
                                     .Take(pageSize)
                                     .ToListAsync();

        var data = mapper.Map<IEnumerable<AaugUserGetViewModel>>(entity);

        foreach (var item in data)
        {
            item.Role = await userManager.GetRolesAsync(
                await userManager.FindByIdAsync(item.UserId));
        }

        return data;
    }

    #endregion

}
