using AAUG.DomainModels.Models.Tables.General;

namespace AAUG.Service.Interfaces.General
{
    public interface INewsService
    {
        Task<List<News>> GetNewsAsync();
    }
}