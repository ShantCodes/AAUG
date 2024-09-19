using AAUG.DataAccess.EntityRepository;
using AAUG.DataAccess.Implementations.UnitOfWork;
using AAUG.DataAccess.Interfaces.General;
using AAUG.DomainModels.Dtos;
using AAUG.DomainModels.Models.Tables.General;
using AutoMapper;

namespace AAUG.DataAccess.Implementations.General;

public class SlideShowRepository : EntityRepository<SlideShow>, ISlideShowRepository
{
    private readonly IAaugUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    public SlideShowRepository(IAaugUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.Context)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public IQueryable<SlideShowGetDto> GetSlideShows()
    {
        return mapper.ProjectTo<SlideShowGetDto>(
            GetData()
        );
    }
}