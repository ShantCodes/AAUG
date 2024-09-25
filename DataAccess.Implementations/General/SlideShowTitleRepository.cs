using AAUG.DataAccess.EntityRepository;
using AAUG.DataAccess.Implementations.UnitOfWork;
using AAUG.DataAccess.Interfaces.General;
using AAUG.DomainModels.Dtos;
using AAUG.DomainModels.Models.Tables.General;
using AutoMapper;

namespace AAUG.DataAccess.Implementations.General;

public class SlideShowTitleRepository : EntityRepository<SlideShowTitle>, ISlideShowTitleRepository
{
    private readonly IAaugUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    public SlideShowTitleRepository(IAaugUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.Context)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
    }

    public Task<SlideShowTitle> AddSlideShowTitle(SlideShowTitleInsertDto inputEntity)
    {
        return AddAsync(mapper.Map<SlideShowTitle>(inputEntity));
    }

    public IQueryable<SlideshowTitleGetDto> GetData()
    {
        return mapper.ProjectTo<SlideshowTitleGetDto>(
            GetData()
        );
    }

    public Task<bool> DeleteAsync(int id)
    {
        return DeleteAsync(id);
    }
}
