using AAUG.DataAccess.Implementations.UnitOfWork;
using AAUG.DomainModels.Dtos;
using AAUG.DomainModels.ViewModels;
using AAUG.Service.Implementations.Media;
using AAUG.Service.Interfaces.General;
using AAUG.Service.Interfaces.Media;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace AAUG.Service.Implementations.General;

public class SlideShowService : ISlideShowService
{
    private readonly IAaugUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly IMediaFileService mediaFIleService;
    public SlideShowService(IAaugUnitOfWork unitOfWork, IMapper mapper, IMediaFileService mediaFIleService)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
        this.mediaFIleService = mediaFIleService;
    }

    public async Task<IEnumerable<SlideShowGetViewModel>> GetSlideShowsAsync()
    {
        return mapper.Map<IEnumerable<SlideShowGetViewModel>>(
            await unitOfWork.SlideShowRepository.GetSlideShows().ToListAsync()
        );
    }

    public async Task<IEnumerable<SlideShowGetViewModel>> GetAllSlideShowsForAdminsAsync()
    {
        return mapper.Map<IEnumerable<SlideShowGetViewModel>>(
            await unitOfWork.SlideShowRepository.GetSlideShowsForAdmins().ToListAsync()
        );
    }

    //one title with many slideshows
    public async Task<SlideShowTitleGetViewModel> GetSlideShowsWithTitleAsync()
    {
        var slideShows = mapper.Map<IEnumerable<SlideShowGetViewModel>>(
            await unitOfWork.SlideShowRepository.GetSlideShows().ToListAsync()
        );

        var x = await unitOfWork.SlideShowTitleRepository.GetDataAsync();
        var title = mapper.Map<SlideShowTitleGetViewModel>(x) ?? new SlideShowTitleGetViewModel();

        // Ensure SlideShowGetViewModels is initialized
        if (title.SlideShowGetViewModels == null)
        {
            title.SlideShowGetViewModels = new List<SlideShowGetViewModel>();
        }

        title.SlideShowGetViewModels.AddRange(slideShows.ToList());

        return title;
    }

    //selecting the slides
    public async Task<bool> SelectSlidesAsync(List<int> slideShowIds)
    {
        var existingSlides = await unitOfWork.SlideShowRepository.GetSlideShowTracking(slideShowIds).ToListAsync();
        foreach (var item in existingSlides)
        {
            if (item.IsActive)
            {
                item.IsActive = false;
            }
            else
            {
                item.IsActive = true;
            }
        }
        await unitOfWork.SaveChangesAsync();
        await unitOfWork.CommitTransactionAsync();

        return true;
    }


    public async Task<IEnumerable<SlideShowGetViewModel>> InsertSlideShowsAsync(List<SlideShowInsertViewModel> inputEntity)
    {
        return mapper.Map<IEnumerable<SlideShowGetViewModel>>(
            await unitOfWork.SlideShowRepository.InsertSlideShowAsync(
            mapper.Map<List<SlideShowInsertDto>>(inputEntity)
        ));
    }
    public async Task<IEnumerable<SlideShowGetViewModel>> InsertSlideShowsAsync(SlideShowInsertViewModel inputEntity)
    {
        var data = mapper.Map<SlideShowInsertDto>(inputEntity);

        var mediaFileDto = await mediaFIleService.InsertSlideShowMediaFileAsync(inputEntity.MediaFile);
        data.MediaFileId = mediaFileDto.Id;

        await unitOfWork.SlideShowRepository.InsertSlideShowAsync(data);

        await unitOfWork.SaveChangesAsync();
        await unitOfWork.CommitTransactionAsync();

        return mapper.Map<IEnumerable<SlideShowGetViewModel>>(unitOfWork.SlideShowRepository.GetSlideShows());

    }

    public async Task<bool> DeleteSlidesAsync(int slideId)
    {
        await unitOfWork.SlideShowRepository.DeleteSlideAsync(slideId);
        
        await unitOfWork.SaveChangesAsync();
        await unitOfWork.CommitTransactionAsync();

        return true;
    }
    #region slide show title
    public async Task<SlideShowTitleInsertViewModel> InsertSlideShowTitleAsync(SlideShowTitleInsertViewModel inputEntity)
    {
        var existingTitle = await unitOfWork.SlideShowTitleRepository.GetDataAsync();
        if (existingTitle != null)
        {
            await unitOfWork.SlideShowTitleRepository.DeleteTitleAsync(existingTitle.Id);
        }
        await unitOfWork.SlideShowTitleRepository.AddSlideShowTitle(
           mapper.Map<SlideShowTitleInsertDto>(inputEntity)
       );

        await unitOfWork.SaveChangesAsync();
        await unitOfWork.CommitTransactionAsync();

        return inputEntity;
    }
    #endregion
}