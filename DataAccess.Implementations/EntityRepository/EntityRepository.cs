using System.Data.Common;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using AAUG.Context.Context;
using AAUG.DataAccess.Implementations;
using AAUG.DataAccess.Implementations.UnitOfWork;
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

        public void Add(T entity)
        {
            context.Set<T>().Add(entity);
        }

        public async Task<T> AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            return entity;
        }
        public async Task<List<T>> AddAsync(List<T> entity)
        {
            await context.Set<List<T>>().AddAsync(entity);
            return entity;
        }

        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }
        public async Task<T> DeleteAsync(int id)
        {
            var entity = await context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                throw new ArgumentException($"Entity with id {id} not found");
            }
            context.Remove(entity);
            return entity;
        }

        public IQueryable<T> GetData()
        {
            return context.Set<T>();
        }

        public IQueryable<T> GetData(Expression<Func<T, bool>> expression, bool isTrackChanges = true)
        {
            if (isTrackChanges)
                return context.Set<T>().Where(expression);
            return context.Set<T>().Where(expression).AsNoTracking();
        }

        public IQueryable<T> GetData(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = context.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query;
        }

        public void Update(T entity)
        {
            context.Set<T>().Update(entity);
        }
    }
}