using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class ConsultaController: GenericController<Consulta, ConsultaRepository>
    {
        public DataTable GetRetornoConsulta(string sql)
        {
            using (ConsultaRepository repository = new ConsultaRepository())
            {
                return repository.GetRetornoConsulta(sql);
            }
        }

        public IEnumerable<Consulta> GetPorIdForm(int idForm)
        {
            using (ConsultaRepository repository = new ConsultaRepository())
            {
                return repository.GetPorIdForm(idForm);
            }
        }
    }
}
