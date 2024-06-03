using System.Data.Common;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using AAUG.Context.Context;
using AAUG.DataAccess.Implementations;
using AAUG.DomainModels.Models.Tables.General;
using Microsoft.EntityFrameworkCore;

namespace AAUG.DataAccess.EntityRepository
{
    public class EntityRepository<T> : IEntityRepository<T> where T : class
    {
        private readonly IAaugUnitOfWork unitOfWork;
        private readonly DbSet<T> Entities;
        private AaugContext context;
        public EntityRepository(AaugContext context)
        {
            this.context = context;
        }

        public void Create(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public IQueryable<T> FindAll()
        {
            return context.Set<T>();
        }

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool isTrackChanges)
        {
            if (isTrackChanges)
                return context.Set<T>().Where(expression);
            return context.Set<T>().Where(expression).AsNoTracking();
        }

        public Task<T> GetData(object id)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            context.Set<T>().Update(entity);
        }
    }
}