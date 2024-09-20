using AAUG.DomainModels.ViewModels;

namespace AAUG.Service.Interfaces.General;

public interface ISlideShowService
{
    Task<IEnumerable<SlideShowGetViewModel>> GetSlideShowsAsync();
    Task<IEnumerable<SlideShowGetViewModel>> InsertSlideShowsAsync(List<SlideShowInsertViewModel> inputEntity);
    Task<IEnumerable<SlideShowGetViewModel>> InsertSlideShowsAsync(SlideShowInsertViewModel inputEntity);
    #region slide show title
    Task<SlideShowTitleInsertViewModel> InsertSlideShowTitleAsync(SlideShowTitleInsertViewModel inputEntity);
    #endregion
    
}