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
    public class TabelaPrecoPCPServiceWeb: GenericServiceWeb<TabelaPrecoPCP, TabelaPrecoPCPRepository, TabelaPrecoPCPController>, ITabelaPrecoPCPService
    {

        public TabelaPrecoPCPServiceWeb(string requestUri) : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public TabelaPrecoPCP GetByReferencia(string referencia)
        {
            return null;
        }

        public IEnumerable<TabelaPrecoPCP> GetListPorDescricao(string desc)
        {
            var c = new ConnectionWebAPI<IEnumerable<TabelaPrecoPCP>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "descricao like '%" + desc + "%' And ativo = 1");
        }

        public IEnumerable<TabelaPrecoPCP> GetListPorReferencia(string referencia)
        {
            var c = new ConnectionWebAPI<IEnumerable<TabelaPrecoPCP>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "referencia like '%" + referencia + "%' And ativo = 1");
        }

        public void CalcularCustos(ref TabelaPrecoPCP tabela)
        {
            throw new NotImplementedException();
        }

        public void CalcularCustosComValoresManuais(ref TabelaPrecoPCP tabela)
        {
            throw new NotImplementedException();
        }

        public void CalcularCustosItemComValorManual(ref TabelaPrecoPCP tabela, Empresa empresa, Produto produto, ItemTabelaPrecoPCP item)
        {
            throw new NotImplementedException();
        }

        public void CalcularCustosItem(ref TabelaPrecoPCP tabela, Empresa empresa, decimal CustoMinutoMaoObra, decimal CustoMinutoPrevisto, decimal CustoMinutoRealizado, Produto produto, ItemTabelaPrecoPCP item)
        {
            throw new NotImplementedException();
        }
    }
}
