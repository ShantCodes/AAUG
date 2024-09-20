using AAUG.DataAccess.EntityRepository;
using AAUG.DataAccess.Implementations.UnitOfWork;
using AAUG.DataAccess.Interfaces.Media;
using AAUG.DomainModels;
using AAUG.DomainModels.Dtos.Media;
using AAUG.DomainModels.Enums;
using AutoMapper;

namespace AAUG.DataAccess.Implementations.Media;

public class MediaFolderRepository : EntityRepository<MediaFolder>, IMediaFolderRepository
{
    private readonly IMapper mapper;
    private IAaugUnitOfWork unitOfWork;
    public MediaFolderRepository(IAaugUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.Context)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public IQueryable<MediaFolderPathDto> GetMediaFolderPath()
    {
        return mapper.ProjectTo<MediaFolderPathDto>(
            GetData()
        );
    }

    public IQueryable<MediaFolderPathDto> GetEventsFolder()
    {
        return mapper.ProjectTo<MediaFolderPathDto>(
            GetData(a => a.MediaPathTypeId == MediaPaths.EventsFolder));
    }
    public IQueryable<MediaFolderPathDto> GetNewsFolder()
    {
        return mapper.ProjectTo<MediaFolderPathDto>(
            GetData(a => a.MediaPathTypeId == MediaPaths.NewsFolder)
        );
    }
    public IQueryable<MediaFolderPathDto> GetCampsFolder()
    {
        return mapper.ProjectTo<MediaFolderPathDto>(GetData(a => a.MediaPathTypeId == MediaPaths.CampsFolder));
    }
    public IQueryable<MediaFolderPathDto> GetProfileFolder()
    {
        return mapper.ProjectTo<MediaFolderPathDto>(GetData(a => a.MediaPathTypeId == MediaPaths.ProfileFolder));
    }
    public IQueryable<MediaFolderPathDto> GetSlideShowFolder()
    {
        return mapper.ProjectTo<MediaFolderPathDto>(GetData(a => a.MediaPathTypeId == MediaPaths.SlideShowFolder));
    }
}