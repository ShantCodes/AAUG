using AAUG.DomainModels.ViewModels;

namespace AAUG.Service.Interfaces.General;

public interface IAaugUserService
{
    Task<AaugUserInsertViewModel> InsertUserInfoAsync(AaugUserInsertViewModel inputEntity);
    Task<IEnumerable<AaugUserGetViewModel>> GetAllUsersAsync();
}
