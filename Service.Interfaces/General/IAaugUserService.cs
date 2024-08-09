using AAUG.DomainModels.Dtos;
using AAUG.DomainModels.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace AAUG.Service.Interfaces.General;

public interface IAaugUserService
{
    Task<AaugUserInsertViewModel> InsertUserInfoAsync(AaugUserInsertViewModel inputEntity);
    Task<IdentityUser> GetUserByIdAsync(string userId);
    Task<AaugUserFullInsertViewModel> InsertFullUserInfoAsync(AaugUserFullInsertViewModel inputEntity);
    Task<AaugUserFullGetViewModel> UpdateSubscribtion(int userId, IFormFile receiptFile);
    Task<AaugUserFullEditViewModel> EditAaugUserFullAsync(AaugUserFullEditViewModel inputEntity);
    Task<IEnumerable<AaugUserGetViewModel>> GetAllUsersAsync();
    Task<AaugUserGetDto> GetCurrentUserInfo();

}
