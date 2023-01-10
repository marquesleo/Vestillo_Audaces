using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class ObservacaoProdutoController : GenericController<ObservacaoProduto, ObservacaoProdutoRepository>
    {
        public ObservacaoProdutoView GetByProduto(int produtoId)
        {
            return _repository.GetByProduto(produtoId);
        }
    }
}
