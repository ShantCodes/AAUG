using AAUG.DataAccess.EntityRepository;
using AAUG.DataAccess.Implementations.UnitOfWork;
using AAUG.DataAccess.Interfaces.General;
using AAUG.DomainModels.Dtos;
using AAUG.DomainModels.Models.Tables.General;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AAUG.DataAccess.Implementations.General;

public class EventLikeRepository : EntityRepository<EventLike>, IEventLikeRepository
{
    private readonly IMapper mapper;
    private IAaugUnitOfWork unitOfWork;
    public EventLikeRepository(IAaugUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.Context)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
    }

    public IQueryable<EventLikeGetDto> GetEventLikes(int eventId)
    {
        return mapper.ProjectTo<EventLikeGetDto>(
            GetData(a => a.EventId == eventId)
                .Include(a => a.User)
        );
    }
    public IQueryable<EventLikeGetDto> GetUserEventLike(int aaugUserId, int eventId)
    {
        return mapper.ProjectTo<EventLikeGetDto>(GetData(a => a.EventId == eventId && a.UserId == aaugUserId));
    }

    public IQueryable<EventLike> GetLikeWithTracking(int id)
    {
        return GetData(a => a.Id == id);
    }

    public Task<EventLike> InsertLikeAsync(EventLikeInsertDto insertEntity)
    {
        return AddAsync(mapper.Map<EventLike>(insertEntity));
    }
    public void DeleteLike(EventLikeDeleteDto inputEntity)
    {
        Delete(mapper.Map<EventLike>(inputEntity));
    }
}