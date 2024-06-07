
using AAUG.DomainModels.ViewModels;
namespace AAUG.Service.Interfaces.General
{
    public interface INewsService
    {
        Task<IEnumerable<NewsForInsertViewModel>> GetNewsAsync();
        Task<NewsForInsertViewModel> InsertNewsAsync(NewsForInsertViewModel inputEntity);
    }
}