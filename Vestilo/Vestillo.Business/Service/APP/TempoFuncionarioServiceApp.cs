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
    public class TempoFuncionarioServiceApp: GenericServiceAPP<TempoFuncionario, TempoFuncionarioRepository, TempoFuncionarioController>, ITempoFuncionarioService
    {
        public TempoFuncionarioServiceApp()
            : base(new TempoFuncionarioController())
        {

        }

        public IEnumerable<TempoFuncionario> GetByFuncionarioIdEData(int funcId, DateTime data)
        {
            TempoFuncionarioController controller = new TempoFuncionarioController();
            return controller.GetByFuncionarioIdEData(funcId, data);
        }
    }
}
