using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;


namespace Vestillo.Business.Controllers
{
    public class ProdutoDetalheController : GenericController<ProdutoDetalhe, ProdutoDetalheRepository>
    {
        public ProdutoDetalhe GetByProduto(int ProdutoId)
        {
            using (ProdutoDetalheRepository repository = new ProdutoDetalheRepository())
            {
                return repository.GetByProduto(ProdutoId);
            }
        }

        public IEnumerable<ProdutoDetalhe> GetListByProduto(int ProdutoId, int ativo)
        {
            using (ProdutoDetalheRepository repository = new ProdutoDetalheRepository())
            {
                return repository.GetListByProduto(ProdutoId, ativo);
            }
        }

        public ProdutoDetalheView GetViewByProduto(int ProdutoId)
        {
            using (ProdutoDetalheRepository repository = new ProdutoDetalheRepository())
            {
                return repository.GetViewByProduto(ProdutoId);
            }
        }

        public IEnumerable<ProdutoDetalheView> GetListViewByProduto(int ProdutoId, int ativo, int ExibeSemuso = 0)
        {
            using (ProdutoDetalheRepository repository = new ProdutoDetalheRepository())
            {
                var listaItens = repository.GetListViewByProduto(ProdutoId, ativo, ExibeSemuso).ToList();

                if (VestilloSession.UsaOrdenacaoFixa && listaItens.Count > 0 )
                {
                    List<ProdutoDetalheView> listaOrdenada = new List<ProdutoDetalheView>();
                    List<ProdutoDetalheView> listaOrdenadaProdCor = new List<ProdutoDetalheView>();

                    var anteriorCor = listaItens.First().AbvCor;
                    listaItens.ForEach(item =>
                    {
                        var corAtual = item.AbvCor;

                        if (corAtual != anteriorCor)
                        {
                            listaOrdenada.AddRange(retornaTamanhoOrdenado(listaOrdenadaProdCor));
                            listaOrdenadaProdCor.Clear();
                            listaOrdenadaProdCor.Add(item);
                        }
                        else
                        {
                            listaOrdenadaProdCor.Add(item);
                        }

                        if (item == listaItens.Last())
                        {
                            listaOrdenada.AddRange(retornaTamanhoOrdenado(listaOrdenadaProdCor));
                            listaOrdenadaProdCor.Clear();

                        }

                        anteriorCor = corAtual;

                    });

                    listaItens = listaOrdenada;


                }

                return listaItens.AsEnumerable();
            }
        }

        public List<ProdutoDetalheView> retornaTamanhoOrdenado(List<ProdutoDetalheView> listaNaoOrddenada)
        {
            List<ProdutoDetalheView> listaOrdenada = new List<ProdutoDetalheView>();
            var listaNumerica = listaNaoOrddenada.Where(l => int.TryParse(l.AbvTamanho, out _))
                                                .OrderBy(l => l.AbvCor)
                                                .ThenBy(l => l.AbvTamanho)
                                                .ToList();

            listaOrdenada.AddRange(listaNumerica);

            var listaNaoNumerica = listaNaoOrddenada.Where(l => !int.TryParse(l.AbvTamanho, out _))
                                                .OrderBy(l => l.AbvCor)
                                                .ThenBy(l => l.IdTamanho)
                                                .ToList();

            listaOrdenada.AddRange(listaNaoNumerica);

            return listaOrdenada;
        }

        public IEnumerable<ProdutoDetalheView> GetByDetalheCodBarras(string codBarras)
        {
            using (ProdutoDetalheRepository repository = new ProdutoDetalheRepository())
            {
                return repository.GetByDetalheCodBarras(codBarras);
            }
        }

        public void AtualizarGrade(ProdutoGradeView grade)
        {
            _repository.AtualizarGrade(grade);
        }

        public IEnumerable<ProdutoGradeView> GetGradeParaEdicao(int[] produtos, int[] cores, int[] tamanhos, int pagina, int registrosPorPagina, int tipoItem, out int totalRegistros)
        {
            return _repository.GetGradeParaEdicao(produtos, cores, tamanhos, pagina, registrosPorPagina, tipoItem, out totalRegistros);
        }

        public IEnumerable<ProdutoDetalhe> GetAtivos(int[] produtos, int[] cores, int[] tamanhos)
        {
            return _repository.GetAtivos(produtos, cores, tamanhos);
        }

        public IEnumerable<ProdutoDetalhe> GetTamanhosDoProdutoECor(int produto, int cor)
        {
            return _repository.GetTamanhosDoProdutoECor(produto, cor);
        }

        public IEnumerable<ProdutoDetalhe> GetCoresDoProdutoETamanho(int produto, int tamanho)
        {
            return _repository.GetCoresDoProdutoETamanho(produto, tamanho);
        }

        public ProdutoDetalhe GetByGrade(int produtoId, int cor, int tamanho)
        {
            return _repository.GetByGrade(produtoId, cor, tamanho);
        }

        public bool VerificarTamanhoUnico(int produtoId, int tamanho)
        {
            return _repository.VerificarTamanhoUnico(produtoId, tamanho);
        }

        public bool VerificarCorUnica(int produtoId, int cor)
        {
            return _repository.VerificarCorUnica(produtoId, cor);
        }
    }
}
