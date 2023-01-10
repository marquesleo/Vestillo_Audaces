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
    public class CaixasServiceWeb : GenericServiceWeb<Caixas, CaixasRepository, CaixasController>, ICaixasService
    {

        public CaixasServiceWeb(string requestUri) : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<Caixas> GetByAtivos(int AtivoInativo)
        {
            var c = new ConnectionWebAPI<Caixas>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri + "?Ativo=" + AtivoInativo.ToString());
        }

        public IEnumerable<Caixas> GetListPorDescricao(string desc)
        {
            var c = new ConnectionWebAPI<IEnumerable<Caixas>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "descricao like '%" + desc + "%' And ativo = 1");
        }

        public IEnumerable<Caixas> GetListPorReferencia(string Abreviatura)
        {
            var c = new ConnectionWebAPI<IEnumerable<Caixas>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "Abreviatura like '%" + Abreviatura + "%' And ativo = 1");
        }

        public IEnumerable<Caixas> GetListById(int id)
        {
            var c = new ConnectionWebAPI<IEnumerable<Caixas>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "id = " + id + " And ativo = 1");
        }

        public IEnumerable<Caixas> GetAllTrataHoras()
        {
            throw new NotImplementedException();
        }

        public Caixas GetByIdTrataHoras(int id)
        {
            throw new NotImplementedException();
        }
    }
}



