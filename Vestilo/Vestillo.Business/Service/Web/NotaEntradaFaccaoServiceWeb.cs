
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
    public class NotaEntradaFaccaoServiceWeb : GenericServiceWeb<NotaEntradaFaccao, NotaEntradaFaccaoRepository, NotaEntradaFaccaoController>, INotaEntradaFaccaoService
    {
        private string modificaWhere = "";

        public NotaEntradaFaccaoServiceWeb(string requestUri) : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<NotaEntradaFaccaoView> GetCamposEspecificos(string parametrosDaBusca)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<NotaEntradaFaccaoView> GetListPorReferencia(string referencia, string parametrosDaBusca)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NotaEntradaFaccaoView> GetListPorNumero(string Numero, string parametrosDaBusca)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<NotaEntradaFaccaoView> GetListPorOrdem(int OrdemId)
        {
            throw new NotImplementedException();
        }
    }
}