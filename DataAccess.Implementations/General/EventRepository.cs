using AAUG.DataAccess.EntityRepository;
using AAUG.DataAccess.Implementations.UnitOfWork;
using AAUG.DataAccess.Interfaces.General;
using AAUG.DomainModels.Dtos;
using AAUG.DomainModels.Models.Tables.General;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AAUG.DataAccess.Implementations.General;

public class EventRepository : EntityRepository<Event>, IEventRepository
{
    private readonly IAaugUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    public EventRepository(IAaugUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.Context)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public Task<List<Event>> GetEventsAsync()
    {
        return GetData(a => a.IsApproved).ToListAsync();
    }

    public IQueryable<EventGetDto> GetEventAsync(int id)
    {
        return mapper.ProjectTo<EventGetDto>(
            GetData(a => a.Id == id)
        );
    }

    public IQueryable<Event> GetEvent(int id)
    {
        return GetData(a => a.Id == id);
    }

    public IQueryable<DateOnly> GetReservedEventDates()
    {
        return GetData().Select(a => a.EventDate);
    }

    public IQueryable<EventGetDto> GetEvents()
    {
        var data = GetData(a => a.IsApproved)
                        .Include(a => a.ThumbnailFile);
        return mapper.ProjectTo<EventGetDto>(data);

    }

    public Task<Event> FirstAsync(int Id)
    {
        return GetData(a => a.Id == Id).FirstAsync();
    }

    public Task<Event> AddAsync(EventInsertDto insertEntity)
    {
        return AddAsync(mapper.Map<Event>(insertEntity));
    }

    public Task<List<Event>> SearchEventAsync(string keyWord)
    {
        return GetData(a => a.EventTitle.Contains(keyWord)).ToListAsync();
    }

    #region Admins
    public Task<List<Event>> GetNotApprovedEventsForAdmins()
    {
        return GetData(a => !a.IsApproved).ToListAsync();
    }
    public IQueryable<EventGetDto> GetAllEventsForAdmins()
    {
        return mapper.ProjectTo<EventGetDto>(GetData());
    }
    public Task<Event> DeleteEventAsync(int eventId)
    {
        return DeleteAsync(eventId);
    }
    #endregion
}
