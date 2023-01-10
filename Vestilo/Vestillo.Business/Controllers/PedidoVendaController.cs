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
    public class PedidoVendaController: GenericController<PedidoVenda, PedidoVendaRepository>
    {

        public PedidoVendaView GetByIdView(int id)
        {
            using (var repository = new PedidoVendaRepository())
            {
                return repository.GetByIdView(id);
            }   
        }

        public PedidoVenda GetByIdAtualizacao(int id)
        {
            using (var repository = new PedidoVendaRepository())
            {
                return repository.GetByIdAtualizacao(id);
            }
        }

        public PedidoVendaView GetByIdViewLiberacao(int id)
        {
            using (var repository = new PedidoVendaRepository())
            {
                return repository.GetByIdViewLiberacao(id);
            }
        }

        public PedidoVendaView GetByIdViewAgrupado(int id)
        {
            using (var repository = new PedidoVendaRepository())
            {
                return repository.GetByIdViewAgrupado(id);
            }
        }

        public PedidoVendaView GetByItemIdView(int id)
        {
            using (var repository = new PedidoVendaRepository())
            {
                return repository.GetByItemIdView(id);
            }
        }

        public IEnumerable<PedidoVendaView> GetAllView()
        {
            using (var repository = new PedidoVendaRepository())
            {
                return repository.GetAllView();
            }   
        }

        public List<PedidoVendaLiberacaoView> GetPedidoLiberacao(int StatusPedido = 0, int StatusPedido2 = 0)
        {
            using (var repository = new PedidoVendaRepository())
            {
                return repository.GetPedidoLiberacao(StatusPedido, StatusPedido2);
            }   
        }

        public List<PedidoVendaLiberacaoView> GetPedidoLiberacaoParaConferencia()
        {
            using (var repository = new PedidoVendaRepository())
            {
                return repository.GetPedidoLiberacaoParaConferencia();
            }
        }

        public List<PedidoVendaLiberacaoView> GetPedidoLiberacaoParaConferencia(int codigo)
        {
            using (var repository = new PedidoVendaRepository())
            {
                return repository.GetPedidoLiberacaoParaConferencia(codigo);
            }
        }

        public IEnumerable<PedidoVendaView> GetListPorReferencia(string referencia, string parametrosDaBusca)
        {
            using (PedidoVendaRepository repository = new PedidoVendaRepository())
            {
                return repository.GetListPorReferencia(referencia, parametrosDaBusca);
            }
        }

        public IEnumerable<PedidoVendaView> GetListPorCliente(string Cliente, string parametrosDaBusca)
        {
            using (PedidoVendaRepository repository = new PedidoVendaRepository())
            {
                return repository.GetListPorCliente(Cliente, parametrosDaBusca);
            }
        }

        public override void Save(ref PedidoVenda pedido)
        {
            var itemPedidoVendaRepository = new ItemPedidoVendaRepository();
            var contasReceberController = new ContasReceberController();
            var itemLiberacaoRepository = new ItemLiberacaoPedidoVendaRepository();
            var tipoMovimentacaoRepository = new TipoMovimentacaoRepository();
            TipoMovimentacao tipoMov = new TipoMovimentacao(); 

            try
            {
                itemPedidoVendaRepository.BeginTransaction();
                if (pedido.Status != (int)enumStatusPedidoVenda.Conferencia && pedido.Status != (int)enumStatusPedidoVenda.Conferencia_Parcial)
                {
                    pedido.Impresso = "N";
                }
                base.Save(ref pedido);

                if (pedido.Itens != null && pedido.Itens.Count() > 0)
                {
                    var itens = pedido.Itens.Where(x => x.Qtd > 0).ToList();
                    tipoMov = tipoMovimentacaoRepository.GetById(itens[0].TipoMovimentacaoId);

                    foreach (ItemPedidoVenda item in itens)
                    {
                        ItemPedidoVenda itemSave;
                        if (item.Id > 0)
                        {
                            itemSave = itemPedidoVendaRepository.GetById(item.Id);
                            itemSave.Qtd = item.Qtd;
                            itemSave.QtdUnidadeMedida2 = item.QtdUnidadeMedida2;
                            itemSave.Preco = item.Preco;
                            itemSave.ReferenciaPedidoCliente = item.ReferenciaPedidoCliente;
                            itemSave.SeqPedCliente = item.SeqPedCliente;
                            itemSave.PrecoUnidadeMedida2 = item.PrecoUnidadeMedida2;
                            itemSave.TipoMovimentacaoId = tipoMov.Id;
                        }
                        else
                        {
                            itemSave = item;
                        }

                        itemSave.PedidoVendaId = pedido.Id;

                        if (itemSave.Qtd != 0)
                            itemPedidoVendaRepository.Save(ref itemSave);

                        item.Id = itemSave.Id;
                    }
                }

                if (pedido.Parcelas != null && pedido.Parcelas.Count() > 0)
                {

                    foreach (ContasReceber p in pedido.Parcelas)
                    {
                        var parcela = p;

                        if (p.Excluir && p.Id > 0)
                        {
                            contasReceberController.Delete(p.Id);
                        }
                        else
                        {
                            parcela.IdPedidoVenda = pedido.Id;
                            contasReceberController.Save(ref parcela);
                        }
                    }
                }

                if (pedido.ItensLiberacao != null && pedido.ItensLiberacao.Count() > 0)
                {
                    var listMovimentacaoEstoque = new List<MovimentacaoEstoque>();
                    EstoqueController estoqueController = new EstoqueController();

                    foreach (var il in pedido.ItensLiberacao)
                    {
                        var itemLiberacao = il;

                        if (itemLiberacao.Qtd == 0 && itemLiberacao.Id > 0) //excluiu item liberacao
                        {
                            if (tipoMov.AtualizaEstoque == 1 && (itemLiberacao.SemEmpenho == 0 || (itemLiberacao.QtdEmpenhada > 0 && itemLiberacao.SemEmpenho == 1)) )
                            {
                                var itemLiberacaoOld = itemLiberacaoRepository.GetById(itemLiberacao.Id);

                                var movEstoque = new MovimentacaoEstoque();
                                movEstoque.Empenho = true;
                                movEstoque.AlmoxarifadoId = itemLiberacao.AlmoxarifadoId;
                                movEstoque.Entrada = itemLiberacaoOld.QtdEmpenhada;
                                movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                                movEstoque.ProdutoId = itemLiberacao.Item.ProdutoId;
                                movEstoque.CorId = itemLiberacao.Item.CorId;
                                movEstoque.TamanhoId = itemLiberacao.Item.TamanhoId;
                                movEstoque.Observacao = "ENTRADA PELO PEDIDO " + pedido.Referencia + " LIBERAÇÃO FOI REMOVIDA";
                                movEstoque.DataMovimento = DateTime.Now;
                                listMovimentacaoEstoque.Add(movEstoque);
                            }
                            itemLiberacao.QtdEmpenhada = 0;
                            itemLiberacaoRepository.Delete(itemLiberacao.Id);
                            new ItemOrdemProducaoRepository().LimparVinculoPedidoOrdem(itemLiberacao.Id);

                            //excluir vínculo de pedido na conferencia
                            var conferenciaPedido = new PedidoVendaConferenciaRepository().GetByPedido(pedido.Id);
                            foreach (var conferencia in conferenciaPedido)
                            {
                                new PedidoVendaConferenciaItemRepository().DeleteByConferencia(conferencia.Id);
                                new PedidoVendaConferenciaRepository().Delete(conferencia.Id);
                            }
                        }
                        else //liberou item
                        {
                            if (itemLiberacao.ItemPedidoVendaId == 0)
                            {
                                itemLiberacao.Item = pedido.Itens.Where(x => x.ProdutoId == itemLiberacao.Item.ProdutoId && x.TamanhoId == itemLiberacao.Item.TamanhoId && x.CorId == itemLiberacao.Item.CorId && x.IdTemp == itemLiberacao.Item.IdTemp).FirstOrDefault();
                                itemLiberacao.ItemPedidoVendaId = itemLiberacao.Item.Id;
                            }
                            if (VestilloSession.ControleDeEstoqueAtivo == VestilloSession.ControleEstoque.SIM)
                            {
                                if ((VerificarEstoque(il, pedido, estoqueController) && tipoMov.AtualizaEstoque == 1) || 
                                    tipoMov.AtualizaEstoque != 1) // a verificação só é válida se o tipo de movimentação movimentar estoque
                                {
                                    itemLiberacao = MovimentaEstoque(pedido, itemLiberacaoRepository, tipoMov, listMovimentacaoEstoque, itemLiberacao);
                                }
                                else
                                {
                                    throw new Exception("A quantidade em estoque foi alterada");
                                }
                            }
                            else
                            {
                                itemLiberacao = MovimentaEstoque(pedido, itemLiberacaoRepository, tipoMov, listMovimentacaoEstoque, itemLiberacao);
                            }
                                                        
                        }
                    }

                    if (listMovimentacaoEstoque.Count > 0)
                    {
                        estoqueController.MovimentarEstoque(listMovimentacaoEstoque, false);
                    }

                    pedido.QtdEmpenhada = pedido.ItensLiberacao.Sum(il => il.QtdEmpenhada);
                    pedido.QtdLiberada = pedido.ItensLiberacao.Sum(il => il.Qtd - il.QtdFaturada);
                    decimal TotalEmpenhado = 0;
                    decimal TotalLiberado = 0;
                    var itens = pedido.Itens;
                    pedido.ItensLiberacao.ForEach(il =>
                    {
                        var item = itens.Find(i => i.Id == il.ItemPedidoVendaId);
                        TotalEmpenhado += il.QtdEmpenhada *  item.Preco;
                        TotalLiberado += (il.Qtd - il.QtdFaturada) * item.Preco;
                    });
                    pedido.ValorEmpenhadoTotal = TotalEmpenhado;
                    pedido.ValorLiberadoTotal = TotalLiberado;
                }

                if (pedido.Status == (int)enumStatusPedidoVenda.Bloqueado)
                {
                    var listMovimentacaoEstoque = new List<MovimentacaoEstoque>();
                    EstoqueController estoqueController = new EstoqueController();
                    var itensLiberacao = itemLiberacaoRepository.GetListByItensLiberacaoView(pedido.Id);
                    foreach (var il in itensLiberacao)
                    {
                        var itemLiberacao = il;

                        if (itemLiberacao.SemEmpenho == 0 || (itemLiberacao.QtdEmpenhada > 0 && itemLiberacao.SemEmpenho == 1))
                        {
                            var movEstoque = new MovimentacaoEstoque();
                            movEstoque.Empenho = true;
                            movEstoque.AlmoxarifadoId = itemLiberacao.AlmoxarifadoId;
                            movEstoque.Entrada = itemLiberacao.QtdEmpenhada;
                            movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                            movEstoque.ProdutoId = itemLiberacao.iditem;
                            movEstoque.CorId = itemLiberacao.idcor;
                            movEstoque.TamanhoId = itemLiberacao.idtamanho;
                            movEstoque.Observacao = "ENTRADA PELO PEDIDO " + pedido.Referencia + " FOI BLOQUEADO!";
                            movEstoque.DataMovimento = DateTime.Now;
                            listMovimentacaoEstoque.Add(movEstoque);
                        }

                        itemLiberacaoRepository.Delete(itemLiberacao.Id);
                        new ItemOrdemProducaoRepository().LimparVinculoPedidoOrdem(itemLiberacao.Id);
                    }
                    if (listMovimentacaoEstoque.Count > 0)
                    {
                        estoqueController.MovimentarEstoque(listMovimentacaoEstoque, false);
                    }
                }

                //Exlui os itens
                if (pedido.Itens != null && pedido.Itens.Count() > 0)
                {
                    var itens = pedido.Itens.Where(x => x.Qtd == 0).ToList();

                    foreach (ItemPedidoVenda item in itens)
                    {
                        itemPedidoVendaRepository.Delete(item.Id);
                    }

                    pedido.QtdPedida = pedido.Itens.Sum(i => i.Qtd);
                }

                //verifica se a referencia está duplicada
                if (VerificaReferenciaDuplicada(pedido))
                {
                    
                    var contador = new ContadorCodigoController().GetProximo("PedidoVenda");
                    pedido.GetType().GetProperty("Referencia").SetValue(pedido, contador, null);

                }

                base.Save(ref pedido);
                
                itemPedidoVendaRepository.CommitTransaction();
            }
            catch (Exception ex)
            {
                itemPedidoVendaRepository.RollbackTransaction();
                throw ex;
            }
        }

        private bool VerificaReferenciaDuplicada (PedidoVenda pedido)
        {
            List<PedidoVenda> pedidoVenda = new PedidoVendaRepository().GetByRef(pedido.Referencia).ToList();
            if(pedidoVenda != null && pedidoVenda.Count > 0)
            {
                if (pedidoVenda.Exists(p => pedido.Id != p.Id))
                    return true;
            }
            return false;
        }

        private static ItemLiberacaoPedidoVenda MovimentaEstoque(PedidoVenda pedido, ItemLiberacaoPedidoVendaRepository itemLiberacaoRepository, TipoMovimentacao tipoMov, List<MovimentacaoEstoque> listMovimentacaoEstoque, ItemLiberacaoPedidoVenda itemLiberacao)
        {
            var itemLiberacaoOld = itemLiberacaoRepository.GetById(itemLiberacao.Id);

            itemLiberacaoRepository.Save(ref itemLiberacao);

            if (tipoMov.AtualizaEstoque == 1 && itemLiberacao.SemEmpenho == 0)
            {
                decimal empenho = 0;
                if (itemLiberacaoOld != null)
                {
                    empenho = itemLiberacaoOld.QtdEmpenhada - itemLiberacao.QtdEmpenhada;
                }

                if (itemLiberacaoOld == null)
                {
                    var movEstoque = listMovimentacaoEstoque.Find(m => itemLiberacao.AlmoxarifadoId == m.AlmoxarifadoId && itemLiberacao.Item.ProdutoId == m.ProdutoId && itemLiberacao.Item.CorId == m.CorId && itemLiberacao.Item.TamanhoId == m.TamanhoId);
                    if (movEstoque != null)
                    {
                        movEstoque.Saida += itemLiberacao.QtdEmpenhada;
                    }
                    else
                    {
                        movEstoque = new MovimentacaoEstoque();
                        movEstoque.Empenho = true;
                        movEstoque.AlmoxarifadoId = itemLiberacao.AlmoxarifadoId;
                        movEstoque.Saida = itemLiberacao.QtdEmpenhada;
                        movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                        movEstoque.ProdutoId = itemLiberacao.Item.ProdutoId;
                        movEstoque.CorId = itemLiberacao.Item.CorId;
                        movEstoque.TamanhoId = itemLiberacao.Item.TamanhoId;
                        movEstoque.Observacao = "SAÍDA PELO PEDIDO " + pedido.Referencia + " FOI LIBERADO ";
                        movEstoque.DataMovimento = DateTime.Now;
                        listMovimentacaoEstoque.Add(movEstoque);
                    }
                }
                else if (empenho > 0)
                {
                    var movEstoque = listMovimentacaoEstoque.Find(m => itemLiberacao.AlmoxarifadoId == m.AlmoxarifadoId && itemLiberacao.Item.ProdutoId == m.ProdutoId && itemLiberacao.Item.CorId == m.CorId && itemLiberacao.Item.TamanhoId == m.TamanhoId);
                    if (movEstoque != null)
                    {
                        movEstoque.Saida += empenho;
                    }
                    else
                    {
                        movEstoque = new MovimentacaoEstoque();
                        movEstoque.Empenho = true;
                        movEstoque.AlmoxarifadoId = itemLiberacao.AlmoxarifadoId;
                        movEstoque.Saida = empenho;
                        movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                        movEstoque.ProdutoId = itemLiberacao.Item.ProdutoId;
                        movEstoque.CorId = itemLiberacao.Item.CorId;
                        movEstoque.TamanhoId = itemLiberacao.Item.TamanhoId;
                        movEstoque.Observacao = "PEDIDO: " + pedido.Referencia;
                        movEstoque.DataMovimento = DateTime.Now;
                        listMovimentacaoEstoque.Add(movEstoque);
                    }

                }
                else if (empenho < 0)
                {
                    var movEstoque = new MovimentacaoEstoque();
                    movEstoque.Empenho = true;
                    movEstoque.AlmoxarifadoId = itemLiberacao.AlmoxarifadoId;
                    movEstoque.Entrada = -empenho;
                    movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                    movEstoque.ProdutoId = itemLiberacao.Item.ProdutoId;
                    movEstoque.CorId = itemLiberacao.Item.CorId;
                    movEstoque.TamanhoId = itemLiberacao.Item.TamanhoId;
                    movEstoque.Observacao = "PEDIDO: " + pedido.Referencia;
                    movEstoque.DataMovimento = DateTime.Now;
                    listMovimentacaoEstoque.Add(movEstoque);
                }
            }
            return itemLiberacao;
        }

        private bool VerificarEstoque(ItemLiberacaoPedidoVenda itemLiberacao, PedidoVenda pedido, EstoqueController estoqueController)
        {
            if (itemLiberacao.Id == 0)
            {
                var item = pedido.Itens.Where(x => x.ProdutoId == itemLiberacao.Item.ProdutoId && x.TamanhoId == itemLiberacao.Item.TamanhoId && x.CorId == itemLiberacao.Item.CorId && x.IdTemp == itemLiberacao.Item.IdTemp).FirstOrDefault();
                int cor = (item.CorId != null) ? (int)item.CorId : 0;
                int tamanho = (item.TamanhoId != null) ? (int)item.TamanhoId : 0;
                var saldoAtual = estoqueController.GetSaldoAtualProduto(itemLiberacao.AlmoxarifadoId, item.ProdutoId, cor, tamanho);
                if (saldoAtual != null)
                {


                    if (saldoAtual.Saldo < 0)
                        saldoAtual.Saldo = 0;

                    var qtdNaoAtendida = saldoAtual.Saldo - itemLiberacao.Qtd;

                    if ((qtdNaoAtendida) >= 0)
                        itemLiberacao.Status = (int)enumStatusLiberacaoPedidoVenda.Atendido;
                    //else
                    //{
                    //    var qtdEmpenhada = itemLiberacao.Qtd + qtdNaoAtendida;
                    //    if (itemLiberacao.QtdEmpenhada != saldoAtual.Saldo && itemLiberacao.QtdNaoAtendida != -qtdNaoAtendida)
                    //    {
                    //        return false;
                    //    }
                    //}

                    var itens = pedido.ItensLiberacao.FindAll(p => p.ItemPedidoVendaId != itemLiberacao.ItemPedidoVendaId && p.Item.ProdutoId == item.ProdutoId && p.Item.CorId == item.CorId && p.Item.TamanhoId == item.TamanhoId);

                    if (pedido.ItensLiberacao != null && itens != null && itens.Count > 0)
                    {
                        //var itens = pedido.ItensLiberacao.FindAll(p => p.ItemPedidoVendaId != itemLiberacao.ItemPedidoVendaId && p.Item.ProdutoId == item.ProdutoId && p.Item.CorId == item.CorId && p.Item.TamanhoId == item.TamanhoId);
                        //if (itens != null)
                        //{
                        //    itens.ForEach(i =>
                        //    {
                        //        saldoAtual.Saldo = saldoAtual.Saldo - i.Qtd;
                        //    });
                        //}
                        var itemLib = new ItemLiberacaoPedidoVenda();
                        pedido.ItensLiberacao.ForEach(il =>
                        {
                            if (il.Item.ProdutoId == item.ProdutoId && il.Item.CorId == item.CorId && il.Item.TamanhoId == item.TamanhoId)
                            {
                                itemLib.QtdEmpenhada += il.QtdEmpenhada;
                                itemLib.QtdNaoAtendida += il.QtdNaoAtendida;
                                itemLib.Qtd += il.Qtd;
                            }
                        });
                        var qtdNao = saldoAtual.Saldo - itemLib.Qtd;
                        //if (itemLiberacao.QtdNaoAtendida == 0 && qtdNao != 0)
                        //{
                        //    itemLiberacao.QtdNaoAtendida = -qtdNao;
                        //    itemLiberacao.QtdEmpenhada = qtdNao;
                        //}
                    }

                    return true;
                }
                else
                {
                    if (itemLiberacao.QtdEmpenhada != 0 && itemLiberacao.QtdNaoAtendida != itemLiberacao.Qtd)
                        return false;
                    else
                        return true;
                }
            }
            return true;
        }
        

        public override void Delete(int id)
        {
            bool openTransaction = false;

            var itemPedidoVendaRepository = new ItemPedidoVendaRepository();
            var contasReceberRepository = new ContasReceberRepository();
            var itemLiberacaoRepository = new ItemLiberacaoPedidoVendaRepository();
            var tipoMovimentacaoRepository = new TipoMovimentacaoRepository();
            TipoMovimentacao tipoMov = new TipoMovimentacao(); 

            try
            {
                openTransaction = _repository.BeginTransaction();

                IEnumerable<ItemPedidoVendaView> itens = itemPedidoVendaRepository.GetByPedido(id);

                var firstItem = itens.FirstOrDefault();

                tipoMov = tipoMovimentacaoRepository.GetById(firstItem.TipoMovimentacaoId);

                var Ped = new PedidoVendaRepository();
                var DPed = Ped.GetById(id);
               

                //==============================================================================================
                // Exclui as liberações do pedido
                //==============================================================================================
                IEnumerable<ItemLiberacaoPedidoVendaView> itensLiberacoes = itemLiberacaoRepository.GetListByItensLiberacaoView(id);
                EstoqueController estoqueController = new EstoqueController();
                var listMovimentacaoEstoque = new List<MovimentacaoEstoque>();
                foreach (ItemLiberacaoPedidoVendaView itemLiberacao in itensLiberacoes)
                {
                    if (itemLiberacao.QtdFaturada > 0)
                        throw new Exception("Pedido de venda não excluído pois existem itens faturados.");

                    //var itemLiberacaoOld = itemLiberacaoRepository.GetById(itemLiberacao.Id);
                    if (tipoMov.AtualizaEstoque == 1 && (itemLiberacao.SemEmpenho == 0 || (itemLiberacao.QtdEmpenhada > 0 && itemLiberacao.SemEmpenho == 1)) )
                    {
                        var movEstoque = new MovimentacaoEstoque();
                        movEstoque.Empenho = true;
                        movEstoque.AlmoxarifadoId = itemLiberacao.AlmoxarifadoId;
                        movEstoque.Entrada = itemLiberacao.QtdEmpenhada;
                        movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                        movEstoque.ProdutoId = itemLiberacao.iditem;
                        movEstoque.CorId = itemLiberacao.idcor;
                        movEstoque.TamanhoId = itemLiberacao.idtamanho;
                        movEstoque.Observacao = "ENTRADA PELO PEDIDO: " + DPed.Referencia + " FOI EXCLUÍDO POSSUINDO LIBERAÇÃO";
                        movEstoque.DataMovimento = DateTime.Now;
                        listMovimentacaoEstoque.Add(movEstoque);
                    }
                    itemLiberacaoRepository.Delete(itemLiberacao.Id);
                    new ItemOrdemProducaoRepository().LimparVinculoPedidoOrdem(itemLiberacao.Id);
                }
                if (listMovimentacaoEstoque.Count > 0)
                {
                    estoqueController.MovimentarEstoque(listMovimentacaoEstoque, false);
                }
                //==============================================================================================

                //==============================================================================================
                // Exclui parcelas do pedido
                //==============================================================================================
                IEnumerable<ContasReceberView> parcelas = contasReceberRepository.GetListaPorCampoEValor("IdPedidoVenda", id.ToString());

                foreach (ContasReceberView parcela in parcelas)
                {
                    if (parcela.ValorPago > 0)
                        throw new Exception("Pedido de venda não excluído pois existem parcelas baixadas.");

                    contasReceberRepository.Delete(parcela.Id);
                }
                //==============================================================================================

                //==============================================================================================
                // Exclui os itens do pedido
                //==============================================================================================
                foreach (ItemPedidoVendaView item in itens)
                {
                    itemPedidoVendaRepository.Delete(item.Id);
                }
                //==============================================================================================

                //==============================================================================================
                // Exclui os registros na conferência
                //==============================================================================================
                var conferenciaPedido = new PedidoVendaConferenciaRepository().GetByPedido(id);
                foreach (var conferencia in conferenciaPedido)
                {
                    new PedidoVendaConferenciaItemRepository().DeleteByConferencia(conferencia.Id);
                    new PedidoVendaConferenciaRepository().Delete(conferencia.Id);
                }
                //==============================================================================================

                base.Delete(id);

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

        public void FinalizarPedidoVenda(int pedidoVendaId)
        {
            var repository = new PedidoVendaRepository();
            var itensRepository = new ItemPedidoVendaRepository();
            var listMovimentacaoEstoque = new List<MovimentacaoEstoque>();

            try
            {
                repository.BeginTransaction();

                var itemLiberacaoPedidoVendaController = new ItemLiberacaoPedidoVendaController();
                var pedido = repository.GetById(pedidoVendaId);
                bool estornar = (pedido.Status == (int)enumStatusPedidoVenda.Finalizado);

                if (pedido.Status == (int)enumStatusPedidoVenda.Faturado_Total)
                {
                    throw new Exception("O Pedido não pode ser Finalizado pois o mesmo já foi faturado total!");
                }
                else if ((pedido.Status == (int)enumStatusPedidoVenda.Finalizado) && !estornar)
                {
                    throw new Exception("Pedido de venda já finalizado.");
                }

              
                bool itemFaturado = false;
                List<ItemLiberacaoPedidoVenda> liberacoes = itemLiberacaoPedidoVendaController.GetByPedidoVenda(pedidoVendaId);


                //testar aqui se possui tratamento de estoque

                var itens = itensRepository.GetByPedido(pedidoVendaId).ToList();
                var rpTipoMov = new TipoMovimentacaoRepository();
                var tipoMov = rpTipoMov.GetById(itens[0].TipoMovimentacaoId);
                var atualizaEstoque = (tipoMov.AtualizaEstoque == 1) ? true : false;

                
                if (liberacoes != null && liberacoes.Count > 0)
                {
                    foreach (var itemLiberacao in liberacoes)
                    {
                        decimal qtd = (itemLiberacao.Qtd - itemLiberacao.QtdFaturada);

                        if (itemLiberacao.QtdFaturada > 0)
                            itemFaturado = true;

                        if (qtd > 0)
                        {
                            var itemPedidoVenda = itensRepository.GetById(itemLiberacao.ItemPedidoVendaId);

                            var movEstoque = new MovimentacaoEstoque();
                            movEstoque.Empenho = true;
                            movEstoque.AlmoxarifadoId = itemLiberacao.AlmoxarifadoId;

                            //if (estornar)
                            //    movEstoque.Saida = qtd;
                            //else
                            if (!estornar)
                                movEstoque.Entrada = itemLiberacao.QtdEmpenhada;

                            movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                            movEstoque.ProdutoId = itemPedidoVenda.ProdutoId;
                            movEstoque.CorId = itemPedidoVenda.CorId;
                            movEstoque.TamanhoId = itemPedidoVenda.TamanhoId;
                            movEstoque.Observacao = "ENTRADA PELO PEDIDO : " + pedido.Referencia + " FOI FINALIZADO";

                            if (qtd == itemLiberacao.Qtd)
                            {
                                itemLiberacaoPedidoVendaController.Delete(itemLiberacao.Id);
                                new ItemOrdemProducaoRepository().LimparVinculoPedidoOrdem(itemLiberacao.Id);
                            }
                            else
                            {
                                var itemSave = new ItemLiberacaoPedidoVenda();
                                itemSave.QtdEmpenhada = 0;
                                itemSave.Qtd = itemLiberacao.QtdFaturada;
                                itemSave.QtdFaturada = itemLiberacao.QtdFaturada;
                                itemSave.Status = (int)enumStatusLiberacaoPedidoVenda.Atendido;
                                itemSave.ItemPedidoVendaId = itemLiberacao.ItemPedidoVendaId;
                                itemSave.LiberacaoId = itemLiberacao.LiberacaoId;
                                itemSave.Id = itemLiberacao.Id;
                                itemSave.Data = itemLiberacao.Data;
                                itemLiberacaoPedidoVendaController.Save(ref itemSave);
                            }

                            if(atualizaEstoque && (itemLiberacao.SemEmpenho == 0 ||( itemLiberacao.QtdEmpenhada > 0 && itemLiberacao.SemEmpenho == 1)) )
                            {
                                listMovimentacaoEstoque.Add(movEstoque);
                            }
                            
                        }

                    }
                }

                if (atualizaEstoque)
                {
                    if (listMovimentacaoEstoque.Count > 0)
                    {
                        EstoqueController estoqueController = new EstoqueController();
                        estoqueController.MovimentarEstoque(listMovimentacaoEstoque, false);
                    }
                }
                

                enumStatusPedidoVenda status = enumStatusPedidoVenda.Finalizado;

                if (estornar)
                {
                    if (itemFaturado)
                        status = enumStatusPedidoVenda.Faturado_Parcial;
                    else
                        status = enumStatusPedidoVenda.Incluido;
                }

                pedido.QtdEmpenhada = 0;
                pedido.QtdLiberada = 0;
                pedido.ValorEmpenhadoTotal = 0;
                pedido.ValorLiberadoTotal = 0;

                repository.Save(ref pedido);
                UpdateStatus(pedidoVendaId, status);

                repository.CommitTransaction();
            }  
            catch (Exception ex)
            {
                repository.RollbackTransaction();
                throw ex;
            }
        }

        public void UpdateStatus(int pedidoVendaId, enumStatusPedidoVenda status)
        {
            using (var repository = new PedidoVendaRepository())
            {
                repository.UpdateStatus(pedidoVendaId, status);
            }   
        }

        public List<PedidoVendaLiberacaoView> GetPedidoByItem(string referencia)
        {
            using (var repository = new PedidoVendaRepository())
            {
                return repository.GetPedidoByItem(referencia);
            }
        }
    }
}
