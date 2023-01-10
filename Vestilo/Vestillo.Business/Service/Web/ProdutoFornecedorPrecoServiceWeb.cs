
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
    public class ProdutoFornecedorPrecoServiceWeb : GenericServiceWeb<ProdutoFornecedorPreco, ProdutoFornecedorPrecoRepository, ProdutoFornecedorPrecoController>, IProdutoFornecedorPrecoService
    {
        public ProdutoFornecedorPrecoServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public ProdutoFornecedorPreco GetByProduto(int ProdutoId)
        {
            var c = new ConnectionWebAPI<ProdutoFornecedorPreco>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "IdProduto=" + ProdutoId.ToString());
        }

        public IEnumerable<ProdutoFornecedorPreco> GetListByProdutoFornecedor(int ProdutoId)
        {
            var c = new ConnectionWebAPI<ProdutoFornecedorPreco>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri + @"?idProduto=" + ProdutoId.ToString());
        }

        public ProdutoFornecedorPrecoView GetByProdutoView(int ProdutoId)
        {
            var c = new ConnectionWebAPI<ProdutoFornecedorPrecoView>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri, "produtoId=" + ProdutoId.ToString());
        }

        public IEnumerable<ProdutoFornecedorPrecoView> GetListByFornecedor(int ProdutoId)
        {
            var c = new ConnectionWebAPI<ProdutoFornecedorPrecoView>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri + @"?idproduto=" + ProdutoId.ToString());
        }

        public decimal GetCusto(int ProdutoId, int CorId, int TamanhoId)
        {
            throw new NotImplementedException();
        }

        public decimal GetCusto(int ProdutoId, int? tipoCalculoCusto, int IdProduto, short sequencia)
        {
            throw new NotImplementedException();
        }

        public decimal GetCustoFornecedor(int ProdutoId, int FornecedorId)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<ProdutoFornecedorPrecoView> GetListByFornecedorFicha(int ProdutoId)
        {
            throw new NotImplementedException();
        }


        public decimal GetCustoMaior(int ProdutoId)
        {
            throw new NotImplementedException();
        }

        public decimal GetCustoMedio(int ProdutoId)
        {
            throw new NotImplementedException();
        }



        public decimal GetCustoMaiorCor(int ProdutoId)
        {
            throw new NotImplementedException();
        }

        public decimal GetCustoMedioCor(int ProdutoId)
        {
            throw new NotImplementedException();
        }

        public decimal GetCustoMaiorTamanho(int ProdutoId)
        {
            throw new NotImplementedException();
        }

        public decimal GetCustoMedioTamanho(int ProdutoId)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<Colaborador> GetFornecedores(int ProdutoId)
        {
            throw new NotImplementedException();
        }

        public decimal GetCustoByExcecao(int ProdutoId, int? tipoCaluloCusto, List<int> idCor, List<int> idTamanho, int IdProduto, short sequencia)
        {
            throw new NotImplementedException();
        }

        public decimal GetCustoMedioTamanhoByExcecao(int ProdutoId, List<int> idCor, List<int> idTamanho)
        {
            throw new NotImplementedException();
        }

        public decimal GetCustoMaiorTamanhoByExcecao(int ProdutoId, List<int> idCor, List<int> idTamanho)
        {
            throw new NotImplementedException();
        }

        public decimal GetCustoMedioCorByExcecao(int ProdutoId, List<int> idCor, List<int> idTamanho)
        {
            throw new NotImplementedException();
        }

        public decimal GetCustoMaiorCorByExcecao(int ProdutoId, List<int> idCor, List<int> idTamanho)
        {
            throw new NotImplementedException();
        }

        public decimal GetCustoMedioByExcecao(int ProdutoId, List<int> idCor, List<int> idTamanho)
        {
            throw new NotImplementedException();
        }

        public decimal GetCustoMaiorByExcecao(int ProdutoId, List<int> idCor, List<int> idTamanho)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ProdutoFornecedorPreco> GetValoresSemInativos(int ProdutoId)
        {
            throw new NotImplementedException();
        }
    }
}