using AAUG.DomainModels.ViewModels;

namespace AAUG.Service.Interfaces.General;

public interface ISlideShowService
{

    #region slideshow with one title
    Task<SlideShowTitleGetViewModel> GetSlideShowsWithTitleAsync();
    #endregion
    Task<IEnumerable<SlideShowGetViewModel>> GetSlideShowsAsync();
    Task<IEnumerable<SlideShowGetViewModel>> GetAllSlideShowsForAdminsAsync();
    Task<IEnumerable<SlideShowGetViewModel>> InsertSlideShowsAsync(List<SlideShowInsertViewModel> inputEntity);
    Task<IEnumerable<SlideShowGetViewModel>> InsertSlideShowsAsync(SlideShowInsertViewModel inputEntity);
    Task<bool> SelectSlidesAsync(List<int> slideShowIds);
    Task<bool> DeleteSlidesAsync(int slideIds);
    #region slide show title
    Task<SlideShowTitleInsertViewModel> InsertSlideShowTitleAsync(SlideShowTitleInsertViewModel inputEntity);
    #endregion

}