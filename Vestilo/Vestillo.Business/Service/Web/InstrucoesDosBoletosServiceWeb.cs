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
using Vestillo.Lib;

namespace Vestillo.Business.Service.Web
{
    public class InstrucoesDosBoletosServiceWeb : GenericServiceWeb<InstrucoesDosBoletos, InstrucoesDosBoletosRepository, InstrucoesDosBoletosController>, IInstrucoesDosBoletosService
    {
        public InstrucoesDosBoletosServiceWeb(string requestUri)  : base(requestUri)
        {
            this.RequestUri = requestUri;
        }


        public IEnumerable<InstrucoesDosBoletosView> GetViewByData(DateTime data)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<InstrucoesDosBoletosView> GetViewByDataEBanco(DateTime data, int IdBanco)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<InstrucoesDosBoletosView> GetViewByDataEBoleto(DateTime data, int IdBoleto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<InstrucoesDosBoletosView> GetViewByBoletoEInstrucao(int IdBoleto, int IdInstrucao)
        {
            throw new NotImplementedException();
        }
    }
}
