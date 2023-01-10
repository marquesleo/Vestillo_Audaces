using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service.APP
{
    public class PedidoVendaConferenciaServiceAPP : GenericServiceAPP<PedidoVendaConferencia, PedidoVendaConferenciaRepository, PedidoVendaConferenciaController>, IPedidoVendaConferenciaService
    {

        public PedidoVendaConferenciaServiceAPP() : base(new PedidoVendaConferenciaController())
        {
        }

        public IEnumerable<PedidoVendaConferenciaView> GetListParaConferencia()
        {
            return controller.GetListParaConferencia();
        }

        public IEnumerable<PedidoVendaConferenciaitensDciView> GetListParaConferenciaDci()
        {
            return controller.GetListParaConferenciaDci();
        }

        public IEnumerable<PedidoVendaConferenciaView> GetListParaConferenciaSemEmpenho()
        {
            return controller.GetListParaConferenciaSemEmpenho();
        }

        public void Save(ref PedidoVendaConferencia conferencia, bool atualizarLiberacoes, bool semEmpenho = false)
        {
            controller.Save(ref conferencia, atualizarLiberacoes, semEmpenho);
        }
    }
}



