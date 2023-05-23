
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Business.Controllers;
using Vestillo.Business.Repositories;
using Vestillo.Lib;

namespace Vestillo.Business.Service.Web
{
    public class ProdutoServiceWeb : GenericServiceWeb<Produto, ProdutoRepository, ProdutoController>, IProdutoService
    {

        public ProdutoServiceWeb(string requestUri): base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public Produto GetByReferencia(string referencia)
        {
            var t = new ConnectionWebAPI<Produto>(VestilloSession.UrlWebAPI);
            return t.Get(this.RequestUri, "referencia = '" + referencia + "'");
        }

        public Produto GetByReferenciaFornecedor(string referencia)
        {
            var t = new ConnectionWebAPI<Produto>(VestilloSession.UrlWebAPI);
            return t.Get(this.RequestUri, "referencia like '%" + referencia + "%'");
        }

        


        public IEnumerable<Produto> GetListPorDescricao(string desc)
        {
            var c = new ConnectionWebAPI<IEnumerable<Produto>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "descricao like '%" + desc + "%' And ativo = 1");
        }

        public IEnumerable<Produto> GetListPorReferencia(string referencia)
        {
            var c = new ConnectionWebAPI<IEnumerable<Produto>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "referencia like '%" + referencia + "%' And ativo = 1");
        }

        public IEnumerable<Produto> GetListById(int id)
        {
            var c = new ConnectionWebAPI<IEnumerable<Produto>>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "id = " + id + " And ativo = 1");
        }

        public Produto GetByUnicoCodBarras(string CodBarras)
        {
            var t = new ConnectionWebAPI<Produto>(VestilloSession.UrlWebAPI);
            return t.Get(this.RequestUri, "CodBarrasUnico = '" + CodBarras + "'");
        }

        public  IEnumerable<MovimentarEstoqueView> GetProdutoPraMovimentarEstoque(string busca, bool buscarPorId, int almoxarifadoId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DevolucaoItensView> GetProdutoDevolucaoItens(string busca, bool buscarPorId, int almoxarifadoId)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Models.Views.FocoVendas> GetFocoVendas(Models.Views.FiltroRelatorioFocoVendas filtro,bool AgruparCor)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProdutoEtiqueta> GetProdutosParaEtiqueta(Models.Views.FiltroRelatorioEtiquetaProduto filtro)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<ProdutoEtiqueta> GetProdutosParaEtiquetaOrdem(Models.Views.FiltroRelatorioEtiquetaProdutoOrdem filtro)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<ProdutoEtiqueta> GetProdutosParaEtiquetaPedido(Models.Views.FiltroRelatorioEtiquetaProdutoPedidoVenda filtro)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<ProdutoEtiqueta> GetProdutosParaEtiquetaPacote(Models.Views.FiltroRelatorioEtiquetaProdutoPacote filtro)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProdutoEtiqueta> GetProdutosParaEtiquetaComposicao(Models.Views.FiltroRelatorioEtiquetaComposicao filtro)
        {
            throw new NotImplementedException();
        }

        public decimal GetPrecoDeCustoDoProduto(Produto produto)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Produto> GetListProdutosQuePossuemGradeENaoPossuemFichaTecnica()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Produto> GetListMateriasPrimasQuePossuemGrade()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Produto> GetProdutosParaManutencaoFichaTecnica(bool comFicha, bool semFicha)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Produto> GetListPorReferenciaComFichaTecnica(string referencia, bool fichaTecnicaCompleta)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Produto> GetListPorDescricaoComFichaTecnica(string desc, bool fichaTecnicaCompleta)
        {
            throw new NotImplementedException();
        }


        public Produto GetByReferenciaComFichaTecnica(string referencia, bool fichaTecnicaCompleta)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<OrdemProducaoMaterialView> GetListPorIdComFichaTecnicaMaterial(List<int> produtosId, int ordemId)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Produto> GetProdutosLiberados()
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Produto> GetProdutosLiberados(string referencia)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<OrdemProducaoMaterialView> GetListPorIdComFichaTecnicaMaterial(ItemOrdemProducaoView item, int ordemId)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<CompraMaterial> GetListPorIdComFichaTecnicaMaterial(FiltroRelatorioCompraMaterial filtro)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Produto> GetListPorTipoAtivo(Produto.enuTipoItem tipo, bool Ambos = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Produto> GetAllAtivos()
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Produto> GetListPorFornecedor(string fornecedor)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Produto> GetListPorDescricaoSemFichaTecnica(string desc)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Produto> GetListPorFornecedorSemFichaTecnica(string fornecedor)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Produto> GetListPorReferenciaSemFichaTecnica(string referencia)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<OrdemProducaoMaterialView> GetListPorIdComFichaTecnicaMaterialSemOP(List<int> produtosId, int ordemId)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Produto> GetListPorDescricaoSemFichaTecnicaMaterial(string desc)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Produto> GetListPorFornecedorSemFichaTecnicaMaterial(string fornecedor)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Produto> GetListPorReferenciaSemFichaTecnicaMaterial(string referencia)
        {
            throw new NotImplementedException();
        }
    }
}

