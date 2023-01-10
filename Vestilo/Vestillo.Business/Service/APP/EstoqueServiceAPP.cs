using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Models.Views;

namespace Vestillo.Business.Service.APP
{
    public class EstoqueServiceAPP : GenericServiceAPP<Estoque, EstoqueRepository, EstoqueController>, IEstoqueService
    {

        public EstoqueServiceAPP()
            : base(new EstoqueController())
        {
        }

        public IEnumerable<ConsultaEstoqueView> GetEstoque()
        {
            return controller.GetEstoque();
        }

        public IEnumerable<ConsultaMovimentacaoEstoqueView> GetMovimentacaoEstoque(int estoqueId)
        {
            return controller.GetMovimentacaoEstoque(estoqueId);
        }

        public ConsultaEstoqueView GetSaldoAtualProduto(int almoxarifadoId, int produtoId, int corId, int tamanhoId)
        {
             return controller.GetSaldoAtualProduto(almoxarifadoId, produtoId, corId, tamanhoId);
        }

        public void MovimentarEstoque(List<MovimentacaoEstoque> lstMovimentacao, bool conjunto)
        {
            controller.MovimentarEstoque(lstMovimentacao, conjunto);
        }

        public void TransferirEstoque(List<MovimentacaoEstoque> lstMovimentacao, int almoxarifadoDestinoId)
        {
            controller.TransferirEstoque(lstMovimentacao, almoxarifadoDestinoId);
        }

        public IEnumerable<ConsultaMovimentacaoEstoqueView> GetMovimentacaoEstoque(int almoxarifadoId, string codigoBarras, int produtoId)
        {
            return controller.GetMovimentacaoEstoque(almoxarifadoId, codigoBarras, produtoId);
        }

        public void MovimentarEstoque(List<MovimentacaoEstoque> lstMovimentacao)
        {
            controller.MovimentarEstoque(lstMovimentacao, false);
        }

        public IEnumerable<ConsultaEstoqueProdutoroduzidoView> GetEstoque(FichaEstoqueProdutoProduzido filtro)
        {
            return controller.GetEstoque(filtro);
        }

        public IEnumerable<Models.Views.ConsultaEstoqueMateriaPrima> GetEstoqueMateriaPrima(FichaEstoqueMateriaPrima filtro)
        {
            return controller.GetEstoqueMateriaPrima(filtro);
        }

        public ConsultaEstoqueView GetEmpenhoAtualProduto(int almoxerifadoId, int produtoId, int corId, int tamanhoId)
        {
            return controller.GetEmpenhoAtualProduto(almoxerifadoId, produtoId, corId, tamanhoId);
        }


        public IEnumerable<ConsultaEstoqueView> GetEstoqueRelatorio(FiltroEstoqueRelatorio filtro)
        {
            return controller.GetEstoqueRelatorio(filtro);
        }

        public IEnumerable<ConsultaEstoqueRelatorioView> GetConsultaEstoqueRelatorio(List<int> idEmpresas, int? tipoProduto, bool faturado, DateTime? daData, DateTime? ateData)
        {
            return controller.GetConsultaEstoqueRelatorio(idEmpresas, tipoProduto, faturado, daData, ateData);
        }
    }
}
