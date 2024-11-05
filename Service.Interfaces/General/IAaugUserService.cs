using AAUG.DomainModels.Dtos;
using AAUG.DomainModels.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace AAUG.Service.Interfaces.General;

public interface IAaugUserService
{
    Task<AaugUserInsertViewModel> InsertUserInfoAsync(AaugUserInsertViewModel inputEntity);
    Task<AaugUserFullGetViewModel> GetAaugUserByPhoneAsync(string phone);
    Task<IdentityUser> GetUserByIdAsync(string userId);
    Task<AaugUserFullGetViewModel> InsertFullUserInfoAsync(AaugUserFullInsertViewModel inputEntity);
    Task<AaugUserFullGetViewModel> UpdateSubscribtion(IFormFile receiptFile);
    Task<AaugUserFullGetViewModel> UpdateSubWithCodeAsync(int membershipCode);
    Task<AaugUserFullEditViewModel> EditAaugUserFullAsync(AaugUserFullEditViewModel inputEntity);
    Task<AaugUserWithProfilePicureGetViewModel> InsertProfilePictureAsync(IFormFile profilePicture);
    Task<bool> ApproveSubscribtionAsync(int aaugUserId, bool approveSub);
    Task<AaugUserFullGetViewModel> GetAaugUserFullByAaugUserIdAsync(int aaugUserId);
    Task<IEnumerable<AaugUserGetViewModel>> GetSubscribedNotSubApprovedUsersAsync(int pageNumber, int pageSize);
    Task<IEnumerable<AaugUserGetViewModel>> GetIsSubApprovedUsersAsync(int pageNumber, int pageSize);
    Task<IEnumerable<AaugUserWithProfilePicureGetViewModel>> SearchAaugUserAsynv(string name);
    Task<AaugUserFullGetViewModel> GetCurrentAaugUserFullAsync();
    Task<IEnumerable<AaugUserGetViewModel>> GetAllUsersAsync();
    Task<IEnumerable<AaugUserGetViewModel>> GetApprovedUsersAsync(int pageNumber, int pageSize = 4);
    Task<AaugUserGetDto> GetCurrentUserInfo();
    #region admins
    Task<bool> DeleteUserAsync(int aaugUserId);
    Task<bool> ApproveAaugUserAsync(int aaugUserId, bool approveState);
    Task<IEnumerable<AaugUserGetViewModel>> GetNotApprovedAaugUsersAsync(int pageNumber, int pageSize = 4);
    #endregion
}
