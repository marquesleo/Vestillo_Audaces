using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class GrupProdutoController : GenericController< GrupProduto , GrupProdutoRepository >
    {

        public IEnumerable<GrupProduto> GetListPorDescricao(string Descricao)
        {
            using (GrupProdutoRepository repository = new GrupProdutoRepository())
            {
                return repository.GetListPorDescricao(Descricao);
            }
        }
    }
}
