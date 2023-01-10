using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Controllers;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.APP;
using Vestillo.Business.Service.Web;

namespace Vestillo.Business.Service
{
    public class GenericService<TModel, TRepository, TController> 
        where TModel : class
        where TController : GenericController<TModel, TRepository>
        where TRepository : GenericRepository<TModel>
    {
        public string RequestUri { get; set; }

        public  IService<TModel, TRepository, TController> GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new GenericServiceWeb<TModel, TRepository, TController>(this.RequestUri);
            }
            else
            {
                return new GenericServiceAPP<TModel, TRepository, TController>(InstanciateTController());
            }
        }   
     
        private TController InstanciateTController()
        {
            return (TController)Activator.CreateInstance(typeof(TController));
        }
    }
}
