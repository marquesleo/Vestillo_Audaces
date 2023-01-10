using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class TamanhoController: GenericController<Tamanho, TamanhoRepository>
    {
        public IEnumerable<Tamanho> GetByAtivos(int AtivoInativo)
        {
            using (TamanhoRepository repository = new TamanhoRepository())
            {
                return repository.GetByAtivos(AtivoInativo);
            }
        }

        public IEnumerable<Tamanho> GetListByIds(List<int> ids)
        {
            using (TamanhoRepository repository = new TamanhoRepository())
            {
                return repository.GetListByIds(ids);
            }
        }

        public IEnumerable<Tamanho> GetListPorDescricao(string Descricao)
        {
            using (TamanhoRepository repository = new TamanhoRepository())
            {
                return repository.GetListPorDescricao(Descricao);
            }
        }

        public IEnumerable<Tamanho> GetTamanhosProduto(int produto)
        {
            using (TamanhoRepository repository = new TamanhoRepository())
            {
                return repository.GetTamanhosProduto(produto);
            }
        }

    }
}
