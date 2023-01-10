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
using Vestillo.Lib;

namespace Vestillo.Business.Service.Web
{
    public class ContasPagarServiceWeb: GenericServiceWeb<ContasPagar, ContasPagarRepository, ContasPagarController>, IContasPagarService
    {
        public ContasPagarServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public void Save(List<ContasPagar> parcelas)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ContasPagarView> GetAllView()
        {
            throw new NotImplementedException();
        }

        public ContasPagarView GetViewById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ContasPagarView> GetListaPorCampoEValor(string campoBusca, string valor)
        {
             throw new NotImplementedException();
        }

        public IEnumerable<TitulosBaixaLoteView> GetParcelasParaBaixaEmLote(int fornecedorId, DateTime? dataVencimentoInicial = null, DateTime? dataVencimentoFinal = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TitulosBaixaLoteView> GetParcelasParaEstornarBaixaEmLote(int contasPagarBaixaId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ContasPagarView> GetViewByReferencia(string referencia, int[] status = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TitulosView> GetParcelasRelatorio(FiltroRelatorioTitulos filtro)
        {
            throw new NotImplementedException();
        }
    }
}
