using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service
{
    public interface IContasPagarService : IService<ContasPagar , ContasPagarRepository, ContasPagarController>

    {
        void Save(List<ContasPagar> parcelas);
        IEnumerable<ContasPagarView> GetAllView();
        ContasPagarView GetViewById(int id);
        IEnumerable<ContasPagarView> GetListaPorCampoEValor(string campoBusca, string valor);
        IEnumerable<TitulosBaixaLoteView> GetParcelasParaBaixaEmLote(int fornecedorId, DateTime? dataVencimentoInicial = null, DateTime? dataVencimentoFinal = null);
        IEnumerable<TitulosBaixaLoteView> GetParcelasParaEstornarBaixaEmLote(int contasPagarBaixaId);
        IEnumerable<ContasPagarView> GetViewByReferencia(string referencia, int[] status = null);
        IEnumerable<TitulosView> GetParcelasRelatorio(FiltroRelatorioTitulos filtro);
        
    }
}
