using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Service.APP;

namespace Vestillo.Business.Service.APP
{
    public class PacoteProducaoServiceAPP: GenericServiceAPP<PacoteProducao, PacoteProducaoRepository, PacoteProducaoController>, IPacoteProducaoService
    {
        public PacoteProducaoServiceAPP()
            : base(new PacoteProducaoController())
        {

        }

        public List<PacoteProducaoView> GetByView(bool CupomEletronico = false)
        {
            PacoteProducaoController controller = new PacoteProducaoController();
            return controller.GetByView(CupomEletronico);
        }

        public IEnumerable<PacoteProducaoView> GetPacotesBrowse(Models.Views.FiltroRelatorioPacote filtro)
        {
            PacoteProducaoController controller = new PacoteProducaoController();
            return controller.GetPacotesBrowse(filtro);
        }

        public List<PacoteProducaoView> GetByViewId(List<int> id)
        {
            PacoteProducaoController controller = new PacoteProducaoController();
            return controller.GetByIdView(id);
        }

        public List<PacoteProducaoView> GetByOrdemIdView(int ordemId)
        {
            PacoteProducaoController controller = new PacoteProducaoController();
            return controller.GetByOrdemIdView(ordemId);
        }


        public PacoteProducaoView GetByViewReferencia(string referencia)
        {
            PacoteProducaoController controller = new PacoteProducaoController();
            return controller.GetByViewReferencia(referencia);
        }

        public PacoteProducaoView GetByViewReferenciaJunior(string referencia)
        {
            PacoteProducaoController controller = new PacoteProducaoController();
            return controller.GetByViewReferenciaJunior(referencia);
        }


        public PacoteProducaoView GetByIdView(int id)
        {
            PacoteProducaoController controller = new PacoteProducaoController();
            return controller.GetByIdView(id);
        }


        public void Save(ref PacoteProducaoView entity)
        {
            PacoteProducaoController controller = new PacoteProducaoController();
            controller.Save(ref entity);
        }


        public IEnumerable<PacoteProducaoView> GetByListViewReferencia(string referencia, bool CupomEletronico = false)
        {
            PacoteProducaoController controller = new PacoteProducaoController();
            return controller.GetByListViewReferencia(referencia, CupomEletronico);
        }


        public IEnumerable<PacoteProducaoView> GetPacotesRelatorio(Models.Views.FiltroRelatorioPacote filtro)
        {
            PacoteProducaoController controller = new PacoteProducaoController();
            return controller.GetPacotesRelatorio(filtro);
        }

        public IEnumerable<Models.Views.ControlePacoteView> GetControlePacotesRelatorio(Models.Views.FiltroControlePacote filtro)
        {
            PacoteProducaoController controller = new PacoteProducaoController();
            return controller.GetControlePacotesRelatorio(filtro);
        }

        public IEnumerable<PacoteProducaoView> GetByFiltroBalanceamento(Models.Views.FiltroRelatorioPacote filtro)
        {
            PacoteProducaoController controller = new PacoteProducaoController();
            return controller.GetByFiltroBalanceamento(filtro);
        }


        public IEnumerable<PacoteFuncionario> GetPacotesFuncionarioRelatorio(List<int> pacotes, List<int> funcionarios, string DaData, string AteData)
        {
            PacoteProducaoController controller = new PacoteProducaoController();
            return controller.GetPacotesFuncionarioRelatorio(pacotes, funcionarios, DaData, AteData);
        }

        public IEnumerable<PacoteFaccao> GetPacotesFaccaoRelatorio(Models.Views.FiltroPacoteFaccao filtro)
        {
            PacoteProducaoController controller = new PacoteProducaoController();
            return controller.GetPacotesFaccaoRelatorio(filtro);
        }

        public IEnumerable<PacoteFaccao> GetPacotesFaccaoFinalizados(Models.Views.FiltroPacoteFaccao filtro)
        {
            PacoteProducaoController controller = new PacoteProducaoController();
            return controller.GetPacotesFaccaoFinalizados(filtro);
        }

        public void UpdateHeader(int pacoteId)
        {
            PacoteProducaoController controller = new PacoteProducaoController();
            controller.UpdateHeader(pacoteId);
        }

        public IEnumerable<PacoteFaccaoValorizado> GetByPacoteFaccaoValorizado(DateTime DataInicio, DateTime DataFim, List<int> Faccao, int Tipo)
        {
            PacoteProducaoController controller = new PacoteProducaoController();
            return controller.GetByPacoteFaccaoValorizado(DataInicio, DataFim, Faccao, Tipo);
        }
    }
}
