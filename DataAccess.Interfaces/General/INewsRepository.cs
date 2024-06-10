using AAUG.DomainModels;
using AAUG.DomainModels.Models.Tables.General;

namespace AAUG.DataAccess.Interfaces.General
{
    public interface INewsRepository
    {
        Task<News> FirstAsync(int id);
        IQueryable<NewsForInsertDto> GetAllNews();
        Task<News> InsertNews(NewsForInsertDto inputEntity);
        Task<News> DeleteNewsAsync(int id);
        
    }
}