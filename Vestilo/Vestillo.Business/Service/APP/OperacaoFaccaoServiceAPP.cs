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
    public class OperacaoFaccaoServiceAPP : GenericServiceAPP<OperacaoFaccao, OperacaoFaccaoRepository, OperacaoFaccaoController>, IOperacaoFaccaoService
    {
        public OperacaoFaccaoServiceAPP()
            : base(new OperacaoFaccaoController())
        {

        }

        public IEnumerable<OperacaoFaccao> GetByIdView(int id)
        {
            OperacaoFaccaoController controller = new OperacaoFaccaoController();
            return controller.GetByIdView(id);
        }


        public IEnumerable<OperacaoFaccaoView> GetByFuncionarioIdEData(int funcId, DateTime data)
        {
            OperacaoFaccaoController controller = new OperacaoFaccaoController();
            return controller.GetByFuncionarioIdEData(funcId, data);
        }


        public OperacaoFaccaoView GetByCupom(int pacoteId, int operacaoId, string sequencia)
        {
            OperacaoFaccaoController controller = new OperacaoFaccaoController();
            return controller.GetByCupom(pacoteId, operacaoId, sequencia);
        }
    }
}
