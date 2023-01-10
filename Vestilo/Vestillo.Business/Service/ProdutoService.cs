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
    public  class ProdutoService: GenericService<Produto,ProdutoRepository,ProdutoController>
    {
        public ProdutoService()
        {
            base.RequestUri = "Produto";
        }


        public new IProdutoService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new ProdutoServiceWeb(this.RequestUri);
            }
            else
            {
                return new ProdutoServiceAPP();
            }
        }   
    }
}
