using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.Interface;
using Vestillo.Lib;

namespace Vestillo.Business.Service.Web
{
    public class ServicoServiceWeb: GenericServiceWeb<Servico, ServicoRepository, ServicoController>, IServicoService
    {
        public ServicoServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<Servico> GetPorReferencia(string referencia)
        {
            var c = new ConnectionWebAPI<IEnumerable<Servico>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "referencia like '%" + referencia + "%' And ativo = 1");
        }

        public IEnumerable<Servico> GetPorDescricao(string desc)
        {
            var c = new ConnectionWebAPI<IEnumerable<Servico>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "descricao like '%" + desc + "%' And ativo = 1");
        }

        public IEnumerable<Servico> GetByIdList(int id)
        {
            var c = new ConnectionWebAPI<IEnumerable<Servico>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "id = " + id);
        }

    }
}
