using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service
{
    public interface IRotaVisitaService : IService<RotaVisita, RotaVisitaRepository, RotaVisitaController>
    {
        IEnumerable<RotaVisita> GetPorReferencia(String referencia);

        IEnumerable<RotaVisita> GetPorDescricao(String desc);

        IEnumerable<RotaVisita> GetByIdList(int id);
    }
}
