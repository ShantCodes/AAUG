using AAUG.DomainModels.Models.Tables.General;

namespace AAUG.DataAccess.Interfaces.General
{
    public interface INewsRepository
    {
        Task<List<News>> GetAllNews();
    }
}