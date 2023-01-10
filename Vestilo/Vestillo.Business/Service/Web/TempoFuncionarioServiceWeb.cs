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
    public class TempoFuncionarioServiceWeb: GenericServiceWeb<TempoFuncionario, TempoFuncionarioRepository, TempoFuncionarioController>, ITempoFuncionarioService
    {
        public TempoFuncionarioServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<TempoFuncionario> GetByFuncionarioIdEData(int funcId, DateTime data)
        {
            throw new NotImplementedException();
        }
    }
}
