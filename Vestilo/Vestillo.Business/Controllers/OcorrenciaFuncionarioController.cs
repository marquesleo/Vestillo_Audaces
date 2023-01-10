using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Controllers
{
    public class OcorrenciaFuncionarioController : GenericController<OcorrenciaFuncionario, OcorrenciaFuncionarioRepository>
    {
        public IEnumerable<OcorrenciaFuncionarioView> GetByFuncionarioIdEData(int funcId, DateTime data)
        {
            using (var repository = new OcorrenciaFuncionarioRepository())
            {
                return repository.GetByFuncionarioIdEData(funcId, data);
            }
        }

        public IEnumerable<OcorrenciaRelatorioView> GetOcorrenciasRelatorio(int ano, decimal diasUteis)
        {
            using (var repository = new OcorrenciaFuncionarioRepository())
            {
                return repository.GetOcorrenciasRelatorio(ano, diasUteis);
            }
        }

    }
}
