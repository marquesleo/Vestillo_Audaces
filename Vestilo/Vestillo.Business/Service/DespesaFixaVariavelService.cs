using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.APP;
using Vestillo.Business.Service.Interface;
using Vestillo.Business.Service.Web;

namespace Vestillo.Business.Service
{
    public class DespesaFixaVariavelService : GenericService<DespesaFixaVariavel, DespesaFixaVariavelRepository, DespesaFixaVariavelController>
    {
        public DespesaFixaVariavelService()
        {
            base.RequestUri = "DespesaFixaVariavel";
        }

        public new IDespesaFixaVariavelService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new DespesaFixaVariavelServiceWeb(this.RequestUri);
            }
            else
            {
                return new DespesaFixaVariavelServiceAPP();
            }
        }  
    }
}
