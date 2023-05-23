using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Controllers
{
    public class ItemOrdemProducaoController : GenericController<ItemOrdemProducao, ItemOrdemProducaoRepository>
    {
        public IEnumerable<ItemOrdemProducaoView> GetByPedidoVenda(int pedidoId)
        {
            using (var repository = new ItemOrdemProducaoRepository())
            {
                return repository.GetByPedidoVenda(pedidoId);
            }
        }

        public IEnumerable<ItemOrdemProducaoView> GetByPedido(int ordemId)
        {
            using (var repository = new ItemOrdemProducaoRepository())
            {
                return repository.GetByPedido(ordemId);
            }
        }

        public IEnumerable<ItemOrdemProducaoView> GetByOrdem(int ordemId)
        {
            using (var repository = new ItemOrdemProducaoRepository())
            {
                return repository.GetByOrdem(ordemId);
            }
        }

        public IEnumerable<ItemOrdemProducaoView> GetByProduto(int produtoId)
        {
            using (var repository = new ItemOrdemProducaoRepository())
            {
                return repository.GetByProduto(produtoId);
            }
        }

        public IEnumerable<OrdemProducaoStatusRel> GetByProduto(FiltroOrdemProducao filtro)
        {
            using (var repository = new ItemOrdemProducaoRepository())
            {
                return repository.GetByFiltro(filtro);
            }
        }

        public IEnumerable<OrdemProducaoStatusRel> GetByFiltroOrdem(FiltroOrdemProducao filtro)
        {
            using (var repository = new ItemOrdemProducaoRepository())
            {
                return repository.GetByFiltroOrdem(filtro);
            }
        }

        public IEnumerable<OrdemProducaoStatusRel> GetBySetorFiltro(FiltroOrdemProducao filtro)
        {
            using (var repository = new ItemOrdemProducaoRepository())
            {
                return repository.GetBySetorFiltro(filtro);
            }
        }

        public IEnumerable<OrdemProducaoMaterialView> GetByOrdensParaPedidoCompra(List<int> idsOdens)
        {
            using (var repository = new ItemOrdemProducaoRepository())
            {
                return repository.GetByOrdensParaPedidoCompra(idsOdens);
            }
        }

        public IEnumerable<OrdemCompra> GetByOrdensParaCompra(List<int> idsOdens, List<int> idsGrupos)
        {
            using (var repository = new ItemOrdemProducaoRepository())
            {
                var ordens = repository.GetByOrdensParaCompra(idsOdens, idsGrupos).ToList();
                ordens.ForEach(o =>
                    {
                        o.QuantidadeNecessaria = Convert.ToDecimal(VestilloSession.FormatarQuantidade(o.QuantidadeNecessaria.ToString()));
                        o.Preco = Convert.ToDecimal(VestilloSession.FormatarMoeda(new ProdutoFornecedorPrecoRepository().GetCusto(o.MateriaPrimaId, o.CorId, o.TamanhoId).ToString()));
                        
                        var compras = new ItemPedidoCompraRepository().GetByMaterialEOrdem(o, idsOdens).ToList();
                        
                        if(compras != null && compras.Count > 0){
                            o.CompraEfetiva = Convert.ToDecimal(VestilloSession.FormatarQuantidade(compras.Sum(c => c.Qtd).ToString()));
                            o.CompraFaltante = Convert.ToDecimal(VestilloSession.FormatarQuantidade(compras.Sum(c => c.QtdRestante).ToString()));
                            
                            List<NotaEntrada> nfes = new ItemPedidoCompraRepository().GetByNFe(compras).ToList();
                            if (nfes != null)
                                o.Nfe = String.Join("|", nfes.Select(p => p.Referencia));
                            //o.Preco = new FichaTecnicaDoMaterialRelacaoRepository().get
                        }
                    });
                return ordens;
            }
        }

        public IEnumerable<OrdemCompra> GetByOrdensParaCompraComEstoque(List<int> idsOdens, List<int> idsGrupos)
        {
            using (var repository = new ItemOrdemProducaoRepository())
            {
                var ordens = repository.GetByOrdensParaCompra(idsOdens, idsGrupos).ToList();
                ordens.ForEach(o =>
                {
                    o.QuantidadeEmpenhada = Convert.ToDecimal(VestilloSession.FormatarQuantidade(o.QuantidadeEmpenhada.ToString()));
                    o.QuantidadeNecessaria = Convert.ToDecimal(VestilloSession.FormatarQuantidade(o.QuantidadeNecessaria.ToString()));
                    o.QuantidadeBaixada = Convert.ToDecimal(VestilloSession.FormatarQuantidade(o.QuantidadeBaixada.ToString()));

                    o.Preco = Convert.ToDecimal(VestilloSession.FormatarMoeda(new ProdutoFornecedorPrecoRepository().GetCusto(o.MateriaPrimaId, o.CorId, o.TamanhoId).ToString()));

                    var compras = new ItemPedidoCompraRepository().GetByMaterial(o).ToList();
                    var estoque = new EstoqueRepository().GetSaldoAtualProduto(o.ArmazemId, o.MateriaPrimaId, o.CorId, o.TamanhoId);
                    if (estoque != null)
                    {
                        o.SaldoDisponivel = Convert.ToDecimal(VestilloSession.FormatarQuantidade(estoque.Saldo.ToString()));

                    }

                    if (compras != null && compras.Count > 0)
                    {
                        o.CompraEfetiva = Convert.ToDecimal(VestilloSession.FormatarQuantidade((compras.Sum(c => c.Qtd) - compras.Sum(c => c.QtdAtendida)).ToString()));
                        o.CompraFaltante = Convert.ToDecimal(VestilloSession.FormatarQuantidade(compras.Sum(c => c.QtdRestante).ToString()));
                        List<NotaEntrada> nfes = new ItemPedidoCompraRepository().GetByNFe(compras).ToList();
                        if (nfes != null)
                            o.Nfe = String.Join("|", nfes.Select(p => p.Referencia));
                        //o.Preco = new FichaTecnicaDoMaterialRelacaoRepository().get
                    }

                    o.CompraFaltante = -(o.QuantidadeNecessaria - o.SaldoDisponivel - o.CompraEfetiva - o.QuantidadeEmpenhada - o.QuantidadeBaixada);
                });
                return ordens;
            }
        }

        public void LimparVinculoPedidoOrdem(int idItemPedido)
        {
            using (var repository = new ItemOrdemProducaoRepository())
            {
                repository.LimparVinculoPedidoOrdem(idItemPedido);
            }
        }


        public IEnumerable<GestaoOrdemCompra> GetByGestaoOrdemCompra(List<int> idsOdens, List<int> idsMateriaPrima, DateTime DaInclusao, DateTime AteInclusao)
        {
            using (var repository = new ItemOrdemProducaoRepository())
            {
                var ordens = repository.GetByGestaoOrdemCompra(idsOdens, idsMateriaPrima, DaInclusao, AteInclusao).ToList();

                ordens.ForEach(o =>
                {
                    o.Preco = new ProdutoFornecedorPrecoRepository().GetCusto(o.MateriaPrimaId, o.CorId, o.TamanhoId);

                    var compras = new ItemPedidoCompraRepository().GetByMaterialGestaoCompra(o).ToList();
                    var estoque = new EstoqueRepository().GetSaldoAtualProdutoGestaoCompra(o.MateriaPrimaId, o.CorId, o.TamanhoId);
                    decimal NecessTotal = repository.GetByQuantidadeNecessariaTotal(o.MateriaPrimaId, o.CorId, o.TamanhoId);
                    o.QuantidadeNecessariaTotal = NecessTotal;

                    if(idsOdens.Count > 0)
                    {

                       
                        decimal PedidosPorOp = repository.GetByQuantidadeEmPEdidoPorOrdem(idsOdens, o.MateriaPrimaId, o.CorId, o.TamanhoId);
                        o.CompraEfetivaPorOp = PedidosPorOp;
                    }
                    

                    if (estoque != null)
                    {
                        o.SaldoDisponivel = estoque.Saldo;
                        o.EstoqueTotal = estoque.Saldo + estoque.Empenhado;
                        o.QuantidadeEmpenhadaTotal = estoque.Empenhado;

                    }

                    if (compras != null && compras.Count > 0)
                    {
                        o.CompraEfetiva = compras.Sum(c => c.Qtd) - compras.Sum(c => c.QtdAtendida);
                        o.CompraFaltante = compras.Sum(c => c.QtdRestante);

                        //List<NotaEntrada> nfes = new ItemPedidoCompraRepository().GetByNFe(compras).ToList();
                        //if (nfes != null)
                        //    o.Nfe = String.Join("|", nfes.Select(p => p.Referencia));
                        //o.Preco = new FichaTecnicaDoMaterialRelacaoRepository().get
                    }

                    List<NotaEntrada> nfes = new ItemPedidoCompraRepository().GetByNotaEntrada(o.MateriaPrimaId, o.CorId, o.TamanhoId, DaInclusao, AteInclusao).ToList();

                    if(nfes != null && nfes.Count > 0)
                    {
                        o.Nfe = "SIM";
                    }
                    else
                    {
                        o.Nfe = "NÃO";
                    }

                    o.NecessidadeCompraTotal = (o.CompraEfetiva + o.SaldoDisponivel) - o.QuantidadeNecessariaTotal;
                    o.NecessidadeCompraOp = (o.CompraEfetivaPorOp + o.SaldoDisponivel) - o.QuantidadeNecessaria;
                    o.CompraFaltante = -(o.QuantidadeNecessaria - o.SaldoDisponivel - o.CompraEfetiva - o.QuantidadeEmpenhada - o.QuantidadeBaixada);
                });
                return ordens;
            }
        }

        public void FinalizaOrdemTela(List<ItensOrdemFinalizacaoView> itensDaOp)
        {
            using (ItemOrdemProducaoRepository ordemRepository = new ItemOrdemProducaoRepository())
            {
                var listMovimentacaoEstoque = new List<MovimentacaoEstoque>();
                EstoqueController estoqueController = new EstoqueController();

                try
                {

                    ordemRepository.BeginTransaction();
                    

                    foreach (var itemOrdem in itensDaOp)
                    {
                        var movEstoque = new MovimentacaoEstoque();

                        movEstoque.Entrada = itemOrdem.Total;
                        movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                        movEstoque.ProdutoId = itemOrdem.IdProduto;
                        movEstoque.CorId = itemOrdem.IdCor;
                        movEstoque.TamanhoId = itemOrdem.IdTamanho;
                        movEstoque.AlmoxarifadoId = itemOrdem.IdAlmoxarifado;
                        movEstoque.Observacao = "Finalização Ordem Produção: " + itemOrdem.RefOrdem;
                        movEstoque.DataMovimento = DateTime.Now;                        
                        listMovimentacaoEstoque.Add(movEstoque);
                    }

                    ordemRepository.FinalizaOrdemTela(itensDaOp);

                    if (listMovimentacaoEstoque.Count > 0)
                    {

                        estoqueController.MovimentarEstoque(listMovimentacaoEstoque);
                    }


                    ordemRepository.CommitTransaction();

                }
                catch (Exception ex)
                {

                    ordemRepository.RollbackTransaction();
                    throw ex;
                }
            }

        }

        public void FinalizaItemOrdemTela(List<ItensOrdemFinalizacaoView> itensDaOp) //Honey Be
        {
            using (ItemOrdemProducaoRepository ordemRepository = new ItemOrdemProducaoRepository())
            {

                try
                {

                    ordemRepository.BeginTransaction();
                    ordemRepository.FinalizaItemOrdemTela(itensDaOp);

                    ordemRepository.CommitTransaction();

                }
                catch (Exception ex)
                {

                    ordemRepository.RollbackTransaction();
                    throw ex;
                }
            }

        }
    }
}
