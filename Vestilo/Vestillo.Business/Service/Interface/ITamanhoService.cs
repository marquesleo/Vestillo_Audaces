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
    public interface ITamanhoService : IService<Tamanho, TamanhoRepository, TamanhoController>
    {
        IEnumerable<Tamanho> GetByAtivos(int AtivoInativo);
        IEnumerable<Tamanho> GetListByIds(List<int> ids);
        IEnumerable<Tamanho> GetListPorDescricao(string Descricao);
        IEnumerable<Tamanho> GetTamanhosProduto(int produto);
    }
}