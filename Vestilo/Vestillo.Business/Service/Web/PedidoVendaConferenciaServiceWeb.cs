using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Controllers;
using Vestillo.Business.Repositories;
using Vestillo.Lib;

namespace Vestillo.Business.Service.Web
{
    public class PedidoVendaConferenciaServiceWeb : GenericServiceWeb<PedidoVendaConferencia, PedidoVendaConferenciaRepository, PedidoVendaConferenciaController>, IPedidoVendaConferenciaService
    {

        public PedidoVendaConferenciaServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<PedidoVendaConferenciaView> GetListParaConferencia()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PedidoVendaConferenciaitensDciView> GetListParaConferenciaDci()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PedidoVendaConferenciaView> GetListParaConferenciaSemEmpenho()
        {
            throw new NotImplementedException();
        }

        public void Save(ref PedidoVendaConferencia conferencia, bool atualizarLiberacoes, bool semEmpenho = false)
        {
            throw new NotImplementedException();
        }
    }
}


