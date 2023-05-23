using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models.Views;
using Vestillo.Business.Service;

namespace Vestillo.Business.Controllers
{
    public class PacoteProducaoController : GenericController<PacoteProducao, PacoteProducaoRepository>
    {

        public List<PacoteProducaoView> GetByIdView(List<int> id)
        {
            using (var repository = new PacoteProducaoRepository())
            {
                return repository.GetByViewId(id);
            }
        }

        public IEnumerable<PacoteProducaoView> GetPacotesBrowse(FiltroRelatorioPacote filtro)
        {
            using (var repository = new PacoteProducaoRepository())
            {
                return repository.GetPacotesBrowse(filtro);
            }
        }

        public IEnumerable<ControlePacoteView> GetControlePacotesRelatorio(FiltroControlePacote filtro)
        {
            using (var repository = new PacoteProducaoRepository())
            {
                return repository.GetControlePacotesRelatorio(filtro);
            }
        }

        public PacoteProducaoView GetByIdView(int id)
        {
            using (var repository = new PacoteProducaoRepository())
            {
                return repository.GetByIdView(id);
            }
        }

        public List<PacoteProducaoView> GetByOrdemIdView(int ordemId)
        {
            using (var repository = new PacoteProducaoRepository())
            {
                return repository.GetByOrdemIdView(ordemId);
            }
        }

        public List<PacoteProducaoView> GetByView(bool CupomEletronico = false)
        {
            using (var repository = new PacoteProducaoRepository())
            {
                return repository.GetByView(CupomEletronico);
            }
        }

        public PacoteProducaoView GetByViewReferencia(string referencia)
        {
            using (var repository = new PacoteProducaoRepository())
            {
                return repository.GetByViewReferencia(referencia);
            }
        }

        public PacoteProducaoView GetByViewReferenciaJunior(string referencia)
        {
            using (var repository = new PacoteProducaoRepository())
            {
                return repository.GetByViewReferenciaJunior(referencia);
            }
        }

        public IEnumerable<PacoteProducaoView> GetByListViewReferencia(string referencia, bool CupomEletronico = false)
        {
            using (var repository = new PacoteProducaoRepository())
            {
                return repository.GetByListViewReferencia(referencia, CupomEletronico);
            }
        }

        public IEnumerable<PacoteProducaoView> GetPacotesRelatorio(FiltroRelatorioPacote filtro)
        {
            using (var repository = new PacoteProducaoRepository())
            {
                return repository.GetPacotesRelatorio(filtro);
            }
        }

        public IEnumerable<PacoteProducaoView> GetByFiltroBalanceamento(FiltroRelatorioPacote filtro)
        {
            using (var repository = new PacoteProducaoRepository())
            {
                return repository.GetByFiltroBalanceamento(filtro);
            }
        }

        public IEnumerable<PacoteFuncionario> GetPacotesFuncionarioRelatorio(List<int> pacotes, List<int> funcionarios, string DaData, string AteData)
        {
            using (var repository = new PacoteProducaoRepository())
            {
                return repository.GetPacotesFuncionarioRelatorio(pacotes, funcionarios, DaData, AteData);
            }
        }

        public IEnumerable<PacoteFaccao> GetPacotesFaccaoRelatorio(FiltroPacoteFaccao filtro)
        {
            using (var repository = new PacoteProducaoRepository())
            {
                return repository.GetPacotesFaccaoRelatorio(filtro);
            }
        }

        public IEnumerable<PacoteFaccao> GetPacotesFaccaoFinalizados(FiltroPacoteFaccao filtro)
        {
            using (var repository = new PacoteProducaoRepository())
            {
                return repository.GetPacotesFaccaoFinalizados(filtro);
            }
        }

