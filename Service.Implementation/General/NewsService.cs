using AAUG.DataAccess.Implementations;
using AAUG.DomainModels.Models.Tables.General;
using AAUG.Service.Interfaces.General;
using AAUG.Service.ViewModels;

namespace AAUG.Service.General
{
    public class NewsService : INewsService
    {
        private IAaugUnitOfWork unitOfWork;
        public NewsService(IAaugUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<List<News>> GetNewsAsync()
        {
            return await unitOfWork.NewsRepository.GetAllNews();
        }
    }
}