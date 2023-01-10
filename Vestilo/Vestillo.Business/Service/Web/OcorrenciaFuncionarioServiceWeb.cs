using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Service.Web
{
    public class OcorrenciaFuncionarioServiceWeb: GenericServiceWeb<OcorrenciaFuncionario, OcorrenciaFuncionarioRepository, OcorrenciaFuncionarioController>, IOcorrenciaFuncionarioService
    {
        public OcorrenciaFuncionarioServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<OcorrenciaFuncionarioView> GetByFuncionarioIdEData(int funcId, DateTime data)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<OcorrenciaRelatorioView> GetOcorrenciasRelatorio(int ano, decimal diasUteis)
        {
            throw new NotImplementedException();
        }
    }
}
