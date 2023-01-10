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
    public interface ITempoFuncionarioService : IService<TempoFuncionario, TempoFuncionarioRepository, TempoFuncionarioController>
    {
        IEnumerable<TempoFuncionario> GetByFuncionarioIdEData(int funcId, DateTime data);
    }
}
