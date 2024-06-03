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
        AaugContext Context {get;}
        INewsRepository NewsRepository {get;}
    }
}
