using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Controllers;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service.APP
{
    public class GenericServiceAPP<TModel, TRepository, TController> : IService<TModel, TRepository, TController>
        where TModel : class
        where TController : GenericController<TModel, TRepository>
        where TRepository : GenericRepository<TModel>
    {
            public TController controller { get; set; }
            public string RequestUri { get; set; }

            public GenericServiceAPP(TController controller)
            {
                this.controller = controller;
            }
            
            public void Save(ref TModel entity)
            {
                this.controller.Save(ref entity);
            }

            public void Delete(int id)
            {
               this.controller.Delete(id);
            }

            public TModel GetById(int id)
            {
               return this.controller.GetById(id);
            }


            public IEnumerable<TModel> GetById(int[] ids)
            {
                return this.controller.GetById(ids);
            }

            public IEnumerable<TModel> GetAll()
            {
                return this.controller.GetAll();
            }

            public int GetIdPropertyModel(TModel entity)
            {
                return this.controller.GetIdPropertyModel(entity);
            }
    }
}
