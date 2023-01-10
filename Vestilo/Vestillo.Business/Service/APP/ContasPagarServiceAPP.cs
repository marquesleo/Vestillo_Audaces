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
    public class ContasPagarServiceAPP: GenericServiceAPP<ContasPagar, ContasPagarRepository, ContasPagarController>, IContasPagarService
    {
        public ContasPagarServiceAPP()
            : base(new ContasPagarController())
        {
        }

        public void Save(List<ContasPagar> parcelas)
        {
            ContasPagarController controller = new ContasPagarController();
            controller.Save(parcelas);
        }

        public IEnumerable<ContasPagarView> GetAllView()
        {
            ContasPagarController controller = new ContasPagarController();
            return controller.GetAllView();
        }

        public ContasPagarView GetViewById(int id)
        {
            ContasPagarController controller = new ContasPagarController();
            return controller.GetViewById(id);
        }

        public IEnumerable<ContasPagarView> GetListaPorCampoEValor(string campoBusca, string valor)
        {
            ContasPagarController controller = new ContasPagarController();
            return controller.GetListaPorCampoEValor(campoBusca, valor);
        }

        public IEnumerable<TitulosBaixaLoteView> GetParcelasParaBaixaEmLote(int fornecedorId, DateTime? dataVencimentoInicial = null, DateTime? dataVencimentoFinal = null)
        {
            ContasPagarController controller = new ContasPagarController();
            return controller.GetParcelasParaBaixaEmLote(fornecedorId, dataVencimentoInicial, dataVencimentoFinal);
        }

        public IEnumerable<TitulosBaixaLoteView> GetParcelasParaEstornarBaixaEmLote(int contasPagarBaixaId)
        {
            ContasPagarController controller = new ContasPagarController();
            return controller.GetParcelasParaEstornarBaixaEmLote(contasPagarBaixaId);
        }
        
        public IEnumerable<ContasPagarView> GetViewByReferencia(string referencia, int[] status = null)
        {
            ContasPagarController controller = new ContasPagarController();
            return controller.GetViewByReferencia(referencia, status);
        }

        public IEnumerable<TitulosView> GetParcelasRelatorio(FiltroRelatorioTitulos filtro)
        {
            ContasPagarController controller = new ContasPagarController();
            return controller.GetParcelasRelatorio(filtro);
        }
    }
}
