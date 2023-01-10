
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class PercentuaisEmpresasController : GenericController<PercentuaisEmpresas, PercentuaisEmpresasRepository>
    {

        public PercentuaisEmpresasView GetEmpresaLogada(int Id)
        {
            using (PercentuaisEmpresasRepository repository = new PercentuaisEmpresasRepository())
            {
                return repository.GetEmpresaLogada(Id);
            }

        }

        public PercentuaisEmpresas GetByEmpresaLogada(int Id)
        {
            using (PercentuaisEmpresasRepository repository = new PercentuaisEmpresasRepository())
            {
                return repository.GetByEmpresaLogada(Id);
            }

        }


    }
}
