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
    public class TabelaPrecoServiceWeb: GenericServiceWeb<TabelaPreco, TabelaPrecoRepository, TabelaPrecoController>, ITabelaPrecoService
    {

        public TabelaPrecoServiceWeb(string requestUri) : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public TabelaPreco GetByReferencia(string referencia)
        {
            return null;
        }

        public IEnumerable<TabelaPreco> GetListPorDescricao(string desc)
        {
            var c = new ConnectionWebAPI<IEnumerable<TabelaPreco>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "descricao like '%" + desc + "%' And ativo = 1");
        }

        public IEnumerable<TabelaPreco> GetListPorReferencia(string referencia)
        {
            var c = new ConnectionWebAPI<IEnumerable<TabelaPreco>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "referencia like '%" + referencia + "%' And ativo = 1");
        }

        public void CalcularCustos(ref ItemTabelaPrecoView item, decimal percentualImpostosEEncargos, decimal margemLucro)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<TabelaPreco> GetAllView()
        {
            throw new NotImplementedException();
        }
    }
}
