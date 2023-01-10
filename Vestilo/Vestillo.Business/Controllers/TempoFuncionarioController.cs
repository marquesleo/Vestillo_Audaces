using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Controllers
{
    public class TempoFuncionarioController : GenericController<TempoFuncionario, TempoFuncionarioRepository>
    {
        public IEnumerable<TempoFuncionario> GetByFuncionarioIdEData(int funcId, DateTime data)
        {
            using (var repository = new TempoFuncionarioRepository())
            {
                return repository.GetByFuncionarioIdEData(funcId, data);
            }
        }
    }
}
