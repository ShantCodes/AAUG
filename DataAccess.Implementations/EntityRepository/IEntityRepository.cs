using System.Linq.Expressions;

namespace AAUG.DataAccess.EntityRepository
{
    public interface IEntityRepository<T> where T : class
    {
        void Add(T entity);
        Task<T> AddAsync(T entity);
        IQueryable<T> GetData();
        IQueryable<T> GetData(Expression<Func<T, bool>> expression, bool isTrackChanges);
        void Update(T entity);
        void Delete(T entity);
        Task<T> DeleteAsync(int id);
    }
}