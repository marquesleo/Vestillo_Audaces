
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
    public class NotaEntradaServiceWeb : GenericServiceWeb<NotaEntrada , NotaEntradaRepository , NotaEntradaController >, INotaEntradaService
    {
        private string modificaWhere = "";

        public NotaEntradaServiceWeb(string requestUri): base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<NotaEntradaView> GetCamposEspecificos(string parametrosDaBusca)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<NotaEntradaView> GetListPorReferencia(string referencia, string parametrosDaBusca)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NotaEntradaView> GetListPorNumero(string Numero, string parametrosDaBusca)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NotaEntradaView> GetListPorPedido(int pedidoId)
        {
            throw new NotImplementedException();
        }
    }
}