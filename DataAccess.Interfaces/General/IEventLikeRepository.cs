using AAUG.DomainModels.Dtos;
using AAUG.DomainModels.Models.Tables.General;

namespace AAUG.DataAccess.Interfaces.General;

public interface IEventLikeRepository
{
    IQueryable<EventLikeGetDto> GetEventLikes(int eventId);
    IQueryable<EventLikeGetDto> GetUserEventLike(int aaugUserId, int eventId);
    IQueryable<EventLike> GetLikeWithTracking(int id);
    Task<EventLike> InsertLikeAsync(EventLikeInsertDto insertEntity);
    IQueryable<EventLikeGetDto> CheckIfLiked(int eventId, int aaugUserId);
    void DeleteLike(EventLikeDeleteDto inputEntity);
}