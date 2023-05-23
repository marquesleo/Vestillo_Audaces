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
    public interface IFatNfeService : IService<FatNfe , FatNfeRepository , FatNfeController >
    {
        IEnumerable<FatNfeView> GetCamposEspecificos(string parametrosDaBusca);
        IEnumerable<FatNfeView> GetListPorReferencia(string referencia, string parametrosDaBusca);
        IEnumerable<FatNfeView> GetListPorNumero(string Numero, string parametrosDaBusca);
        IEnumerable<FechamentoDoDiaView> GetFechamentoDoDia(DateTime DataInicio, DateTime DataFim, int Tipo);
        IEnumerable<RepXVendaView> GetRepXVenda(string Ano, int Uf);
        IEnumerable<ListaFatVendaView> GetListaFatXVenda(DateTime DataInicio, DateTime DataFim, List<int> Vendedor, bool SomenteNFCe, bool SomenteTipoVendaNFCe, bool DataDatNfce);
        int TotalFaturamentos(DateTime DataInicio, DateTime DataFim, int Vendedor, bool SomenteNFCe, bool DataDatNfce);
        IEnumerable<FechamentoDoDiaPagView> GetFechamentoDoDiaPorPagamento(DateTime DataInicio, DateTime DataFim);
        FatNfeEtiquetaView EtiquetaEnderecamento(int Faturamento, int Tipo);
        decimal TotalNcc(DateTime DataInicio, DateTime DataFim, int IdVendedor);
        IEnumerable<FatNfeView> GetListagemNfeRelatorio(FiltroFatNfeListagem filtro);
        void GravarMovimentacaoCaixa(string referenciaNota, int idNota, IEnumerable<ContasReceber> parcelas, bool debito = false);
        FatNfe GetByNfce(int idNFCe);

    }

}