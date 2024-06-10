
using AAUG.DomainModels.ViewModels;
namespace AAUG.Service.Interfaces.General
{
    public interface INewsService
    {
        Task<IEnumerable<NewsForShowViewModel>> GetNewsAsync();
        Task<NewsForInsertViewModel> InsertNewsAsync(NewsForInsertViewModel inputEntity);
        Task<NewsForShowViewModel> DeleteNewsByIdAsync(int id);
        Task<NewsForEditViewModel> EditNewsAsync(NewsForEditViewModel inputEntity);
    }
}