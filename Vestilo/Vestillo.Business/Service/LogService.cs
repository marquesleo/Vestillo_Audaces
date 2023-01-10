
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Controllers;
using Vestillo.Business.Service.APP;
using Vestillo.Business.Service.Web;

namespace Vestillo.Business.Service
{
    public class LogService : GenericService<Log, LogRepository, LogController>
    {
        public LogService()
        {
            base.RequestUri = "Log";
        }

        public new ILogService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new LogServiceWeb(this.RequestUri);
            }
            else
            {
                return new LogServiceAPP();
            }
        }   
    }
}