
using AAUG.Context.Context;
using AAUG.DataAccess.EntityRepository;
using AAUG.DataAccess.Interfaces.General;
using AAUG.DomainModels.Models.Tables.General;
using Microsoft.EntityFrameworkCore;

namespace AAUG.DataAccess.Implementations.General
{
    public class NewsRepository : EntityRepository<News>, INewsRepository
    {
        private AaugUnitOfWork unitOfWork;
        public NewsRepository(AaugUnitOfWork unitOfWork) : base(unitOfWork.Context)
        {           
            this.unitOfWork = unitOfWork;
        }

        public Task<List<News>> GetAllNews()
        {           
            return FindAll().ToListAsync();
        }

        
    }
}