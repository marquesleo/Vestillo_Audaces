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
    public interface IProdutoService : IService<Produto ,ProdutoRepository , ProdutoController >
    {
        Produto GetByReferencia(string referencia);
        Produto GetByReferenciaFornecedor(string referencia);
        IEnumerable<Produto> GetListPorDescricao(string desc);
        IEnumerable<Produto> GetListPorFornecedor(string fornecedor);
        IEnumerable<Produto> GetListPorReferencia(string referencia);
        IEnumerable<Produto> GetListPorDescricaoSemFichaTecnica(string desc);
        IEnumerable<Produto> GetListPorFornecedorSemFichaTecnica(string fornecedor);
        IEnumerable<Produto> GetListPorReferenciaSemFichaTecnica(string referencia);

        IEnumerable<Produto> GetListPorDescricaoSemFichaTecnicaMaterial(string desc);
        IEnumerable<Produto> GetListPorFornecedorSemFichaTecnicaMaterial(string fornecedor);
        IEnumerable<Produto> GetListPorReferenciaSemFichaTecnicaMaterial(string referencia);

        IEnumerable<Produto> GetListById(int id);
        Produto GetByUnicoCodBarras(string CodBarras);
        IEnumerable<MovimentarEstoqueView> GetProdutoPraMovimentarEstoque(string busca, bool buscarPorId, int almoxarifadoId);
        IEnumerable<DevolucaoItensView> GetProdutoDevolucaoItens(string busca, bool buscarPorId, int almoxarifadoId);
        IEnumerable<FocoVendas> GetFocoVendas(FiltroRelatorioFocoVendas filtro,bool AgruparCor);
        IEnumerable<ProdutoEtiqueta> GetProdutosParaEtiqueta(FiltroRelatorioEtiquetaProduto filtro);
        IEnumerable<ProdutoEtiqueta> GetProdutosParaEtiquetaOrdem(FiltroRelatorioEtiquetaProdutoOrdem filtro);
        IEnumerable<ProdutoEtiqueta> GetProdutosParaEtiquetaPedido(FiltroRelatorioEtiquetaProdutoPedidoVenda filtro);
        IEnumerable<ProdutoEtiqueta> GetProdutosParaEtiquetaPacote(FiltroRelatorioEtiquetaProdutoPacote filtro);
        IEnumerable<ProdutoEtiqueta> GetProdutosParaEtiquetaComposicao(FiltroRelatorioEtiquetaComposicao filtro);
        decimal GetPrecoDeCustoDoProduto(Produto produto);
        IEnumerable<Produto> GetListPorReferenciaComFichaTecnica(string referencia, bool fichaTecnicaCompleta);
        IEnumerable<Produto> GetListPorDescricaoComFichaTecnica(string desc, bool fichaTecnicaCompleta);
        IEnumerable<OrdemProducaoMaterialView> GetListPorIdComFichaTecnicaMaterial(List<int> produtosId, int ordemId);
        IEnumerable<OrdemProducaoMaterialView> GetListPorIdComFichaTecnicaMaterial(ItemOrdemProducaoView item, int ordemId);
        IEnumerable<CompraMaterial> GetListPorIdComFichaTecnicaMaterial(FiltroRelatorioCompraMaterial filtro);
        IEnumerable<Produto> GetProdutosLiberados();
        IEnumerable<Produto> GetProdutosLiberados(string referencia);
        IEnumerable<Produto> GetListPorTipoAtivo(Produto.enuTipoItem tipo, bool Ambos = false);
        Produto GetByReferenciaComFichaTecnica(string referencia, bool fichaTecnicaCompleta);
        IEnumerable<Produto> GetAllAtivos();
        IEnumerable<Produto> GetListMateriasPrimasQuePossuemGrade();
        IEnumerable<Produto> GetListProdutosQuePossuemGradeENaoPossuemFichaTecnica();
        IEnumerable<Produto> GetProdutosParaManutencaoFichaTecnica(bool comFicha, bool semFicha);
        IEnumerable<OrdemProducaoMaterialView> GetListPorIdComFichaTecnicaMaterialSemOP(List<int> produtosId, int ordemId);
    }
}

