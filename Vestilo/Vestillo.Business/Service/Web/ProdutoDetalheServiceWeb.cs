using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Lib;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;


namespace Vestillo.Business.Service.Web
{
    public class ProdutoDetalheServiceWeb : GenericServiceWeb<ProdutoDetalhe, ProdutoDetalheRepository, ProdutoDetalheController>, IProdutoDetalheService
    {
        public ProdutoDetalheServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public ProdutoDetalhe GetByProduto(int ProdutoId)
        {
            var c = new ConnectionWebAPI<ProdutoDetalhe>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "IdProduto=" + ProdutoId.ToString());
        }

        public IEnumerable<ProdutoDetalhe> GetListByProduto(int ProdutoId, int ativo)
        {
            var c = new ConnectionWebAPI<ProdutoDetalhe>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri + @"?idProduto=" + ProdutoId.ToString());
        }

        public ProdutoDetalheView GetViewByProduto(int ProdutoId)
        {
            var c = new ConnectionWebAPI<ProdutoDetalheView>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "produtoId=" + ProdutoId.ToString());
        }

        public IEnumerable<ProdutoDetalheView> GetListViewByProduto(int ProdutoId, int ativo, int ExibeSemuso = 0)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProdutoDetalheView> GetByDetalheCodBarras(string codBarras)
        {
            throw new NotImplementedException();
        }

        public void AtualizarGrade(ProdutoGradeView grade)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProdutoGradeView> GetGradeParaEdicao(int[] produtos, int[] cores, int[] tamanhos, int pagina, int registrosPorPagina, int tipoItem, out int totalRegistros)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProdutoDetalhe> GetAtivos(int[] produtos, int[] cores, int[] tamanhos)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<ProdutoDetalhe> GetTamanhosDoProdutoECor(int produto, int cor)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<ProdutoDetalhe> GetCoresDoProdutoETamanho(int produto, int Tamanho)
        {
            throw new NotImplementedException();
        }


        public ProdutoDetalhe GetByGrade(int produtoId, int cor, int tamanho)
        {
            throw new NotImplementedException();
        }


        public bool VerificarCorUnica(int produtoId, int cor)
        {
            throw new NotImplementedException();
        }

        public bool VerificarTamanhoUnico(int produtoId, int tamanho)
        {
            throw new NotImplementedException();
        }
    }
}


