using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Service.APP
{
    public class PremioPartidaFuncionariosServiceAPP : GenericServiceAPP<PremioPartidaFuncionarios, PremioPartidaFuncionariosRepository, PremioPartidaFuncionariosController>, IPremioPartidaFuncionariosService
    {
        public PremioPartidaFuncionariosServiceAPP()
            : base(new PremioPartidaFuncionariosController())
        {
        }

        public PremioPartidaFuncionarios GetByIdView(int id)
        {
            PremioPartidaFuncionariosController controller = new PremioPartidaFuncionariosController();
            return controller.GetByIdView(id);
        }

        public IEnumerable<PremioPartidaFuncionariosView> GetByPremioView(int premioId)
        {
            PremioPartidaFuncionariosController controller = new PremioPartidaFuncionariosController();
            return controller.GetByPremioView(premioId);
        }

        public IEnumerable<PremioPartidaFuncionariosView> GetFuncionariosGrid(int? premioId)
        {
            PremioPartidaFuncionariosController controller = new PremioPartidaFuncionariosController();
            return controller.GetFuncionariosGrid(premioId);
        }

        public decimal CalculaPremioPartidaAssiduidade(DateTime DaData, DateTime AteData, int idFuncionario, PremioPartida premioPartida)
        {
            PremioPartidaFuncionariosController controller = new PremioPartidaFuncionariosController();
            return controller.CalculaPremioPartidaAssiduidade(DaData, AteData, idFuncionario, premioPartida);
        }

        public IEnumerable<GrupoPremioPartidaDiario> CalculaPremioGrupoDiario(DateTime DaData, DateTime AteData, List<int> premioId)
        {
            PremioPartidaFuncionariosController controller = new PremioPartidaFuncionariosController();
            return controller.CalculaPremioGrupoDiario(DaData, AteData, premioId);
        }

        public IEnumerable<GrupoPremioPartidaMedia> CalculaPremioGrupoMedia(DateTime DaData, DateTime AteData, List<int> premioId)
        {
            PremioPartidaFuncionariosController controller = new PremioPartidaFuncionariosController();
            return controller.CalculaPremioGrupoMedia(DaData, AteData, premioId);
        }

        public IEnumerable<GrupoPremioPartidaDiario> CalculaPremioGrupoDiarioVisualBasic(DateTime DaData, DateTime AteData, List<int> premioId)
        {
            PremioPartidaFuncionariosController controller = new PremioPartidaFuncionariosController();
            return controller.CalculaPremioGrupoDiarioVisualBasic(DaData, AteData, premioId);
        }

    }
}
