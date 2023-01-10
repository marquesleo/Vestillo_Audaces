
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
    public class SetoresServiceWeb : GenericServiceWeb<Setores, SetoresRepository, SetoresController>, ISetoresService
    {

        public SetoresServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<Setores> GetByAtivos(int AtivoInativo)
        {
            var c = new ConnectionWebAPI<Setores>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri + "?Ativo=" + AtivoInativo.ToString());
        }

        public IEnumerable<Setores> GetListPorDescricao(string desc)
        {
            var c = new ConnectionWebAPI<IEnumerable<Setores>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "descricao like '%" + desc + "%' And ativo = 1");
        }

        public IEnumerable<Setores> GetListPorReferencia(string Abreviatura)
        {
            var c = new ConnectionWebAPI<IEnumerable<Setores>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "Abreviatura like '%" + Abreviatura + "%' And ativo = 1");
        }

        public IEnumerable<Setores> GetListById(int id)
        {
            var c = new ConnectionWebAPI<IEnumerable<Setores>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "id = " + id + " And ativo = 1");
        }


        public IEnumerable<Setores> GetBalanceamentos()
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Setores> GetByAtivosBalanceamento(int AtivoInativo)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Setores> GetListPorReferenciaBalanceamento(string Abreviatura)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Setores> GetListPorDescricaoBalanceamento(string desc)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Setores> GetListByIdBalanceamento(int id)
        {
            throw new NotImplementedException();
        }


    }
}


