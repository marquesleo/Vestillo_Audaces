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
    public class ObservacaoProdutoService : GenericService<ObservacaoProduto, ObservacaoProdutoRepository, ObservacaoProdutoController>
    {
        public ObservacaoProdutoService()
        {
            base.RequestUri = "ObservacaoProduto";
        }

        public new IObservacaoProdutoService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new ObservacaoProdutoServiceWeb(this.RequestUri);
            }
            else
            {
                return new ObservacaoProdutoServiceAPP();
            }
        }
    }
}
