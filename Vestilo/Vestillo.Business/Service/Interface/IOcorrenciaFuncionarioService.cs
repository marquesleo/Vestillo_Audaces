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
    public interface IOcorrenciaFuncionarioService : IService<OcorrenciaFuncionario, OcorrenciaFuncionarioRepository, OcorrenciaFuncionarioController>
    {
        IEnumerable<OcorrenciaFuncionarioView> GetByFuncionarioIdEData(int funcId, DateTime data);
        IEnumerable<OcorrenciaRelatorioView> GetOcorrenciasRelatorio(int ano, decimal diasUteis);
    }
}
