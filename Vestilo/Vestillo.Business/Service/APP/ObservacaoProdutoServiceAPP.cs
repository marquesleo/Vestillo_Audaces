using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service.APP
{
    public class ObservacaoProdutoServiceAPP : GenericServiceAPP<ObservacaoProduto, ObservacaoProdutoRepository, ObservacaoProdutoController>, IObservacaoProdutoService    
    {
        public ObservacaoProdutoServiceAPP()
            : base(new ObservacaoProdutoController())
        {

        }
        public ObservacaoProdutoView GetByProduto(int produtoId)
        {
            return controller.GetByProduto(produtoId);
        }
    }
}