        public void Save(ref PacoteProducaoView pacote)
        {
           //var pacoteRepository = new PacoteProducaoRepository();
            var itensOrdemRepository = new ItemOrdemProducaoRepository();
            var ordemRepository = new OrdemProducaoRepository();
            var estoqueRepository = new EstoqueRepository();
            var pedidoRepository = new ItemLiberacaoPedidoVendaRepository();
            EstoqueController estoqueController = new EstoqueController();
            var pacoteExistente = new PacoteProducaoRepository().GetById(pacote.Id);

            using (PacoteProducaoRepository pacoteRepository = new PacoteProducaoRepository())
            {
                try
                {

                    pacoteRepository.BeginTransaction();
                    PacoteProducao pacoteProd = new PacoteProducao();
                    
                    pacoteProd = pacote;
                    base.Save(ref pacoteProd);
                    
                    if (pacote.ItemOrdemProducaoId > 0)
                    {
                        var ultimaMovimentacao = estoqueController.GetUltimaMovimentacaoByPacote(pacote.Id);
                        if (pacote.Status == (int)enumStatusPacotesProducao.Finalizado && pacoteExistente.Status != (int)enumStatusPacotesProducao.Finalizado
                            && (ultimaMovimentacao == null || ultimaMovimentacao.CancelamentoPacote))
                        {
                            var listMovimentacaoEstoque = new List<MovimentacaoEstoque>();
                            var itensOrdem = itensOrdemRepository.GetById(pacote.ItemOrdemProducaoId);
                            if (itensOrdem.PedidoVendaId != null && itensOrdem.PedidoVendaId > 0)
                            {
                                var item = pedidoRepository.GetById(itensOrdem.SeqLiberacaoPedido.Value);

                                if (!Convert.ToBoolean(item.SemEmpenho))
                                {
                                    if (VestilloSession.FinalizaPacoteFaccao)
                                        item.QtdEmpenhada += pacote.QtdProduzida - pacote.QtdDefeito - pacote.QuantidadeAlternativa;
                                    else
                                        item.QtdEmpenhada += pacote.Quantidade - pacote.QtdDefeito - pacote.QuantidadeAlternativa;
                                }                                

                                item.QtdNaoAtendida += (pacote.QtdDefeito + pacote.QuantidadeAlternativa);                                                              

                                if (item.Qtd == item.QtdEmpenhada || (item.QtdNaoAtendida == 0 && item.SemEmpenho == 1))
                                {
                                    item.Status = (int)enumStatusLiberacaoPedidoVenda.Atendido;
                                }
                                pedidoRepository.Save(ref item);

                                PedidoDeVendaAtualizacao(item);

                                if ( ( (pacote.Quantidade - pacote.QtdDefeito - pacote.QuantidadeAlternativa) > 0 && !VestilloSession.FinalizaPacoteFaccao)
                                    || ( (pacote.QtdProduzida - pacote.QtdDefeito - pacote.QuantidadeAlternativa) > 0 && VestilloSession.FinalizaPacoteFaccao) )
                                {
                                    var movEstoque = new MovimentacaoEstoque();
                                    if (!Convert.ToBoolean(item.SemEmpenho))
                                    {
                                        if (VestilloSession.FinalizaPacoteFaccao)
                                            movEstoque.Saida = (pacote.QtdProduzida - pacote.QtdDefeito - pacote.QuantidadeAlternativa);
                                        else
                                            movEstoque.Saida = (pacote.Quantidade - pacote.QtdDefeito - pacote.QuantidadeAlternativa);
                                    
                                        movEstoque.Empenho = true;
                                        movEstoque.SoEmpenho = true;
                                    }
                                    else
                                    {
                                        if (VestilloSession.FinalizaPacoteFaccao)
                                            movEstoque.Entrada = (pacote.QtdProduzida - pacote.QtdDefeito - pacote.QuantidadeAlternativa);
                                        else
                                            movEstoque.Entrada = (pacote.Quantidade - pacote.QtdDefeito - pacote.QuantidadeAlternativa);
                                    }
                                    
                                    movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                                    movEstoque.ProdutoId = pacote.ProdutoId;
                                    movEstoque.CorId = pacote.CorId;
                                    movEstoque.TamanhoId = pacote.TamanhoId;
                                    movEstoque.AlmoxarifadoId = pacote.AlmoxarifadoId;
                                    movEstoque.Observacao = "Pacote de Produção: " + pacote.Referencia;
                                    movEstoque.DataMovimento = DateTime.Now;
                                    movEstoque.IdPacote = pacote.Id;
                                    listMovimentacaoEstoque.Add(movEstoque);

                                }
                                if (pacote.QuantidadeAlternativa > 0)
                                {
                                    var movEstoque = new MovimentacaoEstoque();
                                    movEstoque.Entrada = pacote.QuantidadeAlternativa;
                                    movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                                    movEstoque.ProdutoId = pacote.ProdutoId;
                                    movEstoque.CorId = pacote.CorId;
                                    movEstoque.TamanhoId = pacote.TamanhoId;
                                    movEstoque.AlmoxarifadoId = pacote.AlmoxarifadoAlternativoId;
                                    movEstoque.Observacao = "Pacote de Produção: " + pacote.Referencia;
                                    movEstoque.DataMovimento = DateTime.Now;
                                    movEstoque.IdPacote = pacote.Id;
                                    listMovimentacaoEstoque.Add(movEstoque);
                                }
                            }
                            else
                            {
                                var movEstoque = new MovimentacaoEstoque();
                                //movEstoque.Empenho = true;

                                if(VestilloSession.FinalizaPacoteFaccao)
                                    movEstoque.Entrada = pacote.QtdProduzida - pacote.QtdDefeito - pacote.QuantidadeAlternativa;
                                else
                                    movEstoque.Entrada = pacote.Quantidade - pacote.QtdDefeito - pacote.QuantidadeAlternativa;

                                movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                                movEstoque.ProdutoId = pacote.ProdutoId;

                                if (pacote.CorAlteradaId > 0)
                                {
                                    movEstoque.CorId = pacote.CorAlteradaId;
                                }
                                else
                                {
                                    movEstoque.CorId = pacote.CorId;
                                }

                                if (pacote.TamanhoAlteradoId > 0)
                                {
                                    movEstoque.TamanhoId = pacote.TamanhoAlteradoId;
                                }
                                else
                                {
                                    movEstoque.TamanhoId = pacote.TamanhoId;
                                }

                                movEstoque.AlmoxarifadoId = pacote.AlmoxarifadoId;
                                movEstoque.Observacao = "ENTRADA PELO PACOTE: " + pacote.Referencia;
                                movEstoque.DataMovimento = DateTime.Now;
                                movEstoque.IdPacote = pacote.Id;
                                listMovimentacaoEstoque.Add(movEstoque);

                                if (pacote.QuantidadeAlternativa > 0)
                                {
                                    movEstoque = new MovimentacaoEstoque();
                                    movEstoque.Entrada = pacote.QuantidadeAlternativa;
                                    movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                                    movEstoque.ProdutoId = pacote.ProdutoId;

                                    if (pacote.CorAlteradaId > 0)
                                    {
                                        movEstoque.CorId = pacote.CorAlteradaId;
                                    }
                                    else
                                    {
                                        movEstoque.CorId = pacote.CorId;
                                    }

                                    if (pacote.TamanhoAlteradoId > 0)
                                    {
                                        movEstoque.TamanhoId = pacote.TamanhoAlteradoId;
                                    }
                                    else
                                    {
                                        movEstoque.TamanhoId = pacote.TamanhoId;
                                    }

                                    movEstoque.AlmoxarifadoId = pacote.AlmoxarifadoAlternativoId;
                                    movEstoque.Observacao = "ENTRADA PELO PACOTE: " + pacote.Referencia;
                                    movEstoque.DataMovimento = DateTime.Now;
                                    movEstoque.IdPacote = pacote.Id;
                                    listMovimentacaoEstoque.Add(movEstoque);
                                }
                            }

                            if (listMovimentacaoEstoque.Count > 0)
                            {
                                
                                estoqueController.MovimentarEstoque(listMovimentacaoEstoque);
                            }

                            itensOrdem.QuantidadeProduzida += pacote.Quantidade;
                            itensOrdemRepository.Save(ref itensOrdem);

                            var itens = itensOrdemRepository.GetByOrdem(itensOrdem.OrdemProducaoId).ToList();
                            var ordem = ordemRepository.GetById(itensOrdem.OrdemProducaoId);

                            if (itens.Exists(i => i.QuantidadeProduzida > 0))
                            {
                                if (!itens.Exists(i => i.Quantidade > i.QuantidadeProduzida))
                                {
                                    ordem.Status = (int)enumStatusOrdemProducao.Atendido;
                                }
                                else
                                {
                                    ordem.Status = (int)enumStatusOrdemProducao.Atendido_Parcial;
                                }
                            }

                            ordemRepository.Save(ref ordem);
                        }
                        else if (pacoteExistente.Status == (int)enumStatusPacotesProducao.Finalizado && pacote.Status != (int)enumStatusPacotesProducao.Finalizado 
                            && (ultimaMovimentacao == null || !ultimaMovimentacao.CancelamentoPacote))
                        {
                            var listMovimentacaoEstoque = new List<MovimentacaoEstoque>();
                            var itensOrdem = itensOrdemRepository.GetById(pacote.ItemOrdemProducaoId);

                            itensOrdem.QuantidadeProduzida -= pacoteExistente.Quantidade - pacoteExistente.QtdDefeito - pacoteExistente.QuantidadeAlternativa;

                            itensOrdemRepository.Save(ref itensOrdem);

                            if (itensOrdem.PedidoVendaId != null && itensOrdem.PedidoVendaId > 0)
                            {
                                var item = pedidoRepository.GetById(itensOrdem.SeqLiberacaoPedido.Value);

                                if (!Convert.ToBoolean(item.SemEmpenho))
                                {
                                    if ((item.QtdEmpenhada < (pacoteExistente.Quantidade - pacoteExistente.QtdDefeito - pacoteExistente.QuantidadeAlternativa) && !VestilloSession.FinalizaPacoteFaccao)
                                    || (item.QtdEmpenhada < (pacoteExistente.QtdProduzida - pacoteExistente.QtdDefeito - pacoteExistente.QuantidadeAlternativa) && VestilloSession.FinalizaPacoteFaccao))
                                    {
                                        throw new Exception("Item do pedido já faturado!"); //quando a qtdEmpenhada é diminuída pelo faturamento
                                    }

                                    if (VestilloSession.FinalizaPacoteFaccao)
                                        item.QtdEmpenhada -= pacoteExistente.QtdProduzida - pacoteExistente.QtdDefeito - pacoteExistente.QuantidadeAlternativa;
                                    else
                                        item.QtdEmpenhada -= pacoteExistente.Quantidade - pacoteExistente.QtdDefeito - pacoteExistente.QuantidadeAlternativa;
                                }                                    

                                item.QtdNaoAtendida -= (pacoteExistente.QtdDefeito + pacoteExistente.QuantidadeAlternativa);

                                if (VestilloSession.UsaConferencia)
                                {
                                    if (item.QtdConferencia > 0)
                                    {
                                        var Pedido = new PedidoVendaRepository();
                                        var RefPedido = Pedido.GetById(Convert.ToInt32(itensOrdem.PedidoVendaId));

                                        throw new Exception("A conferência do item no pedido " + RefPedido.Referencia + " deve ser desfeita para realizar o cancelamento!");
                                    }
                                }

                                item.Status = (int)enumStatusLiberacaoPedidoVenda.Producao;

                                /*if (item.Qtd == item.QtdEmpenhada)
                                {
                                    item.Status = (int)enumStatusLiberacaoPedidoVenda.Atendido;
                                }
                                else if (item.QtdEmpenhada > 0 && item.QtdNaoAtendida > 0)
                                {
                                    item.Status = (int)enumStatusLiberacaoPedidoVenda.Atendido_Parcial;
                                }
                                else if (item.QtdEmpenhada <= 0 && item.QtdNaoAtendida > 0)
                                {
                                    item.Status = (int)enumStatusLiberacaoPedidoVenda.Sem_Estoque;
                                } */

                                pedidoRepository.Save(ref item);
                                PedidoDeVendaAtualizacao(item);
                                if ( ((pacote.Quantidade - pacote.QtdDefeito - pacote.QuantidadeAlternativa) > 0 && !VestilloSession.FinalizaPacoteFaccao)
                                    || ((pacote.QtdProduzida - pacote.QtdDefeito - pacote.QuantidadeAlternativa) > 0 && VestilloSession.FinalizaPacoteFaccao) )
                                {
                                    var movEstoque = new MovimentacaoEstoque();

                                    if (!Convert.ToBoolean(item.SemEmpenho))
                                    {
                                        if (VestilloSession.FinalizaPacoteFaccao)
                                            movEstoque.Entrada = (pacote.QtdProduzida - pacote.QtdDefeito - pacote.QuantidadeAlternativa);
                                        else
                                            movEstoque.Entrada = (pacote.Quantidade - pacote.QtdDefeito - pacote.QuantidadeAlternativa);
                                                                            
                                        movEstoque.Empenho = true;
                                        movEstoque.SoEmpenho = true;
                                    }
                                    else
                                    {
                                        if (VestilloSession.FinalizaPacoteFaccao)
                                            movEstoque.Saida = (pacote.QtdProduzida - pacote.QtdDefeito - pacote.QuantidadeAlternativa);
                                        else
                                            movEstoque.Saida = (pacote.Quantidade - pacote.QtdDefeito - pacote.QuantidadeAlternativa);
                                    }

                                    movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                                    movEstoque.ProdutoId = pacote.ProdutoId;
                                    movEstoque.CorId = pacote.CorId;
                                    movEstoque.TamanhoId = pacote.TamanhoId;
                                    movEstoque.AlmoxarifadoId = pacote.AlmoxarifadoId;
                                    movEstoque.Observacao = "Pacote de Produção: " + pacote.Referencia;
                                    movEstoque.DataMovimento = DateTime.Now;
                                    movEstoque.IdPacote = pacote.Id;
                                    movEstoque.CancelamentoPacote = true;
                                    listMovimentacaoEstoque.Add(movEstoque);

                                }
                                if (pacoteExistente.QuantidadeAlternativa > 0)
                                {
                                    var movEstoque = new MovimentacaoEstoque();
                                    movEstoque.Saida = pacoteExistente.QuantidadeAlternativa;
                                    movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                                    movEstoque.ProdutoId = pacoteExistente.ProdutoId;
                                    movEstoque.CorId = pacoteExistente.CorId;
                                    movEstoque.TamanhoId = pacoteExistente.TamanhoId;
                                    movEstoque.AlmoxarifadoId = pacoteExistente.AlmoxarifadoAlternativoId;
                                    movEstoque.Observacao = "Pacote de Produção: " + pacoteExistente.Referencia;
                                    movEstoque.DataMovimento = DateTime.Now;
                                    movEstoque.IdPacote = pacote.Id;
                                    movEstoque.CancelamentoPacote = true;
                                    listMovimentacaoEstoque.Add(movEstoque);
                                }
                            }
                            else
                            {
                                var movEstoque = new MovimentacaoEstoque();
                                //movEstoque.Empenho = true;

                                if(VestilloSession.FinalizaPacoteFaccao)
                                    movEstoque.Saida = pacoteExistente.QtdProduzida - pacoteExistente.QtdDefeito - pacoteExistente.QuantidadeAlternativa;
                                else
                                    movEstoque.Saida = pacoteExistente.Quantidade - pacoteExistente.QtdDefeito - pacoteExistente.QuantidadeAlternativa;

                                movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                                movEstoque.ProdutoId = pacoteExistente.ProdutoId;

                                if (pacoteExistente.CorAlteradaId > 0)
                                {
                                    movEstoque.CorId = pacoteExistente.CorAlteradaId;
                                }
                                else
                                {
                                    movEstoque.CorId = pacoteExistente.CorId;
                                }

                                if (pacoteExistente.TamanhoAlteradoId > 0)
                                {
                                    movEstoque.TamanhoId = pacoteExistente.TamanhoAlteradoId;
                                }
                                else
                                {
                                    movEstoque.TamanhoId = pacoteExistente.TamanhoId;
                                }

                                movEstoque.AlmoxarifadoId = pacote.AlmoxarifadoId;
                                movEstoque.Observacao = "SAÍDA PELO PACOTE: " + pacoteExistente.Referencia + " Cancelamento de finalização";
                                movEstoque.DataMovimento = DateTime.Now;
                                movEstoque.IdPacote = pacote.Id;
                                movEstoque.CancelamentoPacote = true;
                                listMovimentacaoEstoque.Add(movEstoque);

                                if (pacoteExistente.QuantidadeAlternativa > 0)
                                {
                                    movEstoque = new MovimentacaoEstoque();
                                    movEstoque.Saida = pacoteExistente.QuantidadeAlternativa;
                                    movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                                    movEstoque.ProdutoId = pacoteExistente.ProdutoId;

                                    if (pacoteExistente.CorAlteradaId > 0)
                                    {
                                        movEstoque.CorId = pacoteExistente.CorAlteradaId;
                                    }
                                    else
                                    {
                                        movEstoque.CorId = pacoteExistente.CorId;
                                    }

                                    if (pacoteExistente.TamanhoAlteradoId > 0)
                                    {
                                        movEstoque.TamanhoId = pacoteExistente.TamanhoAlteradoId;
                                    }
                                    else
                                    {
                                        movEstoque.TamanhoId = pacoteExistente.TamanhoId;
                                    }
                                    movEstoque.AlmoxarifadoId = pacoteExistente.AlmoxarifadoAlternativoId;
                                    movEstoque.Observacao = "Pacote de Produção: " + pacoteExistente.Referencia;
                                    movEstoque.DataMovimento = DateTime.Now;
                                    movEstoque.IdPacote = pacote.Id;
                                    movEstoque.CancelamentoPacote = true;
                                    listMovimentacaoEstoque.Add(movEstoque);
                                }
                            }

                            if (listMovimentacaoEstoque.Count > 0)
                            {
                                estoqueController.MovimentarEstoque(listMovimentacaoEstoque);
                            }
                                                        
                            var itens = itensOrdemRepository.GetByOrdem(itensOrdem.OrdemProducaoId).ToList();
                            var ordem = ordemRepository.GetById(itensOrdem.OrdemProducaoId);

                            if(ordem.Status != (int)enumStatusOrdemProducao.Finalizado)
                            {
                                if (itens.Exists(i => i.QuantidadeProduzida > 0))
                                {
                                    if (!itens.Exists(i => i.Quantidade > i.QuantidadeProduzida))
                                    {
                                        ordem.Status = (int)enumStatusOrdemProducao.Atendido;
                                    }
                                    else
                                    {
                                        ordem.Status = (int)enumStatusOrdemProducao.Atendido_Parcial;
                                    }
                                }
                                else
                                {
                                    ordem.Status = (int)enumStatusOrdemProducao.Em_producao;
                                }
                                ordem.Finalizacao = null;
                                ordemRepository.Save(ref ordem);
                            }                             
                        }
                    }

                    pacoteRepository.CommitTransaction();

                    //tratar mudança de status Pedido de venda com conferência




                }
                catch (Exception ex)
                {
                    if (pacoteExistente != null)
                    {
                        pacoteRepository.Save(ref pacoteExistente);
                    }
                    pacoteRepository.RollbackTransaction();
                    throw ex;
                }
            }
        }

