
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
    public class ExcecaoCalendarioServiceWeb : GenericServiceWeb<ExcecaoCalendario, ExcecaoCalendarioRepository, ExcecaoCalendarioController>, IExcecaoCalendarioService
    {
        public ExcecaoCalendarioServiceWeb(string requestUri) : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<ExcecaoCalendario> GetPorReferencia(string referencia)
        {
            var c = new ConnectionWebAPI<IEnumerable<ExcecaoCalendario>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "referencia like '%" + referencia + "%' And ativo = 1");
        }

        public IEnumerable<ExcecaoCalendario> GetPorDescricao(string desc)
        {
            var c = new ConnectionWebAPI<IEnumerable<ExcecaoCalendario>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "descricao like '%" + desc + "%' And ativo = 1");
        }

        public ExcecaoCalendario GetByDataExcecao(int CalendarioId, DateTime data)
        {
            var c = new ConnectionWebAPI<ExcecaoCalendario>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "CalendarioId = " + CalendarioId + "DataDaExcecao " + data.ToString("yyyy-MM-dd") + "'");
        }
    }


}
