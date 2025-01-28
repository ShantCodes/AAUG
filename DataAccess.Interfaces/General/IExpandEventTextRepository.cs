using AAUG.DomainModels.Dtos;
using AAUG.DomainModels.Models.Tables.General;

namespace AAUG.DataAccess.Interfaces.General;

public interface IExpandEventTextRepository
{
    IQueryable<EventDetailsGetDto> GetEventDetailsById(int eventId);
    Task<List<ExpandEventText>> InsertDetailsAsync(IEnumerable<EventDetailsTextInsertDto> insertEntity);
    IQueryable<EventDetailsGetDto> GetById(int Id);
    IQueryable<ExpandEventText> GetByIdTracking(int Id);
}