using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Core.Repositories
{
    public interface IRepository<TModel> : IDisposable where TModel : class
    {
        void Save(TModel entity);
        TModel Find(int id);
        void Delete(int id);
        IEnumerable<TModel> ListAll();
        void FillReferencia(ref TModel entity); 
    }
}
