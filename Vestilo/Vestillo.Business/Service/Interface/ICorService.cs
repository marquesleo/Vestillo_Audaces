using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Controllers;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service
{
    public interface ICorService : IService<Cor, CorRepository, CorController>
    {
        IEnumerable<Cor> GetByAtivos(int AtivoInativo);
        IEnumerable<Cor> GetListPorDescricao(string Descricao);
        IEnumerable<Cor> GetCoresProduto(int produto);
        IEnumerable<Cor> GetCoresByProdutoTamanho(int produto, int tamanho);
    }
}



