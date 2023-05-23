using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Service;
using Vestillo.Business.Models.Views;

namespace Vestillo.Business.Service
{
    public interface IPacoteProducaoService : IService<PacoteProducao, PacoteProducaoRepository, PacoteProducaoController>
    {
        List<PacoteProducaoView> GetByView(bool CupomEletronico = false);
        IEnumerable<PacoteProducaoView> GetPacotesBrowse(FiltroRelatorioPacote filtro);
        List<PacoteProducaoView> GetByViewId(List<int> id);
        PacoteProducaoView GetByIdView(int id);
        List<PacoteProducaoView> GetByOrdemIdView(int ordemId);
        PacoteProducaoView GetByViewReferencia(string referencia);
        PacoteProducaoView GetByViewReferenciaJunior(string referencia);
        IEnumerable<PacoteProducaoView> GetByListViewReferencia(string referencia,bool CupomEletronico);
        IEnumerable<PacoteProducaoView> GetPacotesRelatorio(FiltroRelatorioPacote filtro);
        IEnumerable<PacoteProducaoView> GetByFiltroBalanceamento(FiltroRelatorioPacote filtro);
        IEnumerable<PacoteFuncionario> GetPacotesFuncionarioRelatorio(List<int> pacotes, List<int> funcionarios, string DaData, string AteData);
        IEnumerable<PacoteFaccao> GetPacotesFaccaoRelatorio(FiltroPacoteFaccao filtro);
        IEnumerable<ControlePacoteView> GetControlePacotesRelatorio(FiltroControlePacote filtro);
        IEnumerable<PacoteFaccao> GetPacotesFaccaoFinalizados(FiltroPacoteFaccao filtro);
        void Save(ref PacoteProducaoView entity);
        void UpdateHeader(int pacoteId);
        IEnumerable<PacoteFaccaoValorizado> GetByPacoteFaccaoValorizado(DateTime DataInicio, DateTime DataFim, List<int> Faccao, int Tipo);
    }
}
