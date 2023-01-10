
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
    public class TotaisCaixasServiceWeb : GenericServiceWeb<TotaisCaixas, TotaisCaixasRepository, TotaisCaixasController>, ITotaisCaixasService
    {
        public TotaisCaixasServiceWeb(string requestUri): base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<TotaisCaixas> GetPorNumCaixa(string numCaixa)
        {
            var c = new ConnectionWebAPI<IEnumerable<TotaisCaixas>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "numbanco like '%" + numCaixa + "%' And ativo = 1");
        }

        public IEnumerable<TotaisCaixas> GetByIdList(int idCaixa)
        {
            var c = new ConnectionWebAPI<IEnumerable<TotaisCaixas>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "descricao like '%" + idCaixa + "%' And ativo = 1");
        }

        public IEnumerable<TotaisCaixasView> GetPorData(string numCaixa, DateTime dataInicial, DateTime dataFinal)
        {
            var c = new ConnectionWebAPI<IEnumerable<TotaisCaixasView>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "id = " + numCaixa);
        }
    }
}
