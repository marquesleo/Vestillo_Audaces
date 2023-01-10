
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
    public interface IMovimentosDaOperacaoService : IService<MovimentosDaOperacao, MovimentosDaOperacaoRepository, MovimentosDaOperacaoController>
    {
        IEnumerable<MovimentosDaOperacao> GetByAtivos(int AtivoInativo);
        IEnumerable<MovimentosDaOperacao> GetListPorDescricao(string desc);
        IEnumerable<MovimentosDaOperacao> GetListPorReferencia(string referencia);
        IEnumerable<MovimentosDaOperacao> GetListById(int id);
    }
}




