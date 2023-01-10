
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service.APP
{
    public class LogServiceAPP : GenericServiceAPP<Log, LogRepository, LogController>, ILogService
    {

        public LogServiceAPP() : base(new LogController())
        {
        }

        public IEnumerable<LogView> GetCarregarAcoes(string Modulos, int operacao, DateTime DataInicio, DateTime DataFim)
        {
            LogController controller = new LogController();
            return controller.GetCarregarAcoes( Modulos,  operacao,  DataInicio,  DataFim);
        }


    }
}



