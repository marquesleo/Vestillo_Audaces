
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
    public class ProdutoFornecedorPrecoServiceAPP : GenericServiceAPP<ProdutoFornecedorPreco, ProdutoFornecedorPrecoRepository, ProdutoFornecedorPrecoController>, IProdutoFornecedorPrecoService
    {

        public ProdutoFornecedorPrecoServiceAPP()
            : base(new ProdutoFornecedorPrecoController())
        {
        }

        public ProdutoFornecedorPreco GetByProduto(int ProdutoId)
        {
            return controller.GetByProduto(ProdutoId);
        }

        public IEnumerable<ProdutoFornecedorPreco> GetListByProdutoFornecedor(int ProdutoId)
        {
            return controller.GetListByProdutoFornecedor(ProdutoId);
        }

        public ProdutoFornecedorPrecoView GetByProdutoView(int ProdutoId)
        {
            return controller.GetByProdutoView(ProdutoId);
        }

        public IEnumerable<ProdutoFornecedorPrecoView> GetListByFornecedor(int ProdutoId)
        {
            return controller.GetListByFornecedor(ProdutoId);
        }

        public decimal GetCusto(int ProdutoId, int CorId, int TamanhoId)
        {
            return controller.GetCusto(ProdutoId, CorId, TamanhoId);
        }

        public decimal GetCusto(int ProdutoId, int? tipoCalculoCusto, int IdProduto, short sequencia)
        {
            return controller.GetCusto(ProdutoId, tipoCalculoCusto, IdProduto, sequencia);
        }

        public decimal GetCustoFornecedor(int ProdutoId, int FornecedorId)
        {
            return controller.GetCustoFornecedor(ProdutoId, FornecedorId);
        }


        public IEnumerable<ProdutoFornecedorPrecoView> GetListByFornecedorFicha(int ProdutoId)
        {
            return controller.GetListByFornecedorFicha(ProdutoId);
        }


        public decimal GetCustoMaior(int ProdutoId)
        {
            return controller.GetCustoMaior(ProdutoId);
        }

        public decimal GetCustoMedio(int ProdutoId)
        {
            return controller.GetCustoMedio(ProdutoId);
        }

        public decimal GetCustoMaiorCor(int ProdutoId)
        {
            return controller.GetCustoMaiorCor(ProdutoId);
        }

        public decimal GetCustoMedioCor(int ProdutoId)
        {
            return controller.GetCustoMedioCor(ProdutoId);
        }

        public decimal GetCustoMaiorTamanho(int ProdutoId)
        {
            return controller.GetCustoMaiorTamanho(ProdutoId);
        }

        public decimal GetCustoMedioTamanho(int ProdutoId)
        {
            return controller.GetCustoMedioTamanho(ProdutoId);
        }

        public IEnumerable<Colaborador> GetFornecedores(int ProdutoId)
        {
            return controller.GetFornecedores(ProdutoId);
        }

        public decimal GetCustoByExcecao(int ProdutoId, int? tipoCaluloCusto, List<int> idCor, List<int> idTamanho, int IdProduto, short sequencia)
        {
            return controller.GetCustoByExcecao(ProdutoId, tipoCaluloCusto, idCor, idTamanho, IdProduto, sequencia);
        }

        public decimal GetCustoMedioTamanhoByExcecao(int ProdutoId, List<int> idCor, List<int> idTamanho)
        {
            return controller.GetCustoMedioTamanhoByExcecao(ProdutoId, idCor, idTamanho);
        }

        public decimal GetCustoMaiorTamanhoByExcecao(int ProdutoId, List<int> idCor, List<int> idTamanho)
        {
            return controller.GetCustoMaiorTamanhoByExcecao(ProdutoId, idCor, idTamanho);
        }

        public decimal GetCustoMedioCorByExcecao(int ProdutoId, List<int> idCor, List<int> idTamanho)
        {
            return controller.GetCustoMedioCorByExcecao(ProdutoId, idCor, idTamanho);
        }

        public decimal GetCustoMaiorCorByExcecao(int ProdutoId, List<int> idCor, List<int> idTamanho)
        {
            return controller.GetCustoMaiorCorByExcecao(ProdutoId, idCor, idTamanho);
        }

        public decimal GetCustoMedioByExcecao(int ProdutoId, List<int> idCor, List<int> idTamanho)
        {
            return controller.GetCustoMedioByExcecao(ProdutoId, idCor, idTamanho);
        }

        public decimal GetCustoMaiorByExcecao(int ProdutoId, List<int> idCor, List<int> idTamanho)
        {
            return controller.GetCustoMaiorByExcecao(ProdutoId, idCor, idTamanho);
        }

        public IEnumerable<ProdutoFornecedorPreco> GetValoresSemInativos(int ProdutoId)
        {
            return controller.GetValoresSemInativos(ProdutoId);
        }

    }
}