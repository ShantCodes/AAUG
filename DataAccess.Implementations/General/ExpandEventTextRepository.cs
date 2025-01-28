using AAUG.DataAccess.EntityRepository;
using AAUG.DataAccess.Implementations.UnitOfWork;
using AAUG.DataAccess.Interfaces.General;
using AAUG.DomainModels.Dtos;
using AAUG.DomainModels.Models.Tables.General;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AAUG.DataAccess.Implementations.General;

public class ExpandEventTextRepository : EntityRepository<ExpandEventText>, IExpandEventTextRepository
{
    private readonly IMapper mapper;
    private readonly IAaugUnitOfWork unitOfWork;
    public ExpandEventTextRepository(IAaugUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.Context)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public IQueryable<EventDetailsGetDto> GetEventDetailsById(int eventId)
    {
        return mapper.ProjectTo<EventDetailsGetDto>(
            GetData(a => a.EventId == eventId)
            .Include(a => a.ExpandEventFiles)
        );
    }

    public IQueryable<EventDetailsGetDto> GetById(int Id)
    {
        return mapper.ProjectTo<EventDetailsGetDto>(
            GetData(a => a.Id == Id)
            .Include(a => a.ExpandEventFiles)
        );
    }
    public IQueryable<ExpandEventText> GetByIdTracking(int Id)
    {
        return
            GetData(a => a.Id == Id)
            .Include(a => a.ExpandEventFiles);
    }

    public Task<List<ExpandEventText>> InsertDetailsAsync(IEnumerable<EventDetailsTextInsertDto> insertEntity)
    {
        return AddAsync(mapper.Map<List<ExpandEventText>>(insertEntity));
    }
}