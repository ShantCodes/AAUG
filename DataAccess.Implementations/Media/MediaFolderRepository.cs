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
        var x = GetData(a => a.MediaPathTypeId == MediaPaths.EventsFolder);
        var test = mapper.ProjectTo<MediaFolderPathDto>(x);
        return test;
    }
    public IQueryable<MediaFolderPathDto> GetCampsFolder()
    {
        return mapper.ProjectTo<MediaFolderPathDto>(GetData(a => a.MediaPathTypeId == MediaPaths.CampsFolder));
    }
    public IQueryable<MediaFolderPathDto> GetProfileFolder()
    {
        return mapper.ProjectTo<MediaFolderPathDto>(GetData(a => a.MediaPathTypeId == MediaPaths.ProfileFolder));
    }
}