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
    public class CfopServiceWeb: GenericServiceWeb<Cfop, CfopRepository, CfopController>, ICfopService
    {
        public CfopServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<Cfop> GetPorReferencia(string referencia, String TipoCfop)
        {
            var c = new ConnectionWebAPI<IEnumerable<Cfop>>(VestilloSession.UrlWebAPI);

            if (TipoCfop.Trim().ToUpper().RemoverAcentos().Equals("ENTRADA"))
            {
                return c.Get(this.RequestUri, "referencia like '%" + referencia + "%' And (referencia >= '1101' and referencia <= '3949') order by referencia");
            } if (TipoCfop.Trim().ToUpper().RemoverAcentos().Equals("SAIDA"))
            {
                return c.Get(this.RequestUri, "referencia like '%" + referencia + "%' And (referencia >= '5101' and referencia <= '7949') order by referencia");
            }

            return c.Get(this.RequestUri, "referencia like '%" + referencia + "%'");

        }

        public IEnumerable<Cfop> GetPorDescricao(string desc, String TipoCfop)
        {
            var c = new ConnectionWebAPI<IEnumerable<Cfop>>(VestilloSession.UrlWebAPI);

            if (TipoCfop.Trim().ToUpper().RemoverAcentos().Equals("ENTRADA"))
            {
                return c.Get(this.RequestUri, "descricao like '%" + desc + "%' And (referencia >= '1101' and referencia <= '3949') order by referencia");
            } if (TipoCfop.Trim().ToUpper().RemoverAcentos().Equals("SAIDA"))
            {
                return c.Get(this.RequestUri, "descricao like '%" + desc + "%' And (referencia >= '5101' and referencia <= '7949') order by referencia");
            }

            return c.Get(this.RequestUri, "descricao like '%" + desc + "%'");
        }

    }
}
