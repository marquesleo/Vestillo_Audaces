
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
    public class AdmCartaoServiceWeb : GenericServiceWeb<AdmCartao, AdmCartaoRepository, AdmCartaoController>, IAdmCartaoService
    {

        public AdmCartaoServiceWeb(string requestUri) : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<AdmCartao> GetByAtivos(int AtivoInativo)
        {
            var adm = new ConnectionWebAPI<AdmCartao>(VestilloSession.UrlWebAPI);
            return adm.Get(this.RequestUri + "?Ativo=" + AtivoInativo.ToString());
        }


        public IEnumerable<AdmCartao> GetPorReferencia(string referencia)
        {

            var c = new ConnectionWebAPI<IEnumerable<AdmCartao>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "referencia like '%" + referencia + "%' And ativo = 1");
        }

        public IEnumerable<AdmCartao> GetPornome(string desc)
        {

            var c = new ConnectionWebAPI<IEnumerable<AdmCartao>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "nome like '%" + desc + "%' And ativo = 1");
            
        }

        public IEnumerable<AdmCartao> GetByIdList(int id)
        {
            var c = new ConnectionWebAPI<IEnumerable<AdmCartao>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "id = " + id);
        }
    }
}



