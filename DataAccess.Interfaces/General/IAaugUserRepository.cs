using AAUG.DomainModels.Dtos;
using AAUG.DomainModels.Models.Tables.General;

namespace AAUG.DataAccess.Interfaces.General;

public interface IAaugUserRepository
{
    Task<AaugUser> AddAsync(AaugUsersInsertDto inputEntity);
    IQueryable<AaugUserWithProfilePictureGetDto> SearchAaugUser(string name);
    IQueryable<AaugUserGetDto> GetUsers();
    Task<List<AaugUser>> GetUsersAsync();
    IQueryable<AaugUserGetDto> GetIsSubApprovedUsers();
    IQueryable<AaugUserGetDto> GetApprovedAaugUsers();
    IQueryable<AaugUserGetDto> GetSubscribedNotSubApprovedUsers();
    IQueryable<AaugUserFullGetDto> GetFullUserInfoByUserId(int Id);
    IQueryable<AaugUser> GetAaugUserById(int id);
    IQueryable<AaugUser> GetFullUserInfoByUserIdWithTracking(int Id);
    IQueryable<AaugUserGetDto> GetUserByGuId(string guId);
    IQueryable<AaugUserGetDto> GetByUserName(string Username);
    Task<AaugUser> DeleteUserAsync(int aaugUserId);
    IQueryable<AaugUserGetDto> GetNotApprovedAaugUsers();
}
