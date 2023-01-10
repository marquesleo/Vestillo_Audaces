using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Controllers;
using Vestillo.Business.Service.APP;
using Vestillo.Business.Service.Web;

namespace Vestillo.Business.Service
{
    public class BalancoEstoqueService : GenericService<BalancoEstoque, BalancoEstoqueRepository, BalancoEstoqueController> 
    {
        public BalancoEstoqueService()
        {
            base.RequestUri = "BalancoEstoque";
        }

        public new IBalancoEstoqueService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new BalancoEstoqueServiceWeb(this.RequestUri);
            }
            else
            {
                return new BalancoEstoqueServiceAPP();
            }
        }
    }
}
