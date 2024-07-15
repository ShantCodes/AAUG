using AAUG.DomainModels.Dtos;
using AAUG.DomainModels.ViewModels;

namespace AAUG.Service.Interfaces.General;

public interface IEventService
{
    Task<IEnumerable<EventGetViewModel>> GetAllEventsAsync();
    Task<EventInsertDto> InsertEventAsync(EventInsertViewModel inputEntity);
    Task<bool> ApproveEvent(int eventId, bool isApproved);

    #region  Admins
    Task<IEnumerable<EventGetViewModel>> GetAllEventsForAdmins();
    Task<IEnumerable<EventGetViewModel>> GetAllNotApprovedEventsForAdmins();
    #endregion
}
