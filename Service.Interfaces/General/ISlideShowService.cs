using AAUG.DomainModels.ViewModels;

namespace AAUG.Service.Interfaces.General;

public interface ISlideShowService
{
    Task<IEnumerable<SlideShowGetViewModel>> GetSlideShowsAsync();
}