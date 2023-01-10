
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.Interface;

namespace Vestillo.Business.Service.APP
{
    public class NotaEntradaServiceAPP : GenericServiceAPP<NotaEntrada, NotaEntradaRepository, NotaEntradaController>, INotaEntradaService
    {
        public NotaEntradaServiceAPP() : base(new NotaEntradaController())
        {
        }

        public IEnumerable<NotaEntradaView> GetCamposEspecificos(string parametrosDaBusca)
        {
            return controller.GetCamposEspecificos(parametrosDaBusca);
        }


        public IEnumerable<NotaEntradaView> GetListPorReferencia(string referencia, string parametrosDaBusca)
        {

            return controller.GetListPorReferencia(referencia,parametrosDaBusca);
            
        }

        public IEnumerable<NotaEntradaView> GetListPorNumero(string Numero, string parametrosDaBusca)
        {
            return controller.GetListPorNumero(Numero,parametrosDaBusca);
            
        }

        public IEnumerable<NotaEntradaView> GetListPorPedido(int pedidoId)
        {
            return controller.GetListPorPedido(pedidoId);
        }
    }
}
