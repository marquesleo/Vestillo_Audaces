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
    public class ContasReceberServiceWeb: GenericServiceWeb<ContasReceber, ContasReceberRepository, ContasReceberController>, IContasReceberService
    {
        public ContasReceberServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public void Save(List<ContasReceber> parcelas)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ContasReceberView> GetAllView()
        {
            throw new NotImplementedException();
        }

        public ContasReceberView GetViewById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ContasReceberView> GetListaPorCampoEValor(string campoBusca, string valor, bool CarregarSomenteAval)
        {
             throw new NotImplementedException();
        }

        public IEnumerable<ContasReceberView> GetViewByReferencia(string referencia, int[] status = null, bool SomenteParaBoleto = false, int BancoPortador = 0)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TitulosBaixaLoteView> GetParcelasParaBaixaEmLote(int clienteId, DateTime? dataVencimentoInicial = null, DateTime? dataVencimentoFinal = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TitulosBaixaLoteView> GetParcelasParaEstornarBaixaEmLote(int contasReceberBaixaId)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<TitulosView> GetParcelasRelatorio(FiltroRelatorioTitulos filtro)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ContasReceber> GetAllTitulosFilhos(int TituloPai)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ContasReceber> GetAllTitulosBaixados(int Titulo)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ReceitasFuturasView> GetReceitaFutura(int[] Ano, bool UnirEmpresas)
        {
            throw new NotImplementedException();
        }
    }
}
