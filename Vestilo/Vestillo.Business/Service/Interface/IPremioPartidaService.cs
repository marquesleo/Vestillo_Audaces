using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Service
{
    public interface IPremioPartidaService : IService<PremioPartida, PremioPartidaRepository, PremioPartidaController>
    {
        PremioPartida GetByIdView(int id);
        IEnumerable<PremioPartida> GetByDescricao(string descricao);
        IEnumerable<PremioPartida> GetByReferencia(string referencia);
    }
}
