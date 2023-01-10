using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.Interface;

namespace Vestillo.Business.Service.APP
{
    public class InstrucoesDosBoletosServiceAPP : GenericServiceAPP<InstrucoesDosBoletos, InstrucoesDosBoletosRepository, InstrucoesDosBoletosController>, IInstrucoesDosBoletosService
    {
        public InstrucoesDosBoletosServiceAPP() : base(new InstrucoesDosBoletosController())
        {
        }

        public IEnumerable<InstrucoesDosBoletosView> GetViewByData(DateTime data)
        {
            InstrucoesDosBoletosController controller = new InstrucoesDosBoletosController();
            return controller.GetViewByData(data);
        }

        public IEnumerable<InstrucoesDosBoletosView> GetViewByDataEBanco(DateTime data, int IdBanco)
        {
            InstrucoesDosBoletosController controller = new InstrucoesDosBoletosController();
            return controller.GetViewByDataEBanco(data, IdBanco);
        }

        public IEnumerable<InstrucoesDosBoletosView> GetViewByDataEBoleto(DateTime data, int IdBoleto)
        {
            InstrucoesDosBoletosController controller = new InstrucoesDosBoletosController();
            return controller.GetViewByDataEBoleto(data, IdBoleto);
        }

        public IEnumerable<InstrucoesDosBoletosView> GetViewByBoletoEInstrucao(int IdBoleto, int IdInstrucao)
        {
            InstrucoesDosBoletosController controller = new InstrucoesDosBoletosController();
            return controller.GetViewByBoletoEInstrucao(IdBoleto, IdInstrucao);
        }

    }
}
