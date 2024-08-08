using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAUG.Context.Context;
using AAUG.DataAccess.Interfaces.General;
using AAUG.DataAccess.Interfaces.Media;

namespace AAUG.DataAccess.Implementations.UnitOfWork
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

        #region Repositories
        AaugContext Context { get; }
        INewsRepository NewsRepository { get; }
        IAaugUserRepository AaugUserRepository { get; }
        IEventRepository EventRepository { get; }
        IMediaFileRepository MediaFileRepository { get; }
        IMediaFolderRepository MediaFolderRepository { get; }
        IEventLikeRepository EventLikeRepository { get; }

        #endregion
    }
}
