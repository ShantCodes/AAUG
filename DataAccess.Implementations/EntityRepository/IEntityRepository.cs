using System.Linq.Expressions;

namespace AAUG.DataAccess.EntityRepository
{
    public interface IEntityRepository<T> where T : class
    {
        Task<T> GetData(object id);
        void Create(T entity);
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool isTrackChanges);
        void Update(T entity);
        void Delete(T entity);
    }
}