using AAUG.DataAccess.Implementations.UnitOfWork;
using AAUG.DomainModels.ViewModels;
using AAUG.Service.Interfaces.General;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AAUG.Service.Implementations.General;

public class SlideShowService : ISlideShowService
{
    private readonly IAaugUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    public SlideShowService(IAaugUnitOfWork unitOfWork, IMapper mapper)
    {
        this.mapper = mapper;
        this.unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<SlideShowGetViewModel>> GetSlideShowsAsync()
    {
        return mapper.Map<IEnumerable<SlideShowGetViewModel>>(
            await unitOfWork.SlideShowRepository.GetSlideShows().ToListAsync()
        );
    }
}