
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Repositories;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;

namespace Vestillo.Business.Service
{
    public interface IPercentuaisEmpresasService : IService<PercentuaisEmpresas, PercentuaisEmpresasRepository, PercentuaisEmpresasController>
    {
        PercentuaisEmpresasView GetEmpresaLogada(int Id);
        PercentuaisEmpresas GetByEmpresaLogada(int Id);
    }
}
