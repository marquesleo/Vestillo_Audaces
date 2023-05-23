
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
    public class FatNfeServiceAPP : GenericServiceAPP<FatNfe, FatNfeRepository, FatNfeController>, IFatNfeService
    {
        public FatNfeServiceAPP() : base(new FatNfeController())
        {
        }

        public IEnumerable<FatNfeView> GetCamposEspecificos(string parametrosDaBusca)
        {
            return controller.GetCamposEspecificos(parametrosDaBusca);
        }


        public IEnumerable<FatNfeView> GetListPorReferencia(string referencia, string parametrosDaBusca)
        {

            return controller.GetListPorReferencia(referencia,parametrosDaBusca);
            
        }

        public IEnumerable<FatNfeView> GetListPorNumero(string Numero, string parametrosDaBusca)
        {
            return controller.GetListPorNumero(Numero,parametrosDaBusca);
            
        }

        public IEnumerable<FatNfeView> GetListagemNfeRelatorio(FiltroFatNfeListagem filtro)
        {
            return controller.GetListagemNfeRelatorio(filtro);
        }

        public IEnumerable<FechamentoDoDiaView> GetFechamentoDoDia(DateTime DataInicio, DateTime DataFim, int Tipo)
        {

            return controller.GetFechamentoDoDia(DataInicio, DataFim, Tipo);

        }

        public IEnumerable<RepXVendaView> GetRepXVenda(string Ano, int Uf)
        {
            return controller.GetRepXVenda(Ano,Uf);
        }

        public IEnumerable<ListaFatVendaView> GetListaFatXVenda(DateTime DataInicio, DateTime DataFim, List<int> Vendedor, bool SomenteNFCe, bool SomenteTipoVendaNFCe, bool DataDatNfce)
        {
            return controller.GetListaFatXVenda(DataInicio, DataFim, Vendedor,SomenteNFCe,SomenteTipoVendaNFCe, DataDatNfce);
        }

        public int TotalFaturamentos(DateTime DataInicio, DateTime DataFim, int Vendedor, bool SomenteNFCe, bool DataDatNfce)
        {
            return controller.TotalFaturamentos(DataInicio, DataFim, Vendedor,SomenteNFCe, DataDatNfce);

        }


        public IEnumerable<FechamentoDoDiaPagView> GetFechamentoDoDiaPorPagamento(DateTime DataInicio, DateTime DataFim)
        {
            return controller.GetFechamentoDoDiaPorPagamento(DataInicio, DataFim);

        }

        public FatNfeEtiquetaView EtiquetaEnderecamento(int Faturamento, int Tipo)
        {

            return controller.EtiquetaEnderecamento(Faturamento, Tipo);
        }

        public decimal TotalNcc(DateTime DataInicio, DateTime DataFim, int IdVendedor)
        {
            return controller.TotalNcc(DataInicio, DataFim, IdVendedor);

        }

        public void GravarMovimentacaoCaixa(string referenciaNota, int idNota, IEnumerable<ContasReceber> parcelas, bool debito = false)
        {
            controller.GravarMovimentacaoCaixa(referenciaNota, idNota, parcelas, debito);
        }

        public FatNfe GetByNfce(int idNFCe)
        {
            return controller.GetByNfce(idNFCe);
        }

    }
}
