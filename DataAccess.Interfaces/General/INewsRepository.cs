using AAUG.DomainModels;
using AAUG.DomainModels.Models.Tables.General;

namespace AAUG.DataAccess.Interfaces.General
{
    public interface INewsRepository
    {
        IQueryable<NewsGetDto> GetNewsByTitle(string newsTitle);
        Task<News> FirstAsync(int id);
        IQueryable<NewsGetDto> GetAllNews();
        Task<News> InsertNews(NewsForInsertDto inputEntity);
        Task<News> DeleteNewsAsync(int id);
        IQueryable<NewsGetDto> Get4LastNews();
        IQueryable<NewsGetDto> GetNewsById(int id);


    }
}