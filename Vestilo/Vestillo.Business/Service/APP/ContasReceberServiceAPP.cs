using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.Interface;

namespace Vestillo.Business.Service.APP
{
    public class ContasReceberServiceAPP: GenericServiceAPP<ContasReceber, ContasReceberRepository, ContasReceberController>, IContasReceberService
    {
        public ContasReceberServiceAPP()
            : base(new ContasReceberController())
        {
        }

        public void Save(List<ContasReceber> parcelas)
        {
            ContasReceberController controller = new ContasReceberController();
            controller.Save(parcelas);
        }

        public IEnumerable<ContasReceberView> GetAllView()
        {
            ContasReceberController controller = new ContasReceberController();
            return controller.GetAllView();
        }

        public ContasReceberView GetViewById(int id)
        {
            ContasReceberController controller = new ContasReceberController();
            return controller.GetViewById(id);
        }

        public IEnumerable<ContasReceberView> GetListaPorCampoEValor(string campoBusca, string valor, bool CarregarSomenteAval = false)
        {
            ContasReceberController controller = new ContasReceberController();
            return controller.GetListaPorCampoEValor(campoBusca, valor, CarregarSomenteAval);
        }

        public IEnumerable<ContasReceberView> GetViewByReferencia(string referencia, int[] status = null, bool SomenteParaBoleto = false, int BancoPortador = 0)
        {
            return controller.GetViewByReferencia(referencia, status, SomenteParaBoleto,BancoPortador);
        }

        public IEnumerable<TitulosBaixaLoteView> GetParcelasParaBaixaEmLote(int clienteId, DateTime? dataVencimentoInicial = null, DateTime? dataVencimentoFinal = null)
        {
            return controller.GetParcelasParaBaixaEmLote(clienteId, dataVencimentoInicial, dataVencimentoFinal);
        }

        public IEnumerable<TitulosBaixaLoteView> GetParcelasParaEstornarBaixaEmLote(int contasReceberBaixaId)
        {
            return controller.GetParcelasParaEstornarBaixaEmLote(contasReceberBaixaId);
        }

        public IEnumerable<TitulosView> GetParcelasRelatorio(FiltroRelatorioTitulos filtro)
        {
            ContasReceberController controller = new ContasReceberController();
            return controller.GetParcelasRelatorio(filtro);
        }

        public IEnumerable<ContasReceber> GetAllTitulosFilhos(int TituloPai)
        {
            ContasReceberController controller = new ContasReceberController();
            return controller.GetAllTitulosFilhos(TituloPai);
        }

        public IEnumerable<ContasReceber> GetAllTitulosBaixados(int Titulo)
        {
            ContasReceberController controller = new ContasReceberController();
            return controller.GetAllTitulosBaixados(Titulo);
        }

        public IEnumerable<ReceitasFuturasView> GetReceitaFutura(int[] Ano, bool UnirEmpresas)
        {
            ContasReceberController controller = new ContasReceberController();
            return controller.GetReceitaFutura(Ano, UnirEmpresas);
        }
    }
}
