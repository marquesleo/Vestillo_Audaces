using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Service.Web;

namespace Vestillo.Business.Service.Web
{
    public class PacoteProducaoServiceWeb: GenericServiceWeb<PacoteProducao, PacoteProducaoRepository, PacoteProducaoController>, IPacoteProducaoService
    {
        public PacoteProducaoServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public List<PacoteProducaoView> GetByView(bool CupomEletronico = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PacoteProducaoView> GetPacotesBrowse(Models.Views.FiltroRelatorioPacote filtro)
        {
            throw new NotImplementedException();
        }


        public List<PacoteProducaoView> GetByViewId(List<int> id)
        {
            throw new NotImplementedException();
        }


        public PacoteProducaoView GetByViewReferencia(string referencia)
        {
            throw new NotImplementedException();
        }


        public PacoteProducaoView GetByIdView(int id)
        {
            throw new NotImplementedException();
        }


        public void Save(ref PacoteProducaoView entity)
        {
            throw new NotImplementedException();
        }

        public List<PacoteProducaoView> GetByOrdemIdView(int ordemId)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<PacoteProducaoView> GetByListViewReferencia(string referencia, bool CupomEletronico = false)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<PacoteProducaoView> GetPacotesRelatorio(Models.Views.FiltroRelatorioPacote filtro)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<PacoteFuncionario> GetPacotesFuncionarioRelatorio(List<int> pacotes, List<int> funcionarios, string DaData, string AteData)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PacoteFaccao> GetPacotesFaccaoRelatorio(Models.Views.FiltroPacoteFaccao filtro)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<Models.Views.ControlePacoteView> GetControlePacotesRelatorio(Models.Views.FiltroControlePacote filtro)
        {
            throw new NotImplementedException();
        }
        public IEnumerable<PacoteFaccao> GetPacotesFaccaoFinalizados(Models.Views.FiltroPacoteFaccao filtro)
        {
            throw new NotImplementedException();
        }

        public void UpdateHeader(int pacoteId)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<PacoteProducaoView> GetByFiltroBalanceamento(Models.Views.FiltroRelatorioPacote filtro)
        {
            throw new NotImplementedException();
        }


        public PacoteProducaoView GetByViewReferenciaJunior(string referencia)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PacoteFaccaoValorizado> GetByPacoteFaccaoValorizado(DateTime DataInicio, DateTime DataFim, List<int> Faccao, int Tipo)
        {
            throw new NotImplementedException();
        }
    }
}
