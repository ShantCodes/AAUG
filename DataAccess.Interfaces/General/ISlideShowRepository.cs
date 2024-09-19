using AAUG.DomainModels.Dtos;

namespace AAUG.DataAccess.Interfaces.General;

public interface ISlideShowRepository
{
    IQueryable<SlideShowGetDto> GetSlideShows();
}