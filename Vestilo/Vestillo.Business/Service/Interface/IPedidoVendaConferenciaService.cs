using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Controllers;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service
{
    public interface IPedidoVendaConferenciaService : IService<PedidoVendaConferencia, PedidoVendaConferenciaRepository, PedidoVendaConferenciaController>
    {
        IEnumerable<PedidoVendaConferenciaView> GetListParaConferencia();
        IEnumerable<PedidoVendaConferenciaitensDciView> GetListParaConferenciaDci();        
        void Save(ref PedidoVendaConferencia conferencia, bool atualizarLiberacoes, bool semEmpenho = false);
        IEnumerable<PedidoVendaConferenciaView> GetListParaConferenciaSemEmpenho();
    }
}



