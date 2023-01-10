using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class ColecaoController : GenericController<Colecao, ColecaoRepository>
    {
        public IEnumerable<Colecao> GetListPorDescricao(string Descricao)
        {
            using (ColecaoRepository repository = new ColecaoRepository())
            {
                return repository.GetListPorDescricao(Descricao);
            }
        }
    }
}
