using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Controllers;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service
{
    public interface IService<TModel, TRepository, TController>
        where TModel : class
        where TController : GenericController<TModel, TRepository>
        where TRepository : GenericRepository<TModel>
    {
        string RequestUri { get; set; }
        void Save(ref TModel entity);
        void Delete(int id);
        TModel GetById(int id);
        IEnumerable<TModel> GetById(int[] ids);
        IEnumerable<TModel> GetAll();
        int GetIdPropertyModel(TModel entity);
    }
}
