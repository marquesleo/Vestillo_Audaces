using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;


namespace Vestillo.Business.Controllers
{
    public class ProdutoFornecedorPrecoController : GenericController<ProdutoFornecedorPreco, ProdutoFornecedorPrecoRepository>
    {
        public ProdutoFornecedorPreco GetByProduto(int ProdutoId)
        {
            using (ProdutoFornecedorPrecoRepository repository = new ProdutoFornecedorPrecoRepository())
            {
                return repository.GetByProduto(ProdutoId);
            }
        }

        public IEnumerable<ProdutoFornecedorPreco> GetListByProdutoFornecedor(int ProdutoId)
        {
            using (ProdutoFornecedorPrecoRepository repository = new ProdutoFornecedorPrecoRepository())
            {
                return repository.GetListByProdutoFornecedor(ProdutoId);
            }
        }

        public IEnumerable<ProdutoFornecedorPrecoView> GetListByFornecedorFicha(int ProdutoId)
        {
            using (ProdutoFornecedorPrecoRepository repository = new ProdutoFornecedorPrecoRepository())
            {
                return repository.GetListByFornecedorFicha(ProdutoId);
            }
        }

        public ProdutoFornecedorPrecoView GetByProdutoView(int ProdutoId)
        {
            using (ProdutoFornecedorPrecoRepository repository = new ProdutoFornecedorPrecoRepository())
            {
                return repository.GetByProdutoView(ProdutoId);
            }
        }

        public IEnumerable<ProdutoFornecedorPrecoView> GetListByFornecedor(int ProdutoId)
        {
            using (ProdutoFornecedorPrecoRepository repository = new ProdutoFornecedorPrecoRepository())
            {
                return repository.GetListByFornecedor(ProdutoId);
            }
        }

        public IEnumerable<Colaborador> GetFornecedores(int ProdutoId)
        {
            using (ProdutoFornecedorPrecoRepository repository = new ProdutoFornecedorPrecoRepository())
            {
                return repository.GetFornecedores(ProdutoId);
            }
        }

        public decimal GetCusto(int ProdutoId, int CorId, int TamanhoId)
        {
            using (ProdutoFornecedorPrecoRepository repository = new ProdutoFornecedorPrecoRepository())
            {
                return repository.GetCusto(ProdutoId, CorId, TamanhoId);
            }
        }

        public decimal GetCusto(int ProdutoId, int? tipoCalculoCusto, int IdProduto, short sequencia)
        {
            using (ProdutoFornecedorPrecoRepository repository = new ProdutoFornecedorPrecoRepository())
            {
                return repository.GetCusto(ProdutoId, tipoCalculoCusto, IdProduto, sequencia);
            }
        }

        public decimal GetCustoMaior(int ProdutoId)
        {
            using (ProdutoFornecedorPrecoRepository repository = new ProdutoFornecedorPrecoRepository())
            {
                return repository.GetCustoMaior(ProdutoId);
            }
        }

        public decimal GetCustoMedio(int ProdutoId)
        {
            using (ProdutoFornecedorPrecoRepository repository = new ProdutoFornecedorPrecoRepository())
            {
                return repository.GetCustoMedio(ProdutoId);
            }
        }

        public decimal GetCustoMaiorCor(int ProdutoId)
        {
            using (ProdutoFornecedorPrecoRepository repository = new ProdutoFornecedorPrecoRepository())
            {
                return repository.GetCustoMaiorCor(ProdutoId);
            }
        }

        public decimal GetCustoMedioCor(int ProdutoId)
        {
            using (ProdutoFornecedorPrecoRepository repository = new ProdutoFornecedorPrecoRepository())
            {
                return repository.GetCustoMedioCor(ProdutoId);
            }
        }

        public decimal GetCustoMaiorTamanho(int ProdutoId)
        {
            using (ProdutoFornecedorPrecoRepository repository = new ProdutoFornecedorPrecoRepository())
            {
                return repository.GetCustoMaiorTamanho(ProdutoId);
            }
        }

        public decimal GetCustoMedioTamanho(int ProdutoId)
        {
            using (ProdutoFornecedorPrecoRepository repository = new ProdutoFornecedorPrecoRepository())
            {
                return repository.GetCustoMedioTamanho(ProdutoId);
            }
        }

        public decimal GetCustoFornecedor(int ProdutoId, int FornecedorId)
        {
            using (ProdutoFornecedorPrecoRepository repository = new ProdutoFornecedorPrecoRepository())
            {
                return repository.GetCustoFornecedor(ProdutoId, FornecedorId);
            }
        }

        public decimal GetCustoByExcecao(int ProdutoId, int? tipoCaluloCusto, List<int> idCor, List<int> idTamanho, int IdProduto, short sequencia)
        {
            using (ProdutoFornecedorPrecoRepository repository = new ProdutoFornecedorPrecoRepository())
            {
                return repository.GetCustoByExcecao(ProdutoId, tipoCaluloCusto, idCor, idTamanho, IdProduto, sequencia);
            }
        }

        public decimal GetCustoMedioTamanhoByExcecao(int ProdutoId, List<int> idCor, List<int> idTamanho)
        {
            using (ProdutoFornecedorPrecoRepository repository = new ProdutoFornecedorPrecoRepository())
            {
                return repository.GetCustoMedioTamanhoByExcecao(ProdutoId, idCor, idTamanho);
            }
        }

        public decimal GetCustoMaiorTamanhoByExcecao(int ProdutoId, List<int> idCor, List<int> idTamanho)
        {
            using (ProdutoFornecedorPrecoRepository repository = new ProdutoFornecedorPrecoRepository())
            {
                return repository.GetCustoMaiorTamanhoByExcecao(ProdutoId, idCor, idTamanho);
            }
        }

        public decimal GetCustoMedioCorByExcecao(int ProdutoId, List<int> idCor, List<int> idTamanho)
        {
            using (ProdutoFornecedorPrecoRepository repository = new ProdutoFornecedorPrecoRepository())
            {
                return repository.GetCustoMedioCorByExcecao(ProdutoId, idCor, idTamanho);
            }
        }

        public decimal GetCustoMaiorCorByExcecao(int ProdutoId, List<int> idCor, List<int> idTamanho)
        {
            using (ProdutoFornecedorPrecoRepository repository = new ProdutoFornecedorPrecoRepository())
            {
                return repository.GetCustoMaiorCorByExcecao(ProdutoId, idCor, idTamanho);
            }
        }

        public decimal GetCustoMedioByExcecao(int ProdutoId, List<int> idCor, List<int> idTamanho)
        {
            using (ProdutoFornecedorPrecoRepository repository = new ProdutoFornecedorPrecoRepository())
            {
                return repository.GetCustoMedioByExcecao(ProdutoId, idCor, idTamanho);
            }
        }

        public decimal GetCustoMaiorByExcecao(int ProdutoId, List<int> idCor, List<int> idTamanho)
        {
            using (ProdutoFornecedorPrecoRepository repository = new ProdutoFornecedorPrecoRepository())
            {
                return repository.GetCustoMaiorByExcecao(ProdutoId, idCor, idTamanho);
            }
        }

        public IEnumerable<ProdutoFornecedorPreco> GetValoresSemInativos(int ProdutoId)
        {
            using (ProdutoFornecedorPrecoRepository repository = new ProdutoFornecedorPrecoRepository())
            {
                return repository.GetValoresSemInativos(ProdutoId);
            }
        }


    }
}
