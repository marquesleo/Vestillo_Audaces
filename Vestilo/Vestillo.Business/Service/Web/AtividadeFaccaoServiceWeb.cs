
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
    public class AtividadeFaccaoServiceWeb : GenericServiceWeb<AtividadeFaccao, AtividadeFaccaoRepository, AtividadeFaccaoController>, IAtividadeFaccaoService
    {

        public AtividadeFaccaoServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<AtividadeFaccao> GetByAtivos(int AtivoInativo)
        {
            var o = new ConnectionWebAPI<AtividadeFaccao>(VestilloSession.UrlWebAPI);
            return o.Get(this.RequestUri + "?Ativo=" + AtivoInativo.ToString());
        }


        public IEnumerable<AtividadeFaccao> GetListPorDescricao(string desc)
        {
            var c = new ConnectionWebAPI<IEnumerable<AtividadeFaccao>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "descricao like '%" + desc + "%' And ativo = 1");
        }

        public IEnumerable<AtividadeFaccao> GetListPorReferencia(string referencia)
        {
            var c = new ConnectionWebAPI<IEnumerable<AtividadeFaccao>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "referencia like '%" + referencia + "%' And ativo = 1");
        }

        public IEnumerable<AtividadeFaccao> GetListById(int id)
        {
            var c = new ConnectionWebAPI<IEnumerable<AtividadeFaccao>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "id = " + id + " And ativo = 1");
        }
         
    }
}


