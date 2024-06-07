using AAUG.DomainModels;
using AAUG.DomainModels.Models.Tables.General;

namespace AAUG.DataAccess.Interfaces.General
{
    public interface INewsRepository
    {
        IQueryable<NewsForInsertDto> GetAllNews();
        Task<News> InsertNews(NewsForInsertDto inputEntity);
        
    }
}