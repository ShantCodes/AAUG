using AAUG.DataAccess.Implementations;
using AAUG.DomainModels.Models.Tables.General;
using AAUG.DomainModels.ViewModels;
using AAUG.Service.Interfaces.General;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AAUG.Service.General
{
    public class NewsService : INewsService
    {
        private IAaugUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public NewsService(IAaugUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        public async Task<IEnumerable<NewsForInsertViewModel>> GetNewsAsync()
        {
            var x = await unitOfWork.NewsRepository.GetAllNews().ToListAsync();
            var y = mapper.Map<IEnumerable<NewsForInsertViewModel>>(x);
            return y;
        }

        public async Task<NewsForInsertViewModel> InsertNewsAsync(NewsForInsertViewModel inputEntity)
        {
            await unitOfWork.NewsRepository.InsertNews(
                   mapper.Map<NewsForInsertDto>(inputEntity));


            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommitTransactionAsync();

            return inputEntity;
        }
    }

}