using System.Diagnostics.CodeAnalysis;
using AAUG.DataAccess.Implementations;
using AAUG.DataAccess.Implementations.UnitOfWork;
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
        public async Task<IEnumerable<NewsForShowViewModel>> GetNewsAsync()
        {
            return mapper.Map<IEnumerable<NewsForShowViewModel>>(
                await unitOfWork.NewsRepository
                .GetAllNews()
                .ToListAsync());
        }
        public async Task<NewsForInsertViewModel> InsertNewsAsync(NewsForInsertViewModel inputEntity)
        {
            await unitOfWork.NewsRepository.InsertNews(
                   mapper.Map<NewsForInsertDto>(inputEntity));

            await unitOfWork.CommitTransactionAsync();
            return inputEntity;
        }
        public async Task<NewsForEditViewModel> EditNewsAsync(NewsForEditViewModel inputEntity)
        {
            var entity = await unitOfWork.NewsRepository.FirstAsync(inputEntity.Id);
            mapper.Map(inputEntity, entity);

            await unitOfWork.CommitTransactionAsync();
            return inputEntity;
        }
        public async Task<NewsForShowViewModel> DeleteNewsByIdAsync(int id)
        {
            var result = await unitOfWork.NewsRepository.DeleteNewsAsync(id);
            await unitOfWork.CommitTransactionAsync();

            return mapper.Map<NewsForShowViewModel>(result);
        }

        #region Methods and Extensions
        #endregion
    }

}