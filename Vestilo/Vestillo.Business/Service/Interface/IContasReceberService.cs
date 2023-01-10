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
    public interface IContasReceberService : IService<ContasReceber , ContasReceberRepository, ContasReceberController>
    {
        void Save(List<ContasReceber> parcelas);
        IEnumerable<ContasReceberView> GetAllView();
        IEnumerable<ContasReceberView> GetViewByReferencia(string referencia, int[] status = null,bool SomenteParaBoleto = false, int BancoPortador = 0);
        ContasReceberView GetViewById(int id);
        IEnumerable<ContasReceberView> GetListaPorCampoEValor(string campoBusca, string valor, bool CarregarSomenteAval = false);
        IEnumerable<TitulosBaixaLoteView> GetParcelasParaBaixaEmLote(int clienteId, DateTime? dataVencimentoInicial = null, DateTime? dataVencimentoFinal = null);
        IEnumerable<TitulosBaixaLoteView> GetParcelasParaEstornarBaixaEmLote(int contasReceberBaixaId);
        IEnumerable<TitulosView> GetParcelasRelatorio(FiltroRelatorioTitulos filtro);
        IEnumerable<ContasReceber> GetAllTitulosFilhos(int TituloPai);
        IEnumerable<ContasReceber> GetAllTitulosBaixados(int Titulo);
        IEnumerable<ReceitasFuturasView> GetReceitaFutura(int[] Ano, bool UnirEmpresas);




    }
}
