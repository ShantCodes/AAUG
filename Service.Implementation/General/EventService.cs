using AAUG.DataAccess.Implementations.UnitOfWork;
using AAUG.DomainModels.Dtos;
using AAUG.DomainModels.ViewModels;
using AAUG.Service.Interfaces.General;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AAUG.Service.Implementations.General;

public class EventService : IEventService
{
    private readonly IMapper mapper;
    private IAaugUnitOfWork unitOfWork;
    public EventService(IAaugUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<EventGetViewModel>> GetAllEventsAsync()
    {
        return mapper.Map<List<EventGetViewModel>>(
            await unitOfWork.EventRepository.GetEventsAsync());
    }

    public async Task<EventInsertDto> InsertEventAsync(EventInsertViewModel inputEntity)
    {
        var data = mapper.Map<EventInsertDto>(inputEntity);
        data.IsApproved = false;
        data.HasHappened = false;

        await unitOfWork.EventRepository.AddAsync(data);
        await unitOfWork.SaveChangesAsync();
        await unitOfWork.CommitTransactionAsync();

        return data;
    }
    public async Task<bool> ApproveEvent(int eventId, bool isApproved)
    {
        var data = await unitOfWork.EventRepository.FirstAsync(eventId);
        data.IsApproved = isApproved;

        await unitOfWork.SaveChangesAsync();
        await unitOfWork.CommitTransactionAsync();

        return true;
    }

    #region admins
    public async Task<IEnumerable<EventGetViewModel>> GetAllEventsForAdmins()
    {
        return mapper.Map<IEnumerable<EventGetViewModel>>(
            await unitOfWork.EventRepository.GetAllEventsForAdmins());
    }

    public async Task<IEnumerable<EventGetViewModel>> GetAllNotApprovedEventsForAdmins()
    {
        return mapper.Map<IEnumerable<EventGetViewModel>>(
            await unitOfWork.EventRepository.GetNotApprovedEventsForAdmins());
    }
    #endregion

}
