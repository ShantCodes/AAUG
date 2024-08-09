using AAUG.DataAccess.Implementations.UnitOfWork;
using AAUG.DomainModels.Dtos;
using AAUG.DomainModels.Enums;
using AAUG.DomainModels.ViewModels;
using AAUG.Service.Interfaces;
using AAUG.Service.Interfaces.General;
using AAUG.Service.Interfaces.Media;
using AutoMapper;
using Azure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AAUG.Service.Implementations.General;

public class EventService : IEventService
{
    #region injection
    private readonly IMapper mapper;
    private IAaugUnitOfWork unitOfWork;
    private readonly IMediaFileService mediaFileService;
    private readonly ITokenService tokenService;
    private readonly IUserService userService;
    private readonly IAaugUserService aaugUserService;
    public EventService(IAaugUnitOfWork unitOfWork,
                        IMapper mapper,
                        IMediaFileService mediaFileService,
                        ITokenService tokenService,
                        IUserService userService,
                        IAaugUserService aaugUserService)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.mediaFileService = mediaFileService;
        this.tokenService = tokenService;
        this.userService = userService;
        this.aaugUserService = aaugUserService;
    }

    #endregion

    public async Task<IEnumerable<EventGetViewModel>> GetAllEventsAsync()
    {
        var data = mapper.Map<List<EventGetViewModel>>(
            await unitOfWork.EventRepository.GetEvents().ToListAsync());
        foreach (var item in data)
        {
            if (item.thumbnailFile != null)
                item.thumbnailFile.FolderPathTypeId = MediaPaths.EventsFolder;
        }

        return data;
    }

    public async Task<EventInsertDto> InsertEventAsync(EventInsertViewModel inputEntity)
    {
        var data = mapper.Map<EventInsertDto>(inputEntity);
        data.IsApproved = false;
        data.HasHappened = false;

        var mediaFileDto = await mediaFileService.InsertEventsMediaFileAsync(inputEntity.ThumbNailFile);
        data.ThumbNailFileId = mediaFileDto.Id;
        await unitOfWork.EventRepository.AddAsync(data);
        await unitOfWork.SaveChangesAsync();
        await unitOfWork.CommitTransactionAsync();

        return data;
    }

    public async Task<EventEditViewModel> EditEventAsync(EventEditViewModel inputEntity)
    {
        var existingData = await unitOfWork.EventRepository.GetEvent(inputEntity.Id).FirstOrDefaultAsync();
        if (existingData == null)
            throw new Exception("the Event not found or the data is empty");
        var eventdto = mapper.Map<EventEditDto>(inputEntity);
        mapper.Map(eventdto, existingData);

        if (inputEntity.ThumbNailFile != null)
        {
            var newMediaFileDto = await mediaFileService.InsertEventsMediaFileAsync(inputEntity.ThumbNailFile, existingData.ThumbNailFileId);
            existingData.ThumbNailFileId = newMediaFileDto.Id;
        }
        await unitOfWork.SaveChangesAsync();
        await unitOfWork.CommitTransactionAsync();

        return inputEntity;
    }

    public async Task<IEnumerable<EventGetViewModel>> SearchEventAsync(string keyWord)
    {
        return mapper.Map<IEnumerable<EventGetViewModel>>(
            await unitOfWork.EventRepository.SearchEventAsync(keyWord)
        );
    }
    #region likes
    public async Task<bool> LikeEventAsync(int eventId)
    {
        var aaugUser = await tokenService.GetAaugUserFromToken();

        var existingLike = await unitOfWork.EventLikeRepository.GetUserEventLike(aaugUser.Id, eventId).FirstOrDefaultAsync();
        var existingEvent = await unitOfWork.EventRepository.GetEvent(eventId).FirstOrDefaultAsync();

        if (existingEvent == null)
            throw new Exception("Event not found");
            
        if (existingLike != null)
        {
            var deleteEntity = mapper.Map<EventLikeDeleteDto>(existingLike);
            unitOfWork.EventLikeRepository.DeleteLike(deleteEntity);
            existingEvent.LikeCount -= 1;

            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommitTransactionAsync();

            return true;
        }
        var eventLikeDto = new EventLikeInsertDto
        {
            EventId = eventId,
            UserId = aaugUser.Id
        };
        existingEvent.LikeCount++;
        await unitOfWork.EventLikeRepository.InsertLikeAsync(eventLikeDto);

        await unitOfWork.SaveChangesAsync();
        await unitOfWork.CommitTransactionAsync();

        return true;
    }
    public async Task<IEnumerable<EventLikeGetViewModel>> GetEventLikesAsync(int eventId)
    {
        var data = await unitOfWork.EventLikeRepository.GetEventLikes(eventId).ToListAsync();
        var result = mapper.Map<IEnumerable<EventLikeGetViewModel>>(data);

        foreach (var item in result)
        {
            var correspondingUser = data.FirstOrDefault(d => d.User.Id == item.UserId)?.User;
            item.User = mapper.Map<AaugUserGetViewModel>(correspondingUser);
        }
        return result;
    }
    #endregion

    #region admins
    public async Task<bool> ApproveEvent(int eventId, bool isApproved)
    {
        var data = await unitOfWork.EventRepository.FirstAsync(eventId);
        data.IsApproved = isApproved;

        await unitOfWork.SaveChangesAsync();
        await unitOfWork.CommitTransactionAsync();

        return true;
    }

    public async Task<IEnumerable<EventGetViewModel>> GetAllEventsForAdmins()
    {
        var data = mapper.Map<List<EventGetViewModel>>(
            await unitOfWork.EventRepository.GetEvents().ToListAsync());
        foreach (var item in data)
        {
            if (item.thumbnailFile != null)
                item.thumbnailFile.FolderPathTypeId = MediaPaths.EventsFolder;
        }

        return data;
    }

    public async Task<IEnumerable<EventGetViewModel>> GetAllNotApprovedEventsForAdmins()
    {
        return mapper.Map<IEnumerable<EventGetViewModel>>(
            await unitOfWork.EventRepository.GetNotApprovedEventsForAdmins());
    }

    public async Task<bool> DeleteEventAsync(int eventId)
    {
        var data = await unitOfWork.EventRepository.FirstAsync(eventId);
        if (data == null)
        {
            return false;
        }
        await unitOfWork.EventRepository.DeleteEventAsync(eventId);

        await unitOfWork.SaveChangesAsync();
        await unitOfWork.CommitTransactionAsync();
        return true;
    }

    #endregion

}
