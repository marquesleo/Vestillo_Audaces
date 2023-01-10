using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Controllers;
using Vestillo.Business.Repositories;
using Vestillo.Business.Models.Views;

namespace Vestillo.Business.Service
{
    public interface IEstoqueService : IService<Estoque, EstoqueRepository, EstoqueController>
    {
        IEnumerable<ConsultaEstoqueView> GetEstoque();
        IEnumerable<ConsultaMovimentacaoEstoqueView> GetMovimentacaoEstoque(int estoqueId);
        ConsultaEstoqueView GetSaldoAtualProduto(int almoxarifadoId, int produtoId, int corId, int tamanhoId);
        void MovimentarEstoque(List<MovimentacaoEstoque> lstMovimentacao, bool conjunto);
        void MovimentarEstoque(List<MovimentacaoEstoque> lstMovimentacao);
        void TransferirEstoque(List<MovimentacaoEstoque> lstMovimentacao, int almoxarifadoDestinoId);
        IEnumerable<ConsultaMovimentacaoEstoqueView> GetMovimentacaoEstoque(int almoxarifadoId, string codigoBarras, int produtoId);
        IEnumerable<ConsultaEstoqueProdutoroduzidoView> GetEstoque(FichaEstoqueProdutoProduzido filtro);
        IEnumerable<ConsultaEstoqueMateriaPrima> GetEstoqueMateriaPrima(FichaEstoqueMateriaPrima filtro);
        ConsultaEstoqueView GetEmpenhoAtualProduto(int almoxerifadoId, int produtoId, int corId, int tamanhoId);
        IEnumerable<ConsultaEstoqueView> GetEstoqueRelatorio(FiltroEstoqueRelatorio filtro);
        IEnumerable<ConsultaEstoqueRelatorioView> GetConsultaEstoqueRelatorio(List<int> idEmpresas, int? tipoProduto, bool faturado, DateTime? daData, DateTime? ateData);
    }
}
