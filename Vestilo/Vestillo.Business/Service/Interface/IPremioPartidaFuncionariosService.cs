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
    public interface IPremioPartidaFuncionariosService : IService<PremioPartidaFuncionarios, PremioPartidaFuncionariosRepository, PremioPartidaFuncionariosController>
    {
        PremioPartidaFuncionarios GetByIdView(int id);
        IEnumerable<PremioPartidaFuncionariosView> GetByPremioView(int premioId);
        IEnumerable<PremioPartidaFuncionariosView> GetFuncionariosGrid(int? premioId);
        decimal CalculaPremioPartidaAssiduidade(DateTime DaData, DateTime AteData, int idFuncionario, PremioPartida premioPartida);
        IEnumerable<GrupoPremioPartidaDiario> CalculaPremioGrupoDiario(DateTime DaData, DateTime AteData, List<int> premioId);
        IEnumerable<GrupoPremioPartidaMedia> CalculaPremioGrupoMedia(DateTime DaData, DateTime AteData, List<int> premioId);
        IEnumerable<GrupoPremioPartidaDiario> CalculaPremioGrupoDiarioVisualBasic(DateTime DaData, DateTime AteData, List<int> premioId);


    }
}
