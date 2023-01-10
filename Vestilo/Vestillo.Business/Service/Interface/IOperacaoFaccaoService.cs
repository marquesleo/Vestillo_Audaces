using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Service
{
    public interface IOperacaoFaccaoService : IService<OperacaoFaccao, OperacaoFaccaoRepository, OperacaoFaccaoController>
    {
        IEnumerable<OperacaoFaccao> GetByIdView(int id);
        IEnumerable<OperacaoFaccaoView> GetByFuncionarioIdEData(int funcId, DateTime data);
        OperacaoFaccaoView GetByCupom(int pacoteId, int operacaoId, string sequencia);
    }
}
