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
    public class TipoMovimentacaoServiceWeb: GenericServiceWeb<TipoMovimentacao, TipoMovimentacaoRepository, TipoMovimentacaoController>, ITipoMovimentacaoService
    {

        public TipoMovimentacaoServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<TipoMovimentacao> GetListByTipoEId(int tipo, int id, int SomenteAtivo)
        {
            string where;

            if (tipo == 0)
                where = "id = " + id;
            else
                where = "tipo = " + tipo + " And id = " + id;

            var t = new ConnectionWebAPI<TipoMovimentacao>(VestilloSession.UrlWebAPI);
            return t.Get(this.RequestUri + where.ToString());
        }


        public IEnumerable<TipoMovimentacao> GetPorDescricao(int tipo, string desc, int SomenteAtivo)
        {
            string where;

            if (tipo == 0)
                where = "descricao like '%" + desc + "%'";
            else
                where = "descricao like '%" + desc + "%' And tipo = " + tipo;

            var c = new ConnectionWebAPI<IEnumerable<TipoMovimentacao>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, where.ToString());
        }

        public IEnumerable<TipoMovimentacao> GetPorReferencia(int tipo, string referencia, int SomenteAtivo)
        {
            string where;

            if (tipo == 0)
                where = "referencia like '%" + referencia + "%'";
            else
                where = "referencia like '%" + referencia + "%' And tipo = " + tipo;

            var c = new ConnectionWebAPI<IEnumerable<TipoMovimentacao>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, where.ToString());
        }

        public int GetCountUso(int IdTipoMovimentacao)
        {
            throw new NotImplementedException();
        }
    }
}
