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
    public class BalancoEstoqueItensService : GenericService<BalancoEstoqueItens, BalancoEstoqueItensRepository, BalancoEstoqueItensController>
    {
        public BalancoEstoqueItensService()
        {
            base.RequestUri = "BalancoEstoqueItens";
        }

        public new IBalancoEstoqueItensService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new BalancoEstoqueItensServiceWeb(this.RequestUri);
            }
            else
            {
                return new BalancoEstoqueItensServiceAPP();
            }
        }
    }
}
