using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service.APP
{
    public class ProdutoDetalheServiceAPP : GenericServiceAPP<ProdutoDetalhe, ProdutoDetalheRepository, ProdutoDetalheController>, IProdutoDetalheService
    {

        public ProdutoDetalheServiceAPP()
            : base(new ProdutoDetalheController())
        {
        }

        public ProdutoDetalhe GetByProduto(int ProdutoId)
        {
            return controller.GetByProduto(ProdutoId);
        }

        public IEnumerable<ProdutoDetalhe> GetListByProduto(int ProdutoId, int ativo)
        {
            return controller.GetListByProduto(ProdutoId, ativo);
        }

        public ProdutoDetalheView GetViewByProduto(int ProdutoId)
        {
            return controller.GetViewByProduto(ProdutoId);
        }

        public IEnumerable<ProdutoDetalheView> GetListViewByProduto(int ProdutoId, int ativo, int ExibeSemuso = 0)
        {
            return controller.GetListViewByProduto(ProdutoId, ativo, ExibeSemuso);
        }


        public IEnumerable<ProdutoDetalheView> GetByDetalheCodBarras(string codBarras)
        {
            return controller.GetByDetalheCodBarras(codBarras);
        }

        public void AtualizarGrade(ProdutoGradeView grade)
        {
            controller.AtualizarGrade(grade);
        }

        public IEnumerable<ProdutoGradeView> GetGradeParaEdicao(int[] produtos, int[] cores, int[] tamanhos, int pagina, int registrosPorPagina, int tipoItem, out int totalRegistros)
        {
            return controller.GetGradeParaEdicao(produtos, cores, tamanhos, pagina, registrosPorPagina, tipoItem, out totalRegistros);
        }

        public IEnumerable<ProdutoDetalhe> GetAtivos(int[] produtos, int[] cores, int[] tamanhos)
        {
            return controller.GetAtivos(produtos, cores, tamanhos);
        }


        public IEnumerable<ProdutoDetalhe> GetTamanhosDoProdutoECor(int produto, int cor)
        {
            return controller.GetTamanhosDoProdutoECor(produto, cor);
        }


        public IEnumerable<ProdutoDetalhe> GetCoresDoProdutoETamanho(int produto, int Tamanho)
        {
            return controller.GetCoresDoProdutoETamanho(produto, Tamanho);
        }


        public ProdutoDetalhe GetByGrade(int produtoId, int cor, int tamanho)
        {
            return controller.GetByGrade(produtoId, cor, tamanho);
        }


        public bool VerificarCorUnica(int produtoId, int cor)
        {
            return controller.VerificarCorUnica(produtoId, cor);
        }

        public bool VerificarTamanhoUnico(int produtoId, int tamanho)
        {
            return controller.VerificarTamanhoUnico(produtoId, tamanho);
        }
    }
}