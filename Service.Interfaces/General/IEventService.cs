using AAUG.DomainModels.Dtos;
using AAUG.DomainModels.ViewModels;

namespace AAUG.Service.Interfaces.General;

public interface IEventService
{
    Task<IEnumerable<EventGetViewModel>> GetAllEventsAsync();
    Task<EventInsertDto> InsertEventAsync(EventInsertViewModel inputEntity);
    Task<bool> ApproveEvent(int eventId, bool isApproved);
    Task<IEnumerable<EventGetViewModel>> SearchEventAsync(string keyWord);

    #region  Admins
    Task<IEnumerable<EventGetViewModel>> GetAllEventsForAdmins();
    Task<IEnumerable<EventGetViewModel>> GetAllNotApprovedEventsForAdmins();
    Task<bool> DeleteEventAsync(int eventId);
    #endregion
}
