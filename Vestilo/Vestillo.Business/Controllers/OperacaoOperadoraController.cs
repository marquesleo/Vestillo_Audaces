using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Controllers
{
    public class OperacaoOperadoraController : GenericController<OperacaoOperadora, OperacaoOperadoraRepository>
    {
        public IEnumerable<OperacaoOperadora> GetByIdView(int id)
        {
            using (var repository = new OperacaoOperadoraRepository())
            {
                return repository.GetByIdView(id);
            }
        }

        public IEnumerable<OperacaoOperadoraView> GetByFuncionarioIdEData(int funcId, DateTime data)
        {
            using (var repository = new OperacaoOperadoraRepository())
            {
                return repository.GetByFuncionarioIdEData(funcId, data);
            }
        }

        public OperacaoOperadoraView GetByCupom(int pacoteId, int operacaoId, string sequencia)
        {
            using (var repository = new OperacaoOperadoraRepository())
            {
                return repository.GetByCupom(pacoteId, operacaoId, sequencia);
            }
        }
    }
}
