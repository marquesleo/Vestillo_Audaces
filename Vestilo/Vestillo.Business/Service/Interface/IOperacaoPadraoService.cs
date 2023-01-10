
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
    public interface IOperacaoPadraoService : IService<OperacaoPadrao, OperacaoPadraoRepository, OperacaoPadraoController>
    {
        IEnumerable<OperacaoPadrao> GetByAllSetorBal();       
        IEnumerable<OperacaoPadrao> GetByAtivos(int AtivoInativo);
        IEnumerable<OperacaoPadrao> GetListPorDescricao(string desc);
        IEnumerable<OperacaoPadrao> GetListPorReferencia(string referencia);
        IEnumerable<OperacaoPadrao> GetListById(int id);
    }
}



