using System.Diagnostics.CodeAnalysis;
using AAUG.DataAccess.Implementations;
using AAUG.DataAccess.Implementations.UnitOfWork;
using AAUG.DomainModels.Dtos.Media;
using AAUG.DomainModels.Models.Tables.General;
using AAUG.DomainModels.ViewModels;
using AAUG.Service.Implementations;
using AAUG.Service.Interfaces;
using AAUG.Service.Interfaces.General;
using AAUG.Service.Interfaces.Media;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AAUG.Service.General
{
    public class NewsService : INewsService
    {
        private IAaugUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly ITokenService tokenService;
        private readonly IMediaFileService mediaFileService;
        public NewsService(IAaugUnitOfWork unitOfWork, IMapper mapper, ITokenService tokenService, IMediaFileService mediaFileService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.tokenService = tokenService;
            this.mediaFileService = mediaFileService;
        }

        public async Task<IEnumerable<NewsForShowViewModel>> GetNewsByTitleAsync(string newsTitle)
        {
            return mapper.Map<IEnumerable<NewsForShowViewModel>>(
                await unitOfWork.NewsRepository.GetNewsByTitle(newsTitle).ToListAsync()
            );
        }
        public async Task<IEnumerable<NewsForShowViewModel>> GetNewsAsync()
        {
            return mapper.Map<IEnumerable<NewsForShowViewModel>>(
                await unitOfWork.NewsRepository
                .GetAllNews()
                .ToListAsync());
        }
        public async Task<NewsForShowViewModel> InsertNewsAsync(NewsForInsertViewModel inputEntity)
        {
            var aaugUser = await tokenService.GetAaugUserFromToken();
            var entityDto = mapper.Map<NewsForInsertDto>(inputEntity);

            if (inputEntity.NewsFile != null)
            {
                var newsFileWithId = await mediaFileService.InsertNewsMediaFileAsync(inputEntity.NewsFile);
                entityDto.NewsFileId = newsFileWithId.Id;
            }
            entityDto.CreatorUserId = aaugUser.Id;

            var news = await unitOfWork.NewsRepository.InsertNews(entityDto);
            await unitOfWork.SaveChangesAsync();

            var newViewModel = mapper.Map<NewsForShowViewModel>(news);
            await unitOfWork.CommitTransactionAsync();

            return newViewModel;
        }

        public async Task<NewsForEditViewModel> EditNewsAsync(NewsForEditViewModel inputEntity)
        {
            var aaugUser = await tokenService.GetAaugUserFromToken();
            if (aaugUser == null)
                throw new Exception("user not found");

            var entityDto = mapper.Map<NewsForEditDto>(inputEntity);
            entityDto.CreatorUserId = aaugUser.Id;

            var existingData = await unitOfWork.NewsRepository.FirstAsync(inputEntity.Id);
            if (existingData == null)
                throw new Exception("News not found");
            if (existingData.NewsFileId.HasValue)
            {
                entityDto.NewsFileId = existingData.NewsFileId;
            }
            mapper.Map(entityDto, existingData);
            if (inputEntity.NewsFile != null)
            {
                var newNewsFileSto = await mediaFileService.InsertNewsMediaFileAsync(inputEntity.NewsFile, existingData.NewsFileId);
            }

            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommitTransactionAsync();
            return inputEntity;
        }
        public async Task<NewsForShowViewModel> DeleteNewsByIdAsync(int id)
        {
            var result = await unitOfWork.NewsRepository.DeleteNewsAsync(id);

            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommitTransactionAsync();

            return mapper.Map<NewsForShowViewModel>(result);
        }

        public async Task<NewsForShowViewModel> GetNewsById(int id)
        {
            return mapper.Map<NewsForShowViewModel>(
                await unitOfWork.NewsRepository.GetNewsById(id).FirstOrDefaultAsync());

        }

        #region news teaser
        public async Task<IEnumerable<NewTeaserGetViewModel>> GetNewsTeasersAsync()
        {
            return mapper.Map<IEnumerable<NewTeaserGetViewModel>>(unitOfWork.NewsRepository.Get4LastNews());
        }
        #endregion

        #region Methods and Extensions
        #endregion
    }

}