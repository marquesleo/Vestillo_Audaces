using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service.APP
{
    public class ProdutoServiceAPP : GenericServiceAPP<Produto, ProdutoRepository, ProdutoController>, IProdutoService
    {

        public ProdutoServiceAPP() : base(new ProdutoController())
        {
        }

        public Produto GetByReferencia(string referencia)
        {
            return controller.GetByReferencia(referencia);
        }

        public Produto GetByReferenciaFornecedor(string referencia)
        {
            return controller.GetByReferenciaFornecedor(referencia);
        }


        public IEnumerable<Produto> GetListPorDescricao(string desc)
        {
            ProdutoController controller = new ProdutoController();
            return controller.GetListPorDescricao(desc);
        }

        public IEnumerable<Produto> GetListPorReferencia(string referencia)
        {
            ProdutoController controller = new ProdutoController();
            return controller.GetListPorReferencia(referencia);
        }

        public IEnumerable<Produto> GetListById(int id)
        {
            ProdutoController controller = new ProdutoController();
            return controller.GetListById(id);
        }

        public Produto GetByUnicoCodBarras(string CodBarras)
        {
            ProdutoController controller = new ProdutoController();
            return controller.GetByUnicoCodBarras(CodBarras);
        }

        public IEnumerable<MovimentarEstoqueView> GetProdutoPraMovimentarEstoque(string busca, bool buscarPorId, int almoxarifadoId)
        {
            ProdutoController controller = new ProdutoController();
            return controller.GetProdutoPraMovimentarEstoque(busca, buscarPorId, almoxarifadoId);
        }



        public IEnumerable<DevolucaoItensView> GetProdutoDevolucaoItens(string busca, bool buscarPorId, int almoxarifadoId)
        {
            ProdutoController controller = new ProdutoController();
            return controller.GetProdutoDevolucaoItens(busca, buscarPorId, almoxarifadoId);
        }

        public IEnumerable<Models.Views.FocoVendas> GetFocoVendas(Models.Views.FiltroRelatorioFocoVendas filtro,bool AgruparCor)
        {
            ProdutoController controller = new ProdutoController();
            return controller.GetFocoVendas(filtro, AgruparCor);
            
        }

        public IEnumerable<ProdutoEtiqueta> GetProdutosParaEtiqueta(Models.Views.FiltroRelatorioEtiquetaProduto filtro)
        {
            return controller.GetProdutosParaEtiqueta(filtro);
        }


        public IEnumerable<ProdutoEtiqueta> GetProdutosParaEtiquetaOrdem(Models.Views.FiltroRelatorioEtiquetaProdutoOrdem filtro)
        {
            return controller.GetProdutosParaEtiquetaOrdem(filtro);
        }


        public IEnumerable<ProdutoEtiqueta> GetProdutosParaEtiquetaPedido(Models.Views.FiltroRelatorioEtiquetaProdutoPedidoVenda filtro)
        {
            return controller.GetProdutosParaEtiquetaPedido(filtro);
        }


        public IEnumerable<ProdutoEtiqueta> GetProdutosParaEtiquetaPacote(Models.Views.FiltroRelatorioEtiquetaProdutoPacote filtro)
        {
            return controller.GetProdutosParaEtiquetaPacote(filtro);
        }

        public IEnumerable<ProdutoEtiqueta> GetProdutosParaEtiquetaComposicao(Models.Views.FiltroRelatorioEtiquetaComposicao filtro)
        {
            return controller.GetProdutosParaEtiquetaComposicao(filtro);
        }

        public IEnumerable<Produto> GetAllAtivos()
        {
            return controller.GetAllAtivos();
        }

        public IEnumerable<Produto> GetProdutosParaManutencaoFichaTecnica(bool comFicha, bool semFicha)
        {
            return controller.GetProdutosParaManutencaoFichaTecnica(comFicha, semFicha);
        }

        public IEnumerable<Produto> GetListPorTipoAtivo(Produto.enuTipoItem tipo, bool Ambos = false)
        {
            return controller.GetListPorTipoAtivo(tipo,Ambos);
        }


        public decimal GetPrecoDeCustoDoProduto(Produto produto)
        {
            return controller.GetPrecoDeCustoDoProduto(produto);
        }

        public IEnumerable<Produto> GetListProdutosQuePossuemGradeENaoPossuemFichaTecnica()
        {
            return controller.GetListProdutosQuePossuemGradeENaoPossuemFichaTecnica();
        }

        public IEnumerable<Produto> GetListMateriasPrimasQuePossuemGrade()
        {
            return controller.GetListMateriasPrimasQuePossuemGrade();
        }

        public IEnumerable<Produto> GetListPorReferenciaComFichaTecnica(string referencia, bool fichaTecnicaCompleta)
        {
            return controller.GetListPorReferenciaComFichaTecnica(referencia, fichaTecnicaCompleta);
        }

        public IEnumerable<Produto> GetListPorDescricaoComFichaTecnica(string desc, bool fichaTecnicaCompleta)
        {
            return controller.GetListPorDescricaoComFichaTecnica(desc, fichaTecnicaCompleta);
        }

        public IEnumerable<OrdemProducaoMaterialView> GetListPorIdComFichaTecnicaMaterial(List<int> produtosId, int ordemId)
        {
            return controller.GetListPorIdComFichaTecnicaMaterial(produtosId, ordemId);
        }

        public IEnumerable<OrdemProducaoMaterialView> GetListPorIdComFichaTecnicaMaterialSemOP(List<int> produtosId, int ordemId)
        {
            return controller.GetListPorIdComFichaTecnicaMaterialSemOP(produtosId, ordemId);
        }

        public IEnumerable<Produto> GetProdutosLiberados(string referencia)
        {
            return controller.GetProdutosLiberados(referencia);
        }

        public IEnumerable<Produto> GetProdutosLiberados()
        {
            return controller.GetProdutosLiberados("");
        }


        public IEnumerable<OrdemProducaoMaterialView> GetListPorIdComFichaTecnicaMaterial(ItemOrdemProducaoView item, int ordemId)
        {
            return controller.GetListPorIdComFichaTecnicaMaterial(item, ordemId);
        }


        public IEnumerable<CompraMaterial> GetListPorIdComFichaTecnicaMaterial(FiltroRelatorioCompraMaterial filtro)
        {
            return controller.GetListPorIdComFichaTecnicaMaterial(filtro);
        }
        
        public Produto GetByReferenciaComFichaTecnica(string referencia, bool fichaTecnicaCompleta)
        {
            return controller.GetByReferenciaComFichaTecnica(referencia, fichaTecnicaCompleta);
        }


        public IEnumerable<Produto> GetListPorFornecedor(string fornecedor)
        {
            ProdutoController controller = new ProdutoController();
            return controller.GetListPorFornecedor(fornecedor);
        }


        public IEnumerable<Produto> GetListPorDescricaoSemFichaTecnica(string desc)
        {
            return controller.GetListPorDescricaoSemFichaTecnica(desc);
        }

        public IEnumerable<Produto> GetListPorFornecedorSemFichaTecnica(string fornecedor)
        {
            return controller.GetListPorFornecedorSemFichaTecnica(fornecedor);
        }

        public IEnumerable<Produto> GetListPorReferenciaSemFichaTecnica(string referencia)
        {
            return controller.GetListPorReferenciaSemFichaTecnica(referencia);
        }


        public IEnumerable<Produto> GetListPorDescricaoSemFichaTecnicaMaterial(string desc)
        {
            return controller.GetListPorDescricaoSemFichaTecnicaMaterial(desc);
        }

        public IEnumerable<Produto> GetListPorFornecedorSemFichaTecnicaMaterial(string fornecedor)
        {
            return controller.GetListPorFornecedorSemFichaTecnicaMaterial(fornecedor);
        }

        public IEnumerable<Produto> GetListPorReferenciaSemFichaTecnicaMaterial(string referencia)
        {
            return controller.GetListPorReferenciaSemFichaTecnicaMaterial(referencia);
        }
    }
}



