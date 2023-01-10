using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Controllers;
using Vestillo.Business.Repositories;
using System.Data;

namespace Vestillo.Business.Service
{
    public interface IConsultaService : IService<Consulta, ConsultaRepository, ConsultaController>
    {
        IEnumerable<Consulta> GetPorIdForm(int idForm);
        DataTable GetRetornoConsulta(string sql);
    }
}


