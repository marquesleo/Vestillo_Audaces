using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Controllers;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service
{
    public interface IProdutoDetalheService : IService<ProdutoDetalhe ,ProdutoDetalheRepository , ProdutoDetalheController >
    {
        ProdutoDetalhe  GetByProduto(int ProdutoId);
        IEnumerable<ProdutoDetalhe> GetListByProduto(int ProdutoId, int ativo);
        ProdutoDetalheView GetViewByProduto(int ProdutoId);
        IEnumerable<ProdutoDetalheView> GetListViewByProduto(int ProdutoId, int ativo, int ExibeSemuso = 0);
        IEnumerable<ProdutoDetalheView> GetByDetalheCodBarras(string codBarras);

        ProdutoDetalhe GetByGrade(int produtoId, int cor, int tamanho);
        bool VerificarCorUnica(int produtoId, int cor);
        bool VerificarTamanhoUnico(int produtoId, int tamanho);
        void AtualizarGrade(ProdutoGradeView grade);
        IEnumerable<ProdutoGradeView> GetGradeParaEdicao(int[] produtos, int[] cores, int[] tamanhos, int pagina, int registrosPorPagina, int tipoItem, out int totalRegistros);
        IEnumerable<ProdutoDetalhe> GetAtivos(int[] produtos, int[] cores, int[] tamanhos);
        IEnumerable<ProdutoDetalhe> GetTamanhosDoProdutoECor(int produto, int cor);
        IEnumerable<ProdutoDetalhe> GetCoresDoProdutoETamanho(int produto, int Tamanho);
    }
}

