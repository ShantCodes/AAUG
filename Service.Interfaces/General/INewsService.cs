
using AAUG.DomainModels.ViewModels;
namespace AAUG.Service.Interfaces.General
{
    public interface INewsService
    {
        Task<IEnumerable<NewsForShowViewModel>> GetNewsByTitleAsync(string newsTitle);
        Task<IEnumerable<NewsForShowViewModel>> GetNewsAsync();
        Task<NewsForShowViewModel> InsertNewsAsync(NewsForInsertViewModel inputEntity);
        Task<NewsForShowViewModel> DeleteNewsByIdAsync(int id);
        Task<NewsForShowViewModel> GetNewsById(int id);
        Task<NewsForEditViewModel> EditNewsAsync(NewsForEditViewModel inputEntity);
        #region teaser
        Task<IEnumerable<NewTeaserGetViewModel>> GetNewsTeasersAsync();
        #endregion
    }
}