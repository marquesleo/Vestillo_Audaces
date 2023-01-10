
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
    public class MovimentosDaOperacaoServiceWeb : GenericServiceWeb<MovimentosDaOperacao, MovimentosDaOperacaoRepository, MovimentosDaOperacaoController>, IMovimentosDaOperacaoService
    {

        public MovimentosDaOperacaoServiceWeb(string requestUri) : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<MovimentosDaOperacao> GetByAtivos(int AtivoInativo)
        {
            var o = new ConnectionWebAPI<MovimentosDaOperacao>(VestilloSession.UrlWebAPI);
            return o.Get(this.RequestUri + "?Ativo=" + AtivoInativo.ToString());
        }


        public IEnumerable<MovimentosDaOperacao> GetListPorDescricao(string desc)
        {
            var c = new ConnectionWebAPI<IEnumerable<MovimentosDaOperacao>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "descricao like '%" + desc + "%' And ativo = 1");
        }

        public IEnumerable<MovimentosDaOperacao> GetListPorReferencia(string referencia)
        {
            var c = new ConnectionWebAPI<IEnumerable<MovimentosDaOperacao>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "referencia like '%" + referencia + "%' And ativo = 1");
        }

        public IEnumerable<MovimentosDaOperacao> GetListById(int id)
        {
            var c = new ConnectionWebAPI<IEnumerable<MovimentosDaOperacao>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "id = " + id + " And ativo = 1");
        }
    }
}


