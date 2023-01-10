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
    public interface ITabelaPrecoService: IService<TabelaPreco, TabelaPrecoRepository, TabelaPrecoController>
    {
        TabelaPreco GetByReferencia(string referencia);
        IEnumerable<TabelaPreco> GetListPorDescricao(string desc);
        IEnumerable<TabelaPreco> GetListPorReferencia(string referencia);
        IEnumerable<TabelaPreco> GetAllView();
        void CalcularCustos(ref ItemTabelaPrecoView item, decimal percentualImpostosEEncargos, decimal margemLucro);

    }
}
