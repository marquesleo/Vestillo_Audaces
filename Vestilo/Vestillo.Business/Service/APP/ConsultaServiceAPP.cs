using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service.APP
{
    public class ConsultaServiceAPP : GenericServiceAPP<Consulta, ConsultaRepository, ConsultaController>, IConsultaService
    {
        public ConsultaServiceAPP() : base(new ConsultaController())
        {
        }       

        public IEnumerable<Consulta> GetPorIdForm(int IdForm)
        {
            return controller.GetPorIdForm(IdForm);
        }
        
        public DataTable GetRetornoConsulta(string sql)
        {
            return controller.GetRetornoConsulta(sql);
        }
    }
}