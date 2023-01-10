using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class AlmoxarifadoController : GenericController<Almoxarifado, AlmoxarifadoRepository>
    {
        public IEnumerable<Almoxarifado> GetListPorDescricao(string Descricao)
        {
            using (AlmoxarifadoRepository repository = new AlmoxarifadoRepository())
            {
                return repository.GetListPorDescricao(Descricao);
            }
        }

        public IEnumerable<Almoxarifado> GetByEmpresa()
        {
            using (AlmoxarifadoRepository repository = new AlmoxarifadoRepository())
            {
                return repository.GetByEmpresa();
            }
        }
    }
}
