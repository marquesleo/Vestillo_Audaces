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
    public class ProdutoFornecedorPrecoService : GenericService<ProdutoFornecedorPreco, ProdutoFornecedorPrecoRepository, ProdutoFornecedorPrecoController>
    {
        public ProdutoFornecedorPrecoService()
        {
            base.RequestUri = "ProdutoFornecedorPreco";
        }
        
        public new IProdutoFornecedorPrecoService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new ProdutoFornecedorPrecoServiceWeb(this.RequestUri);
            }
            else
            {
                return new ProdutoFornecedorPrecoServiceAPP();
            }
        } 
       
    }
}
