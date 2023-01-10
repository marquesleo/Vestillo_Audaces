using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo;

namespace Vestillo.Business.Controllers
{
    public class OrdemProducaoController : GenericController<OrdemProducao, OrdemProducaoRepository>
    {

        public OrdemProducaoView GetByIdView(int id)
        {
            using (var repository = new OrdemProducaoRepository())
            {
                return repository.GetByIdView(id);
            }
        }

        public IEnumerable<OrdemProducaoView> GetAllView(int IdOp = 0)
        {
            using (var repository = new OrdemProducaoRepository())
            {
                return repository.GetAllView(IdOp);
            }
        }

        public List<int> GetSemanas()
        {
            using (var repository = new OrdemProducaoRepository())
            {
                return repository.GetSemanas();
            }
        }

        public IEnumerable<OrdemProducao> GetAllByProduto(int produtoId, bool ExcluirRegisto)
        {
            using (var repository = new OrdemProducaoRepository())
            {
                return repository.GetAllByProduto(produtoId, ExcluirRegisto);
            }
        }

        public override void Delete(int id)
        {
            var itemOrdemProducaoRepository = new ItemOrdemProducaoRepository();

            try
            {
                itemOrdemProducaoRepository.BeginTransaction();
                var itemLiberacaoPedidoVendaRepository = new ItemLiberacaoPedidoVendaRepository();
                IEnumerable<ItemOrdemProducaoView> itens = itemOrdemProducaoRepository.GetByPedido(id);
                bool ExistemEssePedidoEmOutrasOps = false;
                var AtualizaPedido = new PedidoVendaRepository();

                if (itens != null && itens.Count() > 0)
                {
                    foreach (var i in itens)
                    {
                        if (i.PedidoVendaId != null && i.PedidoVendaId > 0)
                        {
                            var itemLiberacaoVenda = itemLiberacaoPedidoVendaRepository.GetById(Convert.ToInt32(i.SeqLiberacaoPedido));
                            if (itemLiberacaoVenda != null)
                            {
                                if(itemLiberacaoVenda.SemEmpenho == 1)
                                {
                                    var saldoAtual = new EstoqueController().GetSaldoAtualProduto(itemLiberacaoVenda.AlmoxarifadoId, i.ProdutoId, Convert.ToInt32(i.CorId), Convert.ToInt32(i.TamanhoId));
                                    if (saldoAtual != null)
                                    {
                                        if (saldoAtual.Saldo < 0)
                                            saldoAtual.Saldo = 0;

                                        var qtdNaoAtendida = i.Quantidade - saldoAtual.Saldo;
                                        itemLiberacaoVenda.QtdNaoAtendida = qtdNaoAtendida < 0 ? 0 : qtdNaoAtendida;

                                        if (itemLiberacaoVenda.QtdNaoAtendida == 0)
                                            itemLiberacaoVenda.Status = (int)enumStatusLiberacaoPedidoVenda.Atendido;
                                        else if(itemLiberacaoVenda.QtdNaoAtendida == itemLiberacaoVenda.Qtd)
                                            itemLiberacaoVenda.Status = (int)enumStatusLiberacaoPedidoVenda.Sem_Estoque;
                                        else
                                            itemLiberacaoVenda.Status = (int)enumStatusLiberacaoPedidoVenda.Atendido_Parcial;
                                    }
                                }
                                else
                                {
                                    itemLiberacaoVenda.QtdNaoAtendida += i.Quantidade;
                                    if (itemLiberacaoVenda.QtdNaoAtendida == itemLiberacaoVenda.Qtd)
                                    {
                                        itemLiberacaoVenda.Status = (int)enumStatusLiberacaoPedidoVenda.Sem_Estoque;
                                    }
                                    else
                                    {
                                        itemLiberacaoVenda.Status = (int)enumStatusLiberacaoPedidoVenda.Atendido_Parcial;
                                    }
                                }                                

                                itemLiberacaoPedidoVendaRepository.Save(ref itemLiberacaoVenda);
                            }

                            /*
                            ExistemEssePedidoEmOutrasOps = itemOrdemProducaoRepository.PedidoExisteEmOutrasOps(Convert.ToInt32(i.PedidoVendaId), id);
                            if(ExistemEssePedidoEmOutrasOps == false)
                            {
                                //tratar mudança de pedido para producao = 1                               
                               AtualizaPedido.UpdatePedidoEmProducao(Convert.ToInt32(Convert.ToInt32(i.PedidoVendaId)), false);                               

                            }
                            */

                            //itemLiberacaoPedidoVendaRepository.GetById(i.Id);
                        }
                        itemOrdemProducaoRepository.Delete(i.Id);
                    }
                }
                TrataOrdemAberto();

                base.Delete(id);

                itemOrdemProducaoRepository.CommitTransaction();
            }
            catch (Exception ex)
            {
                itemOrdemProducaoRepository.RollbackTransaction();
                throw ex;
            }
        }

