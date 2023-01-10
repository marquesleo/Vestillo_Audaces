using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Controllers;
using Vestillo.Business.Repositories;
using Vestillo.Lib;

namespace Vestillo.Business.Service.Web
{
    public class ObservacaoProdutoServiceWeb : GenericServiceWeb<ObservacaoProduto, ObservacaoProdutoRepository, ObservacaoProdutoController>, IObservacaoProdutoService
    {
        public ObservacaoProdutoServiceWeb(string requestUri)
           : base(requestUri)
        {
            this.RequestUri = requestUri;
        }
        public ObservacaoProdutoView GetByProduto(int produtoId)
        {
            throw new NotImplementedException();
        }
    }
}
