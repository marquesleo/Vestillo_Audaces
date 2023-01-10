using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class LogController : GenericController<Log, LogRepository>
    {

        public IEnumerable<LogView> GetCarregarAcoes(string Modulos, int operacao, DateTime DataInicio, DateTime DataFim)
        {
            using (var repository = new LogRepository())
            {
                return repository.GetCarregarAcoes( Modulos,  operacao,  DataInicio,  DataFim);
            }
        }
    }
     
         
}
