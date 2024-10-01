using AAUG.DomainModels.Dtos;
using AAUG.DomainModels.Models.Tables.General;

namespace AAUG.DataAccess.Interfaces.General;

public interface ISlideShowRepository
{
    IQueryable<SlideShowGetDto> GetSlideShows();
    IQueryable<SlideShowGetDto> GetSlideShowsForAdmins();
    Task<List<SlideShow>> InsertSlideShowAsync(List<SlideShowInsertDto> inputEntity);
    Task<SlideShow> InsertSlideShowAsync(SlideShowInsertDto inputEntity);
    IQueryable<SlideShow> GetSlideShowTracking(List<int> slideIds);
}