        private static void PedidoDeVendaAtualizacao(ItemLiberacaoPedidoVenda item)
        {
            var serviceItemPedido = new ItemPedidoVendaController();
            var servicePedido = new PedidoVendaController();
            var itemPedido = serviceItemPedido.GetById(item.ItemPedidoVendaId);
            
            var pedido = servicePedido.GetByIdAtualizacao(itemPedido.PedidoVendaId);
            servicePedido.Save(ref pedido);
        }

        public override void Delete(int id)
        {
            var pacoteProducaoRepository = new PacoteProducaoRepository();
            var itemOrdemProducaoRepository = new ItemOrdemProducaoRepository();
            var ordemProducaoRepository = new OrdemProducaoRepository();
            // grupoOperacoesRepository = new GrupoOperacoesRepository();

            try
            {
                itemOrdemProducaoRepository.BeginTransaction();

                PacoteProducao pacote = pacoteProducaoRepository.GetById(id);
                if (pacote.ItemOrdemProducaoId > 0)
                {
                    ItemOrdemProducao itemOrdem = itemOrdemProducaoRepository.GetById(pacote.ItemOrdemProducaoId);
                    itemOrdem.QuantidadeAtendida -= pacote.Quantidade;
                    if (itemOrdem.QuantidadeAtendida < 0)
                        itemOrdem.QuantidadeAtendida = 0;
                    if (pacote.Status == (int)enumStatusPacotesProducao.Finalizado)
                    {
                        itemOrdem.QuantidadeProduzida -= pacote.Quantidade;
                    }
                    if (itemOrdem.QuantidadeAtendida < itemOrdem.Quantidade)
                    {
                        itemOrdem.Status = 1;
                    }
                    OrdemProducao op = ordemProducaoRepository.GetById(itemOrdem.OrdemProducaoId);
                    List<PacoteProducaoView> pacotes = pacoteProducaoRepository.GetByOrdemIdView(itemOrdem.OrdemProducaoId);
                    pacotes.RemoveAll(p => p.Id == pacote.Id);

                    itemOrdemProducaoRepository.Save(ref itemOrdem);

                    if (pacotes.Count <= 0)
                    {
                        op.Status = (int)enumStatusOrdemProducao.Em_Corte;
                        op.Corte = null;
                        ordemProducaoRepository.Save(ref op);
                    }
                }
                pacoteProducaoRepository.Delete(pacote.Id);

                

                itemOrdemProducaoRepository.CommitTransaction();
            }
            catch (Exception ex)
            {
                itemOrdemProducaoRepository.RollbackTransaction();
                throw ex;
            }
        }

