using AAUG.DomainModels.Dtos;
using AAUG.DomainModels.Models.Tables.General;

namespace AAUG.DataAccess.Interfaces.General;

public interface IEventRepository
{
    Task<List<Event>> GetEventsAsync();
    IQueryable<Event> GetEvent(int id);
    IQueryable<EventGetDto> GetEventAsync(int id);
    Task<Event> FirstAsync(int Id);
    Task<Event> AddAsync(EventInsertDto insertEntity);
    Task<List<Event>> SearchEventAsync(string keyWord);
    IQueryable<EventGetDto> GetEvents();

    #region Admins
    Task<List<Event>> GetNotApprovedEventsForAdmins();
    IQueryable<EventGetDto> GetAllEventsForAdmins();
    Task<Event> DeleteEventAsync(int eventId);
    #endregion
}
