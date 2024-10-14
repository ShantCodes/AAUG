
using System.Linq;
using AAUG.Context.Context;
using AAUG.DataAccess.EntityRepository;
using AAUG.DataAccess.Implementations.UnitOfWork;
using AAUG.DataAccess.Interfaces.General;
using AAUG.DomainModels;
using AAUG.DomainModels.Models.Tables.General;
using AAUG.DomainModels.ViewModels;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AAUG.DataAccess.Implementations.General
{
    public class NewsRepository : EntityRepository<News>, INewsRepository
    {
        private IAaugUnitOfWork unitOfWork;
        private IMapper mapper;
        public NewsRepository(IAaugUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.Context)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public IQueryable<NewsGetDto> GetNewsByTitle(string newsTitle)
        {
            return mapper.ProjectTo<NewsGetDto>(
                GetData(a => a.NewsTitle.Contains(newsTitle))
            );
        }

        public Task<News> FirstAsync(int id)
        {
            return GetData(a => a.Id == id).FirstAsync();
        }

        public IQueryable<NewsGetDto> GetAllNews()
        {
            return mapper.ProjectTo<NewsGetDto>(GetData());
        }

        public IQueryable<NewsGetDto> Get4LastNews()
        {
            return mapper.ProjectTo<NewsGetDto>(
                GetData().OrderByDescending(a => a.Id).Take(4)
            );
        }

        public Task<News> InsertNews(NewsForInsertDto inputEntity)
        {
            return AddAsync(mapper.Map<News>(inputEntity));
        }

        public Task<News> DeleteNewsAsync(int id)
        {
            return DeleteAsync(id);
        }

        public IQueryable<NewsGetDto> GetNewsById(int id)
        {
            return mapper.ProjectTo<NewsGetDto>(
                GetData(a => a.Id == id));
        }
    }
}