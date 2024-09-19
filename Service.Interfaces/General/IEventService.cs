using AAUG.DomainModels.Dtos;
using AAUG.DomainModels.ViewModels;

namespace AAUG.Service.Interfaces.General;

public interface IEventService
{
    Task<IEnumerable<EventGetViewModel>> GetAllEventsAsync(int pageNumber, int pageSize = 4);
    Task<IEnumerable<DateOnly>> GetReservedEventDatesAsync();
    Task<EventInsertDto> InsertEventAsync(EventInsertViewModel inputEntity);
    Task<EventEditViewModel> EditEventAsync(EventEditViewModel inputEntity);
    Task<bool> ApproveEvent(int eventId, bool isApproved);
    Task<IEnumerable<EventGetViewModel>> SearchEventAsync(string keyWord);
    #region likes
    Task<IEnumerable<EventLikeGetViewModel>> GetEventLikesAsync(int eventId);
    Task<bool> CheckIfLiked(int eventId);
    Task<bool> LikeEventAsync(int eventId);
    #endregion

    #region  Admins
    Task<IEnumerable<EventGetViewModel>> GetAllEventsForAdmins();
    Task<IEnumerable<EventGetViewModel>> GetAllNotApprovedEventsForAdmins();
    Task<bool> DeleteEventAsync(int eventId);
    #endregion
}
