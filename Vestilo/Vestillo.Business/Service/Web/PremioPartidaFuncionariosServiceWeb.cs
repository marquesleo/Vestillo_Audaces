using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.APP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Service.Web
{
    public class PremioPartidaFuncionariosServiceWeb : GenericServiceWeb<PremioPartidaFuncionarios, PremioPartidaFuncionariosRepository, PremioPartidaFuncionariosController>, IPremioPartidaFuncionariosService
    {
        public PremioPartidaFuncionariosServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public PremioPartidaFuncionarios GetByIdView(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PremioPartidaFuncionariosView> GetByPremioView(int premioId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PremioPartidaFuncionariosView> GetFuncionariosGrid(int? premioId)
        {
            throw new NotImplementedException();
        }

        public decimal CalculaPremioPartidaAssiduidade(DateTime DaData, DateTime AteData, int idFuncionario, PremioPartida premioPartida)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GrupoPremioPartidaDiario> CalculaPremioGrupoDiario(DateTime DaData, DateTime AteData, List<int> premioId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GrupoPremioPartidaMedia> CalculaPremioGrupoMedia(DateTime DaData, DateTime AteData, List<int> premioId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GrupoPremioPartidaDiario> CalculaPremioGrupoDiarioVisualBasic(DateTime DaData, DateTime AteData, List<int> premioId)
        {
            throw new NotImplementedException();
        }

    }
}
