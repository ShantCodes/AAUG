using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAUG.Context.Context;
using AAUG.DataAccess.Interfaces.General;

namespace AAUG.DataAccess.Implementations
{
    public interface IAaugUnitOfWork
    {
        #region Transaction Methods
        void SaveChanges();
        Task SaveChangesAsync();
        void BeginTransaction();
        void BeginTransactionAsync();
        void CommitTransaction();
        Task CommitTransactionAsync();
        void Dispose();

        #endregion
        AaugContext Context {get;}
        INewsRepository NewsRepository {get;}
    }
}
