using AAUG.DataAccess.EntityRepository;
using AAUG.DataAccess.Implementations.UnitOfWork;
using AAUG.DataAccess.Interfaces.Media;
using AAUG.DomainModels.Dtos.Media;
using AAUG.DomainModels.Models.Tables.General;
using AutoMapper;

namespace AAUG.DataAccess.Implementations.Media;

public class MediaFileRepository : EntityRepository<MediaFile>, IMediaFileRepository
{
    private readonly IMapper mapper;
    private readonly IAaugUnitOfWork unitOfWork;
    public MediaFileRepository(IAaugUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.Context)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public Task<MediaFile> AddMediaFileAsync(MediaFileInsertDto insertEntity)
    {
        return AddAsync(mapper.Map<MediaFile>(insertEntity));
    }

    public IQueryable<MediaFileGetDto> GetMediaFile(int fileId)
    {
        return mapper.ProjectTo<MediaFileGetDto>(
            GetData(a => a.Id == fileId)
        );
    }
}
