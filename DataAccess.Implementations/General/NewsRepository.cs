
using AAUG.Context.Context;
using AAUG.DataAccess.EntityRepository;
using AAUG.DataAccess.Interfaces.General;
using AAUG.DomainModels;
using AAUG.DomainModels.Models.Tables.General;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AAUG.DataAccess.Implementations.General
{
    public class NewsRepository : EntityRepository<News>, INewsRepository
    {
        private AaugUnitOfWork unitOfWork;
        private IMapper mapper;
        public NewsRepository(AaugUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork.Context)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public IQueryable<NewsForInsertDto> GetAllNews()
        {
            return mapper.ProjectTo<NewsForInsertDto>(FindAll());
        }

        public Task<News> InsertNews(NewsForInsertDto inputEntity)
        {
            return AddAsync(mapper.Map<News>(inputEntity));
        }


    }
}