using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Lib;

namespace Vestillo.Business.Service.Web
{
    public class NaturezaFinanceiraServiceWeb: GenericServiceWeb<NaturezaFinanceira, NaturezaFinanceiraRepository, NaturezaFinanceiraController>, INaturezaFinanceiraService
    {
        public NaturezaFinanceiraServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<NaturezaFinanceira> GetPorReferencia(string referencia)
        {
            var c = new ConnectionWebAPI<IEnumerable<NaturezaFinanceira>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "referencia like '%" + referencia + "%'");
        }

        public IEnumerable<NaturezaFinanceira> GetPorDescricao(string desc)
        {
            var c = new ConnectionWebAPI<IEnumerable<NaturezaFinanceira>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "descricao like '%" + desc + "%'");
        }

        public IEnumerable<NaturezaFinanceira> GetByIdList(int id)
        {
            var c = new ConnectionWebAPI<IEnumerable<NaturezaFinanceira>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "id = " + id);
        }
    }
}
