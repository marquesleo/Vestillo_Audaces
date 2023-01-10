using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Controllers
{
    public class OperacaoFaccaoController : GenericController<OperacaoFaccao, OperacaoFaccaoRepository>
    {
        public IEnumerable<OperacaoFaccao> GetByIdView(int id)
        {
            using (var repository = new OperacaoFaccaoRepository())
            {
                return repository.GetByIdView(id);
            }
        }

        public IEnumerable<OperacaoFaccaoView> GetByFuncionarioIdEData(int funcId, DateTime data)
        {
            using (var repository = new OperacaoFaccaoRepository())
            {
                return repository.GetByFuncionarioIdEData(funcId, data);
            }
        }

        public OperacaoFaccaoView GetByCupom(int pacoteId, int operacaoId, string sequencia)
        {
            using (var repository = new OperacaoFaccaoRepository())
            {
                return repository.GetByCupom(pacoteId, operacaoId, sequencia);
            }
        }
    }
}
