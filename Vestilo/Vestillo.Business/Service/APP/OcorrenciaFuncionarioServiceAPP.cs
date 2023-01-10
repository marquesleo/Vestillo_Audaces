using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Service.APP
{
    class OcorrenciaFuncionarioServiceAPP: GenericServiceAPP<OcorrenciaFuncionario, OcorrenciaFuncionarioRepository, OcorrenciaFuncionarioController>, IOcorrenciaFuncionarioService
    {
        public OcorrenciaFuncionarioServiceAPP()
            : base(new OcorrenciaFuncionarioController())
        {

        }

        public IEnumerable<OcorrenciaFuncionarioView> GetByFuncionarioIdEData(int funcId, DateTime data)
        {
            OcorrenciaFuncionarioController controller = new OcorrenciaFuncionarioController();
            return controller.GetByFuncionarioIdEData(funcId, data);
        }

        public IEnumerable<OcorrenciaRelatorioView> GetOcorrenciasRelatorio(int ano, decimal diasUteis)
        {
            OcorrenciaFuncionarioController controller = new OcorrenciaFuncionarioController();
            return controller.GetOcorrenciasRelatorio(ano, diasUteis );

        }
    }
}