        public override void Save(ref OrdemProducao ordem)
        {
            var itemOrdemProducaoRepository = new ItemOrdemProducaoRepository();
            var itemLiberacaoRepository = new ItemLiberacaoPedidoVendaRepository();
            var AtualizaPedido = new PedidoVendaRepository();

            try
            {
                itemOrdemProducaoRepository.BeginTransaction();
                base.Save(ref ordem);


                if (ordem.Itens != null && ordem.Itens.Count() > 0)
                {
                    //var itens = ordem.Itens.Where(x => x.Quantidade > 0).ToList();

                    foreach (ItemOrdemProducaoView item in ordem.Itens)
                    {
                        var itemLiberacaoVenda = itemLiberacaoRepository.GetById(item.LiberacaoItemId);
                        if (itemLiberacaoVenda != null)
                        {
                            itemLiberacaoVenda.QtdNaoAtendida -= item.Quantidade;
                            if (itemLiberacaoVenda.QtdNaoAtendida < 0) itemLiberacaoVenda.QtdNaoAtendida = 0;
                            itemLiberacaoVenda.Status = (int)enumStatusLiberacaoPedidoVenda.Producao;
                            itemLiberacaoRepository.Save(ref itemLiberacaoVenda);
                        }

                        //tratar mudança de pedido para producao = 1
                        /*
                        if(item.PedidoVendaId != null && item.PedidoVendaId > 0)
                        {                           
                            AtualizaPedido.UpdatePedidoEmProducao(Convert.ToInt32(item.PedidoVendaId), true);
                        }
                        */

                        ItemOrdemProducao itemSave;
                        itemSave = item;
                        itemSave.OrdemProducaoId = ordem.Id;

                        if (item.Quantidade != 0)
                        {
                            itemOrdemProducaoRepository.Save(ref itemSave);
                        }
                        else
                            itemOrdemProducaoRepository.Delete(item.Id);

                    }
                }
                itemOrdemProducaoRepository.CommitTransaction();

                using (var repository = new OrdemProducaoRepository())
                {
                    repository.AtualizaTotalItens(ordem.Id);
                }


            }
            catch (Exception ex)
            {
                itemOrdemProducaoRepository.RollbackTransaction();
                throw ex;
            }
        }

        public IEnumerable<OrdemProducaoView> GetByRefView(string referencia, int IdColecao = 0)
        {
            using (var repository = new OrdemProducaoRepository())
            {
                return repository.GetByRefView(referencia,IdColecao);
            }
        }

        public void UpdateStatus(int OrdemId)
        {
            using (var repository = new OrdemProducaoRepository())
            {
                repository.UpdateStatus(OrdemId);
            }
        }

        public void TrataOrdemAberto()
        {
            using (var repository = new OrdemProducaoRepository())
            {
                repository.TrataOrdemAberto();
            }
        }

        public void EnviarParaCorte(bool cancelaEnvio, int ordemId)
        {
            using (var repository = new OrdemProducaoRepository())
            {
                repository.EnviarParaCorte(cancelaEnvio, ordemId);
            }
        }

        public void UpdateObsMateriais(int ordemId, string observacao)
        {
            using (var repository = new OrdemProducaoRepository())
            {
                repository.UpdateObsMateriais(ordemId, observacao);
            }
        }

        public IEnumerable<OrdemProducaoView> GetByItem(string referencia)
        {
            using (var repository = new OrdemProducaoRepository())
            {
               return repository.GetByItem(referencia);
            }
        }
    }
}
