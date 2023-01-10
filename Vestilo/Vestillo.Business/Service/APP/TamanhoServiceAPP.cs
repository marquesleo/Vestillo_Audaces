
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
    public class TamanhoServiceAPP : GenericServiceAPP<Tamanho, TamanhoRepository, TamanhoController>, ITamanhoService
    {

        public TamanhoServiceAPP() : base(new TamanhoController())
        {
        }

        public IEnumerable<Tamanho> GetByAtivos(int AtivoInativo)
        {
            return controller.GetByAtivos(AtivoInativo);
        }


        public IEnumerable<Tamanho> GetListByIds(List<int> ids)
        {
            return controller.GetListByIds(ids);
        }


        public IEnumerable<Tamanho> GetListPorDescricao(string Descricao)
        {
            return controller.GetListPorDescricao(Descricao);
        }


        public IEnumerable<Tamanho> GetTamanhosProduto(int produto)
        {
            return controller.GetTamanhosProduto(produto);
        }
    }
}