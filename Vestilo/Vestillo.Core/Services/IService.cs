using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Core.Services
{
    public interface IService<TModel> : IDisposable where TModel : class
    {
        void Save(TModel entity);
        TModel Find(int id);
        void Delete(int id);
        IEnumerable<TModel> ListAll();
    }
}
