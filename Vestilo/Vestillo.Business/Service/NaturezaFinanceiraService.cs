using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Controllers;
using Vestillo.Business.Service.Interface;
using Vestillo.Business.Service.Web;
using Vestillo.Business.Service.APP;

namespace Vestillo.Business.Service
{
    public class NaturezaFinanceiraService: GenericService<NaturezaFinanceira, NaturezaFinanceiraRepository, NaturezaFinanceiraController>
    {

        public NaturezaFinanceiraService()
        {
            base.RequestUri = "NaturezaFinanceira";
        }

        public new INaturezaFinanceiraService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new NaturezaFinanceiraServiceWeb(this.RequestUri);
            }
            else
            {
                return new NaturezaFinanceiraServiceAPP();
            }
        }  
    }
}
