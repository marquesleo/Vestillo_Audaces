using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Controllers;

namespace Vestillo.Business.Service
{
    public class GrupProdutoService : GenericService<GrupProduto, GrupProdutoRepository, GrupProdutoController>
    {
        public GrupProdutoService()
        {
            base.RequestUri = "GrupProduto";
        }

        public IEnumerable<GrupProduto> GetListPorDescricao(string Descricao)
        {
            var controller = new GrupProdutoController();
            return controller.GetListPorDescricao(Descricao);
        }
    }
}