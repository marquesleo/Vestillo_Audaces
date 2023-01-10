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
    public interface IOperacaoOperadoraService : IService<OperacaoOperadora, OperacaoOperadoraRepository, OperacaoOperadoraController>
    {
        IEnumerable<OperacaoOperadora> GetByIdView(int id);
        IEnumerable<OperacaoOperadoraView> GetByFuncionarioIdEData(int funcId, DateTime data);
        OperacaoOperadoraView GetByCupom(int pacoteId, int operacaoId, string sequencia);
    }
}
