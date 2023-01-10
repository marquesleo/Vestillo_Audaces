
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
    public interface IProdutoFornecedorPrecoService : IService<ProdutoFornecedorPreco, ProdutoFornecedorPrecoRepository, ProdutoFornecedorPrecoController>
    {
        ProdutoFornecedorPreco GetByProduto(int ProdutoId);
        IEnumerable<ProdutoFornecedorPreco> GetListByProdutoFornecedor(int ProdutoId);
        IEnumerable<Colaborador> GetFornecedores(int ProdutoId);
        ProdutoFornecedorPrecoView GetByProdutoView(int ProdutoId);
        IEnumerable<ProdutoFornecedorPrecoView> GetListByFornecedor(int ProdutoId);
        IEnumerable<ProdutoFornecedorPrecoView> GetListByFornecedorFicha(int ProdutoId);
        decimal GetCusto(int ProdutoId, int CorId, int TamanhoId);
        decimal GetCusto(int ProdutoId, int? tipoCalculoCusto, int IdProduto, short sequencia);
        decimal GetCustoMaior(int ProdutoId);
        decimal GetCustoMedio(int ProdutoId);
        decimal GetCustoMaiorCor(int ProdutoId);
        decimal GetCustoMedioCor(int ProdutoId);
        decimal GetCustoMaiorTamanho(int ProdutoId);
        decimal GetCustoMedioTamanho(int ProdutoId);
        decimal GetCustoFornecedor(int ProdutoId, int FornecedorId);
        decimal GetCustoByExcecao(int ProdutoId, int? tipoCaluloCusto, List<int> idCor, List<int> idTamanho, int IdProduto, short sequencia);
        decimal GetCustoMedioTamanhoByExcecao(int ProdutoId, List<int> idCor, List<int> idTamanho);
        decimal GetCustoMaiorTamanhoByExcecao(int ProdutoId, List<int> idCor, List<int> idTamanho);
        decimal GetCustoMedioCorByExcecao(int ProdutoId, List<int> idCor, List<int> idTamanho);
        decimal GetCustoMaiorCorByExcecao(int ProdutoId, List<int> idCor, List<int> idTamanho);
        decimal GetCustoMedioByExcecao(int ProdutoId, List<int> idCor, List<int> idTamanho);
        decimal GetCustoMaiorByExcecao(int ProdutoId, List<int> idCor, List<int> idTamanho);
        IEnumerable<ProdutoFornecedorPreco> GetValoresSemInativos(int ProdutoId);


    }
}