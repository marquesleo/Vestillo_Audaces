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
    public class CorServiceAPP : GenericServiceAPP<Cor, CorRepository, CorController>, ICorService
    {

        public CorServiceAPP() : base(new CorController())
        {
        }

        public IEnumerable<Cor> GetByAtivos(int AtivoInativo)
        {
            return controller.GetByAtivos(AtivoInativo);
        }


        public IEnumerable<Cor> GetListPorDescricao(string Descricao)
        {
            return controller.GetListPorDescricao(Descricao);
        }


        public IEnumerable<Cor> GetCoresProduto(int produto)
        {
            return controller.GetCoresProduto(produto);
        }


        public IEnumerable<Cor> GetCoresByProdutoTamanho(int produto, int tamanho)
        {
            return controller.GetCoresByProdutoTamanho(produto, tamanho);
        }
    }
}



