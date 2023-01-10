using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class InstrucoesDosBoletosController : GenericController<InstrucoesDosBoletos, InstrucoesDosBoletosRepository>
    {
        public IEnumerable<InstrucoesDosBoletosView> GetViewByData(DateTime data)
        {
            var repository = new InstrucoesDosBoletosRepository();
            return repository.GetViewByData(data);
        }

        public IEnumerable<InstrucoesDosBoletosView> GetViewByDataEBanco(DateTime data, int IdBanco)
        {
            var repository = new InstrucoesDosBoletosRepository();
            return repository.GetViewByDataEBanco(data, IdBanco);
        }

        public IEnumerable<InstrucoesDosBoletosView> GetViewByDataEBoleto(DateTime data, int IdBoleto)
        {
            var repository = new InstrucoesDosBoletosRepository();
            return repository.GetViewByDataEBoleto(data, IdBoleto);
        }

        public IEnumerable<InstrucoesDosBoletosView> GetViewByBoletoEInstrucao(int IdBoleto, int IdInstrucao)
        {
            var repository = new InstrucoesDosBoletosRepository();
            return repository.GetViewByBoletoEInstrucao(IdBoleto, IdInstrucao);
        }

    }

    
}
