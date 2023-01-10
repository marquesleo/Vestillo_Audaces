using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service
{
    public class ColecaoService : GenericService<Colecao, ColecaoRepository, ColecaoController>
    {
        public ColecaoService()
        {
            base.RequestUri = "Colecao";
        }

        public IEnumerable<Colecao> GetListPorDescricao(string Descricao)
        {
            var controller = new ColecaoRepository();
            return controller.GetListPorDescricao(Descricao);
        }
    }
}
