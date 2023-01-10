using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Lib;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System.Data;


namespace Vestillo.Business.Service.Web
{
    public class ConsultaServiceWeb : GenericServiceWeb<Consulta, ConsultaRepository, ConsultaController>,  IConsultaService
    {
        public ConsultaServiceWeb(string requestUri)  : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<Consulta> GetPorIdForm(int IdForm)
        {
            var c = new ConnectionWebAPI<Consulta>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri + @"?IdForm=" + IdForm.ToString());
        }
        
        public DataTable GetRetornoConsulta(string sql)
        {
            throw new NotImplementedException();
        }
    }
}


