using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AAUG.Context.Context;
using AAUG.DataAccess.Implementations.General;
using AAUG.DataAccess.Interfaces.General;
using Microsoft.EntityFrameworkCore.Diagnostics;


namespace AAUG.DataAccess.Implementations
{
    public class AaugUnitOfWork : IAaugUnitOfWork
    {
        
        private AaugContext dbContext;
        public AaugUnitOfWork(AaugContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //public AaugContext Context { get; private set; }
         public AaugContext Context => dbContext;

        public required INewsRepository _newsRepository;
        public INewsRepository NewsRepository => _newsRepository ??= new NewsRepository(this);
    }
}