        public void UpdateHeader(int pacoteId)
        {
            _repository.UpdateHeader(pacoteId);
        }


        public IEnumerable<PacoteFaccaoValorizado> GetByPacoteFaccaoValorizado(DateTime DataInicio, DateTime DataFim, List<int> Faccao, int Tipo)
        {

            using (var repository = new PacoteProducaoRepository())
            {
                return repository.GetByPacoteFaccaoValorizado(DataInicio, DataFim, Faccao, Tipo);
            }
        }

        public void PacoteEmLote(List<PacoteLoteView> Pacotes,string Usuario,int IdAlmoxarifadoAlternativo,DateTime DataFinalizacao)
        {
            using (PacoteProducaoRepository pacoteRepository = new PacoteProducaoRepository())
            {
                var listMovimentacaoEstoque = new List<MovimentacaoEstoque>();
                EstoqueController estoqueController = new EstoqueController();

                try
                {

                    pacoteRepository.BeginTransaction();
                    List<int> IdsPacote = new List<int>();

                    foreach (var itemPacotes in Pacotes)
                    {
                        var movEstoque = new MovimentacaoEstoque();
                        IdsPacote.Add(itemPacotes.IdPacote);

                        movEstoque.Entrada = itemPacotes.Total;
                        movEstoque.UsuarioId = VestilloSession.UsuarioLogado.Id;
                        movEstoque.ProdutoId = itemPacotes.IdProduto;
                        movEstoque.CorId = itemPacotes.IdCor;
                        movEstoque.TamanhoId = itemPacotes.IdTamanho;
                        if(IdAlmoxarifadoAlternativo > 0)
                        {
                            movEstoque.AlmoxarifadoId = IdAlmoxarifadoAlternativo;
                        }
                        else
                        {
                            movEstoque.AlmoxarifadoId = itemPacotes.IdAlmoxarifado;
                        }
                        
                        movEstoque.Observacao = "Pacote de Produção: " + itemPacotes.RefPacote;
                        movEstoque.DataMovimento = DateTime.Now;
                        movEstoque.IdPacote = itemPacotes.IdPacote;
                        listMovimentacaoEstoque.Add(movEstoque);

                        if (VestilloSession.LancaOperacaoAuto)
                        {
                            var funcDados = new Funcionario();
                            int CodFuncionario = VestilloSession.DefineFuncionarioLancamento;
                            if(CodFuncionario <= 0)
                            {
                                throw new Exception ("Funcionário automático não preenchido !!");
                            }
                            else
                            {
                                funcDados = new FuncionarioService().GetServiceFactory().GetById(CodFuncionario);
                                if(funcDados == null || funcDados.Ativo == false)
                                {
                                    throw new Exception("Funcionário não encontrado ou Inativo !!");
                                }

                            }
                            DateTime DiaHoje = DateTime.Now;
                            int DiaDeHoje = (int)DiaHoje.DayOfWeek;

                            DateTime ProximoDomingo = new DateTime();

                            switch (DiaDeHoje)
                            {
                                case 0: // Domingo
                                    ProximoDomingo = DiaHoje.AddDays(7);
                                    break;
                                case 1://Segunda
                                    ProximoDomingo = DiaHoje.AddDays(6);
                                    break;
                                case 2://Terça
                                    ProximoDomingo = DiaHoje.AddDays(5);
                                    break;
                                case 3://Quarta
                                    ProximoDomingo = DiaHoje.AddDays(4);
                                    break;
                                case 4://Quinta
                                    ProximoDomingo = DiaHoje.AddDays(3);
                                    break;
                                case 5://Sexta
                                    ProximoDomingo = DiaHoje.AddDays(2);
                                    break;
                                case 6://Sabado
                                    ProximoDomingo = DiaHoje.AddDays(1);
                                    break;
                                default:
                                    break;
                            }

                            string nova_data = ProximoDomingo.ToString("dd/MM/yyyy");
                            ProximoDomingo  = Convert.ToDateTime(nova_data);

                            pacoteRepository.UpdatePacoteLoteComLancamento(itemPacotes.IdPacote, itemPacotes.IdItemNaOp, itemPacotes.RefPacote, Usuario, itemPacotes.Total, itemPacotes.DefeitoItem, IdAlmoxarifadoAlternativo, CodFuncionario,ProximoDomingo, DataFinalizacao);



                        }
                        else
                        {

                            pacoteRepository.UpdatePacoteLote(itemPacotes.IdPacote, itemPacotes.IdItemNaOp, Usuario, itemPacotes.Total, itemPacotes.DefeitoItem, IdAlmoxarifadoAlternativo, DataFinalizacao);
                        }
                    }

                    

                    if (listMovimentacaoEstoque.Count > 0)
                    {

                        estoqueController.MovimentarEstoque(listMovimentacaoEstoque);
                    }


                    pacoteRepository.CommitTransaction();

                }
                catch (Exception ex)
                {
                
                    pacoteRepository.RollbackTransaction();
                    throw ex;
                }
            }
                
        }

    }
}
