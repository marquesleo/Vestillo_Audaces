using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Controllers;
using Vestillo.Business.Service.Web;
using Vestillo.Business.Service.APP;

namespace Vestillo.Business.Service
{
    public class ProdutoDetalheService : GenericService<ProdutoDetalhe, ProdutoDetalheRepository, ProdutoDetalheController>
    {
        public ProdutoDetalheService()
        {
            base.RequestUri = "ProdutoDetalhe";
        }


        public new IProdutoDetalheService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new ProdutoDetalheServiceWeb(this.RequestUri);
            }
            else
            {
                return new ProdutoDetalheServiceAPP();
            }
        } 

    }
}
