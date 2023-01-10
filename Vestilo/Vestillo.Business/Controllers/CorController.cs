using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class CorController: GenericController<Cor, CorRepository>
    {

        public IEnumerable<Cor> GetByAtivos(int AtivoInativo)
        {
            using (CorRepository repository = new CorRepository())
            {
                return repository.GetByAtivos(AtivoInativo);
            }
        }

        public IEnumerable<Cor> GetListPorDescricao(string Descricao)
        {
            using (CorRepository repository = new CorRepository())
            {
                return repository.GetListPorDescricao(Descricao);
            }
        }

         public IEnumerable<Cor> GetCoresProduto(int produto)
        {
            using (CorRepository repository = new CorRepository())
            {
                return repository.GetCoresProduto(produto);
            }
        }

         public IEnumerable<Cor> GetCoresByProdutoTamanho(int produto, int tamanho)
         {
             using (CorRepository repository = new CorRepository())
             {
                 return repository.GetCoresByProdutoTamanho(produto, tamanho);
             }
         }
    }
}
