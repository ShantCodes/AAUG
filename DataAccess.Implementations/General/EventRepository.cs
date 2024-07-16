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
    public Task<List<Event>> GetAllEventsForAdmins()
    {
        return GetData().ToListAsync();
    }
    public Task<Event> DeleteEventAsync(int eventId)
    {
        return DeleteAsync(eventId);
    }
    #endregion
}
