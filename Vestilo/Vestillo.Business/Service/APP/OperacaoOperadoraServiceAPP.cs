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
    public class OperacaoOperadoraServiceAPP: GenericServiceAPP<OperacaoOperadora, OperacaoOperadoraRepository, OperacaoOperadoraController>, IOperacaoOperadoraService
    {
        public OperacaoOperadoraServiceAPP()
            : base(new OperacaoOperadoraController())
        {

        }

        public IEnumerable<OperacaoOperadora> GetByIdView(int id)
        {
            OperacaoOperadoraController controller = new OperacaoOperadoraController();
            return controller.GetByIdView(id);
        }


        public IEnumerable<OperacaoOperadoraView> GetByFuncionarioIdEData(int funcId, DateTime data)
        {
            OperacaoOperadoraController controller = new OperacaoOperadoraController();
            return controller.GetByFuncionarioIdEData(funcId, data);
        }


        public OperacaoOperadoraView GetByCupom(int pacoteId, int operacaoId, string sequencia)
        {
            OperacaoOperadoraController controller = new OperacaoOperadoraController();
            return controller.GetByCupom(pacoteId, operacaoId, sequencia);
        }
    }
}
