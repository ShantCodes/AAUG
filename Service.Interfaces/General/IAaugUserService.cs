using AAUG.DomainModels.Dtos;
using AAUG.DomainModels.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace AAUG.Service.Interfaces.General;

public interface IAaugUserService
{
    Task<AaugUserInsertViewModel> InsertUserInfoAsync(AaugUserInsertViewModel inputEntity);
    Task<IdentityUser> GetUserByIdAsync(string userId);
    Task<AaugUserFullGetViewModel> InsertFullUserInfoAsync(AaugUserFullInsertViewModel inputEntity);
    Task<AaugUserFullGetViewModel> UpdateSubscribtion(IFormFile receiptFile);
    Task<AaugUserFullEditViewModel> EditAaugUserFullAsync(AaugUserFullEditViewModel inputEntity);
    Task<AaugUserWithProfilePicureGetViewModel> InsertProfilePictureAsync(IFormFile profilePicture);
    Task<bool> ApproveSubscribtionAsync(int aaugUserId, bool approveSub);
    Task<AaugUserFullGetViewModel> GetAaugUserFullByAaugUserIdAsync(int aaugUserId);
    Task<IEnumerable<AaugUserGetViewModel>> GetSubscribedNotSubApprovedUsersAsync();
    Task<IEnumerable<AaugUserGetViewModel>> GetIsSubApprovedUsersAsync();
    Task<IEnumerable<AaugUserWithProfilePicureGetViewModel>> SearchAaugUserAsynv(string name);
    Task<AaugUserFullGetViewModel> GetCurrentAaugUserFullAsync();
    Task<IEnumerable<AaugUserGetViewModel>> GetAllUsersAsync();
    Task<IEnumerable<AaugUserGetViewModel>> GetApprovedUsersAsync();
    Task<AaugUserGetDto> GetCurrentUserInfo();
    #region admins
    Task<bool> DeleteUserAsync(int aaugUserId);
    Task<bool> ApproveAaugUserAsync(int aaugUserId, bool approveState);
    Task<IEnumerable<AaugUserGetViewModel>> GetNotApprovedAaugUsersAsync();
    #endregion
}
