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
    public class CondPagamentoServiceWeb : GenericServiceWeb<CondPagamento, CondPagamentoRepository, CondPagamentoController>, ICondPagamentoService
    {
        public CondPagamentoServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<CondPagamento> GetPorReferencia(string referencia)
        {
            var c = new ConnectionWebAPI<IEnumerable<CondPagamento>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "referencia like '%" + referencia + "%' And ativo = 1");
        }

        public IEnumerable<CondPagamento> GetPorDescricao(string desc)
        {
            var c = new ConnectionWebAPI<IEnumerable<CondPagamento>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "descricao like '%" + desc + "%' And ativo = 1");
        }

        public IEnumerable<CondPagamento> GetByIdList(int id)
        {
            var c = new ConnectionWebAPI<IEnumerable<CondPagamento>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "id = " + id);
        }
    }


}
