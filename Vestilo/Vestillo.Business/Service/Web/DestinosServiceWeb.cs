
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
    public class DestinosServiceWeb : GenericServiceWeb<Destinos, DestinosRepository, DestinosController>, IDestinosService
    {

        public DestinosServiceWeb(string requestUri) : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<Destinos> GetByAtivos(int AtivoInativo)
        {
            var c = new ConnectionWebAPI<Destinos>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri + "?Ativo=" + AtivoInativo.ToString());
        }

        public IEnumerable<Destinos> GetListPorDescricao(string desc)
        {
            var c = new ConnectionWebAPI<IEnumerable<Destinos>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "descricao like '%" + desc + "%' And ativo = 1");
        }

        public IEnumerable<Destinos> GetListPorReferencia(string Abreviatura)
        {
            var c = new ConnectionWebAPI<IEnumerable<Destinos>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "Abreviatura like '%" + Abreviatura + "%' And ativo = 1");
        }

        public IEnumerable<Destinos> GetListById(int id)
        {
            var c = new ConnectionWebAPI<IEnumerable<Destinos>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "id = " + id + " And ativo = 1");
        }
    }
}


