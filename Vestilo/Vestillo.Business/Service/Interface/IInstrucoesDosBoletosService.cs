using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service
{
    public interface IInstrucoesDosBoletosService : IService<InstrucoesDosBoletos, InstrucoesDosBoletosRepository, InstrucoesDosBoletosController>
    {
        IEnumerable<InstrucoesDosBoletosView> GetViewByData(DateTime data);
        IEnumerable<InstrucoesDosBoletosView> GetViewByDataEBanco(DateTime data, int IdBanco);       
        IEnumerable<InstrucoesDosBoletosView> GetViewByDataEBoleto(DateTime data, int IdBoleto);
        IEnumerable<InstrucoesDosBoletosView> GetViewByBoletoEInstrucao(int IdBoleto, int IdInstrucao);       

    }
}
