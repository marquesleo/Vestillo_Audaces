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
    public class EstoqueService: GenericService<Estoque, EstoqueRepository, EstoqueController>
    {
        public EstoqueService()
        {
            base.RequestUri = "Estoque";
        }

        public new IEstoqueService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new EstoqueServiceWeb(this.RequestUri);
            }
            else
            {
                return new EstoqueServiceAPP();
            }
        }   
    }
}
