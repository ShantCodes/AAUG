using AAUG.DataAccess.Implementations.UnitOfWork;
using AAUG.DomainModels.Dtos;
using AAUG.DomainModels.ViewModels;
using AAUG.Service.Implementations.Media;
using AAUG.Service.Interfaces.General;
using AAUG.Service.Interfaces.Media;
using AutoMapper;
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

    #region slide show title
    public async Task<SlideShowTitleInsertViewModel> InsertSlideShowTitleAsync(SlideShowTitleInsertViewModel inputEntity)
    {
        var existingTitle = await unitOfWork.SlideShowTitleRepository.GetData().FirstOrDefaultAsync();
        if (existingTitle != null)
        {
            await unitOfWork.SlideShowTitleRepository.DeleteAsync(existingTitle.Id);
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