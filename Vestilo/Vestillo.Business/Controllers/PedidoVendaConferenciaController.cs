using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class PedidoVendaConferenciaController : GenericController<PedidoVendaConferencia, PedidoVendaConferenciaRepository>
    {
        public IEnumerable<PedidoVendaConferenciaView> GetListParaConferencia()
        {
            return  _repository.GetListParaConferencia();            
        }

        public IEnumerable<PedidoVendaConferenciaitensDciView> GetListParaConferenciaDci()
        {
            return _repository.GetListParaConferenciaDci();
        }

        public IEnumerable<PedidoVendaConferenciaView> GetListParaConferenciaSemEmpenho()
        {
            return _repository.GetListParaConferenciaSemEmpenho();
        }

        public override PedidoVendaConferencia GetById(int id)
        {
            PedidoVendaConferencia conferencia = base.GetById(id);

            if (conferencia != null)
            {
                PedidoVendaConferenciaItemRepository pedidoVendaConferenciaItemRepository = new PedidoVendaConferenciaItemRepository();
                conferencia.Itens = pedidoVendaConferenciaItemRepository.GetListByPedidoVendaConferencia(id);
            }

            return conferencia;
        }

        public void Save(ref PedidoVendaConferencia conferencia, bool atualizarLiberacoes, bool semEmpenho = false)
        {
            bool openTransaction = false;

            try
            {
                openTransaction = _repository.BeginTransaction();

                base.Save(ref conferencia);

                PedidoVendaConferenciaItemRepository pedidoVendaConferenciaItemRepository = new PedidoVendaConferenciaItemRepository();

                foreach (PedidoVendaConferenciaItem i in conferencia.Itens)
                {
                    PedidoVendaConferenciaItem item = i;

                    item.Id = 0;
                    item.PedidoVendaConferenciaId = conferencia.Id;
                    item.QtdAtualizarConferencia = item.QtdConferida;
                    pedidoVendaConferenciaItemRepository.Save(ref item);
                }

                if (atualizarLiberacoes)
                {
                    ItemLiberacaoPedidoVendaRepository itemLiberacaoPedidoVendaRepository = new ItemLiberacaoPedidoVendaRepository();

                    //Ordena os pedidos para pegar o de menor id, ou seja, o pedido mais antigo
                    int[] pedidosIds = conferencia.PedidosIds.OrderBy(x => x).GroupBy(x => x)
                           .Select(grp => grp.Key).ToArray();

                    foreach (int pedidoVendaId in pedidosIds)
                    {
                        foreach (PedidoVendaConferenciaItem i in conferencia.Itens)
                        {
                            PedidoVendaConferenciaItem item = i;
                            
                            if (item.QtdAtualizarConferencia > 0)
                            {
                                if (!semEmpenho)
                                {
                                    List<ItemLiberacaoPedidoVenda> itensLiberacao = itemLiberacaoPedidoVendaRepository.GetLiberacaoPedidoVendaPorPedidoProdutoEGrade(pedidoVendaId, item.ProdutoId, item.CorId.GetValueOrDefault(), item.TamanhoId.GetValueOrDefault()).ToList();
                                    itensLiberacao.ForEach(itemL => itemL.QtdConferida = 0);
                                    var pedidoDci = new PedidoVendaController().GetById(pedidoVendaId);
                                    //ItemLiberacaoPedidoVenda liberacao = itensLiberacao.Where(x => x.QtdConferencia < x.Qtd).FirstOrDefault();
                                    if (itensLiberacao != null)
                                    {
                                        foreach (ItemLiberacaoPedidoVenda itemLiberacao in itensLiberacao)
                                        {
                                            ItemLiberacaoPedidoVenda liberacao = itemLiberacao;
                                            if (liberacao != null)
                                            {
                                                if (item.QtdAtualizarConferencia > liberacao.QtdConferencia)
                                                {
                                                    liberacao.QtdConferida += liberacao.QtdConferencia;
                                                    item.QtdAtualizarConferencia -= liberacao.QtdConferencia;
                                                    //rotina DCI
                                                    if (VestilloSession.EmpresaLogada.CNPJ == "31.584.436/0001-07" && pedidoDci.ClienteId == 30 || pedidoDci.ClienteId == 52114)
                                                    {
                                                        if(liberacao.QtdConferencia > 0)
                                                        {
                                                            using (PedidoVendaConferenciaItemRepository ConferenciaItemRepository = new PedidoVendaConferenciaItemRepository())
                                                            {
                                                                var itemConf = new PedidoVendaConferenciaitensDci();
                                                                itemConf.Id = 0;
                                                                itemConf.PedidoVendaConferenciaId = conferencia.Id;
                                                                itemConf.PedidoVendaId = pedidoVendaId;
                                                                itemConf.ProdutoId = item.ProdutoId;
                                                                itemConf.TamanhoId = Convert.ToInt32(item.TamanhoId);
                                                                itemConf.CorId = Convert.ToInt32(item.CorId);
                                                                itemConf.QtdConferida = liberacao.QtdConferencia;
                                                                itemConf.Observacao = pedidoDci.Obs;
                                                                ConferenciaItemRepository.GravarItensConferenciaDci(itemConf);
                                                            }
                                                        }

                                                    }

                                                }
                                                else
                                                {
                                                    liberacao.QtdConferida += item.QtdAtualizarConferencia;
                                                    var qtdConf = item.QtdAtualizarConferencia;
                                                    item.QtdAtualizarConferencia = 0;
                                                    //rotina DCI
                                                    if (VestilloSession.EmpresaLogada.CNPJ == "31.584.436/0001-07" && pedidoDci.ClienteId == 30 || pedidoDci.ClienteId == 52114)
                                                    {
                                                        if(qtdConf > 0 )
                                                        {
                                                            using (PedidoVendaConferenciaItemRepository ConferenciaItemRepository = new PedidoVendaConferenciaItemRepository())
                                                            {
                                                                var itemConf = new PedidoVendaConferenciaitensDci();
                                                                itemConf.Id = 0;
                                                                itemConf.PedidoVendaConferenciaId = conferencia.Id;
                                                                itemConf.PedidoVendaId = pedidoVendaId;
                                                                itemConf.ProdutoId = item.ProdutoId;
                                                                itemConf.TamanhoId = Convert.ToInt32(item.TamanhoId);
                                                                itemConf.CorId = Convert.ToInt32(item.CorId);
                                                                itemConf.QtdConferida = qtdConf;
                                                                itemConf.Observacao = pedidoDci.Obs;
                                                                ConferenciaItemRepository.GravarItensConferenciaDci(itemConf);
                                                            }
                                                        }
                        
                                                    }
                                                }

                                                itemLiberacaoPedidoVendaRepository.Save(ref liberacao);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    List<ItemLiberacaoPedidoVenda> itensLiberacao = itemLiberacaoPedidoVendaRepository.GetLiberacaoPedidoVendaPorPedidoProdutoEGrade(pedidoVendaId, item.ProdutoId, item.CorId.GetValueOrDefault(), item.TamanhoId.GetValueOrDefault()).ToList();
                                    var pedido = new PedidoVendaController().GetById(pedidoVendaId);
                                    
                                    if (itensLiberacao != null)
                                    {
                                        var listMovimentacaoEstoque = new List<MovimentacaoEstoque>();
                                        foreach (ItemLiberacaoPedidoVenda itemLiberacao in itensLiberacao)
                                        {
                                            ItemPedidoVenda itemPedidoVenda = new ItemPedidoVendaRepository().GetById(itemLiberacao.ItemPedidoVendaId);
                                            ItemLiberacaoPedidoVenda liberacao = itemLiberacao;
                                            if (liberacao != null)
                                            {
                                                decimal qtdEmpenhar = 0;
                                                if ((item.QtdAtualizarConferencia + liberacao.QtdConferida) > liberacao.QtdConferencia)
                                                    qtdEmpenhar = liberacao.QtdConferencia - liberacao.QtdConferida;
                                                else
                                                    qtdEmpenhar = item.QtdAtualizarConferencia;

                                                var movEstoque = new MovimentacaoEstoque();
                                                movEstoque.Empenho = true;
                                                movEstoque.AlmoxarifadoId = itemLiberacao.AlmoxarifadoId;
                                                movEstoque.Saida = qtdEmpenhar;
                                                movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                                                movEstoque.ProdutoId = item.ProdutoId;
                                                movEstoque.CorId = item.CorId;
                                                movEstoque.TamanhoId = item.TamanhoId;
                                                movEstoque.Observacao = "SAÍDA PELA CONFERÊNCIA DO PEDIDO " + pedido.Referencia ;
                                                movEstoque.DataMovimento = DateTime.Now;
                                                listMovimentacaoEstoque.Add(movEstoque);

                                                liberacao.QtdEmpenhada += qtdEmpenhar;
                                                liberacao.QtdConferida += qtdEmpenhar;
                                                item.QtdAtualizarConferencia -= qtdEmpenhar;

                                                pedido.QtdEmpenhada += qtdEmpenhar;
                                                pedido.ValorEmpenhadoTotal += (qtdEmpenhar * itemPedidoVenda.Preco); 

                                                itemLiberacaoPedidoVendaRepository.Save(ref liberacao);
                                            }

                                        }

                                        if (listMovimentacaoEstoque.Count > 0)
                                        {
                                            new EstoqueController().MovimentarEstoque(listMovimentacaoEstoque, false);
                                        }

                                        new PedidoVendaRepository().Save(ref pedido);
                                    }
                                }    
                            }
                        }
                    }
                }
                
                if (openTransaction)
                    _repository.CommitTransaction();
            }
            catch (Exception ex)
            {
                if (openTransaction)
                    _repository.RollbackTransaction();

                throw ex;
            }
        }

    }
}
