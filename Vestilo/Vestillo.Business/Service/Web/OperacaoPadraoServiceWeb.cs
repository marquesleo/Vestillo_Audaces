

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
    public class OperacaoPadraoServiceWeb : GenericServiceWeb<OperacaoPadrao, OperacaoPadraoRepository, OperacaoPadraoController>, IOperacaoPadraoService
    {

        public OperacaoPadraoServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<OperacaoPadrao> GetByAtivos(int AtivoInativo)
        {
            var o = new ConnectionWebAPI<OperacaoPadrao>(VestilloSession.UrlWebAPI);
            return o.Get(this.RequestUri + "?Ativo=" + AtivoInativo.ToString());
        }


        public IEnumerable<OperacaoPadrao> GetListPorDescricao(string desc)
        {
            var c = new ConnectionWebAPI<IEnumerable<OperacaoPadrao>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "descricao like '%" + desc + "%' And ativo = 1");
        }

        public IEnumerable<OperacaoPadrao> GetListPorReferencia(string referencia)
        {
            var c = new ConnectionWebAPI<IEnumerable<OperacaoPadrao>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "referencia like '%" + referencia + "%' And ativo = 1");
        }

        public IEnumerable<OperacaoPadrao> GetListById(int id)
        {
            var c = new ConnectionWebAPI<IEnumerable<OperacaoPadrao>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "id = " + id + " And ativo = 1");
        }

        public IEnumerable<OperacaoPadrao> GetByAllSetorBal()
        {
            throw new NotImplementedException();
        }
    }
}


