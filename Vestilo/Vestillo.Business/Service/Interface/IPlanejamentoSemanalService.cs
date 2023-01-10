using System.Collections.Generic;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models.Views;

namespace Vestillo.Business.Service
{
    public interface IPlanejamentoSemanalService : IService<PlanejamentoSemanal, PlanejamentoSemanalRepository, PlanejamentoSemanalController>
    {
        IEnumerable<PlanejamentoSemanal> GetByRefView(string referencia);
    }
}
