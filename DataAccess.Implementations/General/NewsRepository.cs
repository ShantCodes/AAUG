
using AAUG.Context.Context;
using AAUG.DataAccess.EntityRepository;
using AAUG.DataAccess.Implementations.UnitOfWork;
using AAUG.DataAccess.Interfaces.General;
using AAUG.DomainModels;
using AAUG.DomainModels.Models.Tables.General;
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

        public Task<News> FirstAsync(int id)
        {
            return FindByCondition(a => a.Id == id).FirstAsync();
        }

        public IQueryable<NewsForInsertDto> GetAllNews()
        {
            return mapper.ProjectTo<NewsForInsertDto>(FindAll());
        }

        public Task<News> InsertNews(NewsForInsertDto inputEntity)
        {
            return AddAsync(mapper.Map<News>(inputEntity));
        }

        public Task<News> DeleteNewsAsync(int id)
        {
            return DeleteAsync(id);
        }
    }
}