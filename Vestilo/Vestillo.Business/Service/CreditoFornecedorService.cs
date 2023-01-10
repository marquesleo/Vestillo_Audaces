
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.APP;
using Vestillo.Business.Service.Web;

namespace Vestillo.Business.Service
{
    public class CreditoFornecedorService: GenericService<CreditoFornecedor, CreditoFornecedorRepository, CreditoFornecedorController>
    {
        public CreditoFornecedorService()
        {
            base.RequestUri = "CreditoFornecedor";
        }

        public new ICreditoFornecedorService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new CreditoFornecedorServiceWeb(this.RequestUri);
            }
            else
            {
                return new CreditoFornecedorServiceAPP();
            }
        }  
    }
}
