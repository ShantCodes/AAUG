using AAUG.DomainModels.Dtos;
using AAUG.DomainModels.Models.Tables.General;

namespace AAUG.DataAccess.Interfaces.General;

public interface IEventRepository
{
    Task<List<Event>> GetEventsAsync();
    Task<Event> FirstAsync(int Id);
    Task<Event> AddAsync(EventInsertDto insertEntity);
    Task<List<Event>> SearchEventAsync(string keyWord);

    #region Admins
    Task<List<Event>> GetNotApprovedEventsForAdmins();
    Task<List<Event>> GetAllEventsForAdmins();
    Task<Event> DeleteEventAsync(int eventId);
    #endregion
}
