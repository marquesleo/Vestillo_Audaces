
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
    public class LogServiceWeb : GenericServiceWeb<Log , LogRepository, LogController>, ILogService
    {

        public LogServiceWeb(string requestUri) : base(requestUri)
        {
            this.RequestUri = requestUri;
        }



        public IEnumerable<LogView> GetCarregarAcoes(string Modulos, int operacao, DateTime DataInicio, DateTime DataFim)
        {          
            var Valor = "'" + DataInicio.ToString("yyyy-MM-dd") + "' AND '" + DataFim.ToString("yyyy-MM-dd") + "'";


            var SQL = new StringBuilder();
            SQL.AppendLine("SELECT *,usuarios.Nome as NomeUsuario, CONCAT(mid(log.Data,9,2), '/',mid(log.Data,6,2),'/',left(log.Data,4)) as DataAcao from log ");
            SQL.AppendLine("INNER JOIN usuarios on usuarios.id = log.UsuarioId ");
            SQL.AppendLine("WHERE SUBSTRING(log.Data,1,10)  BETWEEN " + Valor);
            SQL.AppendLine(" AND log.DescricaoOperacao like " + "'%" + Modulos + "%'");
            SQL.AppendLine(" AND log.DescricaoOperacao like " + "'%" + Modulos + "%'");
            SQL.AppendLine(" WHERE SUBSTRING(nfe.DataInclusao,1,10) BETWEEN " + Valor);


            var c = new ConnectionWebAPI<IEnumerable<LogView>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, SQL.ToString());
        }

        
    }
}



