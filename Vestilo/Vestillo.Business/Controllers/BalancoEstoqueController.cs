using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Models.Views;

namespace Vestillo.Business.Controllers
{
    public class BalancoEstoqueController : GenericController<BalancoEstoque, BalancoEstoqueRepository>
    {
        public override void Save(ref BalancoEstoque balancoEstoque)
        {
            var repository = new BalancoEstoqueRepository();
            bool openTransaction = false;

            try
            {
                openTransaction = repository.BeginTransaction();
                VestilloSession.UsandoBanco = true;
                base.Save(ref balancoEstoque);

                var ctItem = new BalancoEstoqueItensController();
                ctItem.DeleteByBalanco(balancoEstoque.Id);

                foreach (var item in balancoEstoque.Itens)
                {
                    BalancoEstoqueItens itemBalanco = item;
                    itemBalanco.Id = 0;
                    itemBalanco.BalancoEstoqueId = balancoEstoque.Id;
                    ctItem.Save(ref itemBalanco);
                }                

                VestilloSession.UsandoBanco = false;
                if (openTransaction)
                    repository.CommitTransaction();
            }
            catch (Exception ex)
            {
                VestilloSession.UsandoBanco = false;
                if (openTransaction)
                    repository.RollbackTransaction();

                throw ex;
            }
            finally
            {
                try
                {
                    repository.Dispose();
                }
                finally { }
            }
        }

        public override void Delete(int id)
        {
            bool openTransaction = false;

            var itemBalancoEstoqueRepository = new BalancoEstoqueItensRepository();           

            try
            {
                openTransaction = _repository.BeginTransaction();

                IEnumerable<BalancoEstoqueItensView> itens = itemBalancoEstoqueRepository.GetViewByBalanco(id);
                
                foreach (BalancoEstoqueItensView item in itens)
                {
                    itemBalancoEstoqueRepository.Delete(item.Id);
                }

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

        public IEnumerable<BalancoEstoqueView> GetBalancoEstoque()
         {
             using (var repository = new BalancoEstoqueRepository())
             {
                 return repository.GetBalancoEstoque();
             }
        }

        public BalancoEstoqueView GetByIdView(int idBalanco)
        {
            using (var repository = new BalancoEstoqueRepository())
            {
                return repository.GetByIdView(idBalanco);
            }
        }

        public IEnumerable<BalancoEstoqueItensView> GetBuscaProduto(int almoxarifadoId, int tipoBusca, List<int> id)
        {
            using (var repository = new BalancoEstoqueRepository())
            {
                return repository.GetBuscaProduto(almoxarifadoId, tipoBusca, id);
            }
        }

        public IEnumerable<BalancoEstoqueItensView> GetBuscaByProduto(string busca, bool buscarPorId, int almoxarifadoId)
        {
            using (var repository = new BalancoEstoqueRepository())
            {
                return repository.GetBuscaByProduto(busca, buscarPorId, almoxarifadoId);
            }
        }

        public void FinalizarBalancoEstoque(int balancoId)
        {
            var repository = new BalancoEstoqueRepository();
            var itensRepository = new BalancoEstoqueItensRepository();
            var itensController = new BalancoEstoqueItensController();
            var listMovimentacaoEstoque = new List<MovimentacaoEstoque>();
            bool usaEstoquePedido = VestilloSession.EstoquePedido;

            try
            {
                repository.BeginTransaction();                

                var balanco = repository.GetById(balancoId);
                
                List<BalancoEstoqueItensView> itensBalanco = itensRepository.GetViewByBalanco(balancoId).ToList();

                var rpTipoMov = new TipoMovimentacaoRepository();

                if (itensBalanco != null && itensBalanco.Count > 0)
                {
                    if(usaEstoquePedido) repository.UpdateEstoqueEmpenho(false);

                    int count = 1;
                    foreach (var item in itensBalanco)
                    {
                        if(count % 50 == 0 )
                            Vestillo.Lib.Funcoes.Processar(count.ToString() + " itens já processados, aguarde! " );

                        decimal divergencia = (item.Quantidade - item.Saldo);

                        if (divergencia > 0) //entrada no saldo
                        {
                            var movEstoque = new MovimentacaoEstoque();
                            movEstoque.Baixa = false;
                            movEstoque.SoEmpenho = false;
                            movEstoque.AlmoxarifadoId = balanco.AlmoxarifadoId;
                            movEstoque.Entrada = divergencia;
                            movEstoque.UsuarioId = balanco.UserId;
                            movEstoque.ProdutoId = item.ProdutoId;
                            movEstoque.CorId = item.CorId;
                            movEstoque.TamanhoId = item.TamanhoId;
                            movEstoque.Observacao = "ENTRADA PELO BALANÇO DE ESTOQUE";

                            listMovimentacaoEstoque.Add(movEstoque);
                        }
                        else if (divergencia < 0)
                        {
                            var movEstoque = new MovimentacaoEstoque();
                            movEstoque.Baixa = false;
                            movEstoque.SoEmpenho = false;
                            movEstoque.AlmoxarifadoId = balanco.AlmoxarifadoId;
                            movEstoque.Saida = item.Saldo - item.Quantidade;
                            movEstoque.UsuarioId = balanco.UserId;
                            movEstoque.ProdutoId = item.ProdutoId;
                            movEstoque.CorId = item.CorId;
                            movEstoque.TamanhoId = item.TamanhoId;
                            movEstoque.Observacao = "SAÍDA PELO BALANÇO DE ESTOQUE";
                            listMovimentacaoEstoque.Add(movEstoque);
                        }
                        if (balanco.ZerarEmpenho)
                            divergencia = (item.Quantidade - (item.Saldo + item.Empenhado));

                        BalancoEstoqueItens itemSave = new BalancoEstoqueItens();
                        itemSave = item;
                        itemSave.Divergencia = divergencia;
                        itensController.Save(ref itemSave);

                        count++;
                    }


                    if (listMovimentacaoEstoque.Count > 0)
                    {
                        EstoqueController estoqueController = new EstoqueController();
                        estoqueController.MovimentarEstoque(listMovimentacaoEstoque, false);
                    }

                    if (balanco.ZerarEmpenho)
                    {
                        foreach (var item in itensBalanco)
                        {
                            if ( count == itensBalanco.Count) 
                                Vestillo.Lib.Funcoes.Processar(" Finalizando processos... " + count + " itens já processados, aguarde!" );

                            var itensPedidoVenda = new ItemPedidoVendaRepository().GetItensZerarEmpenho(item.ProdutoId, Convert.ToInt32(item.CorId), Convert.ToInt32(item.TamanhoId), balanco.AlmoxarifadoId);
                            foreach (var itemPedido in itensPedidoVenda)
                            {
                                ItemLiberacaoPedidoVenda itemPedidoSave = new ItemLiberacaoPedidoVenda();
                                itemPedidoSave = itemPedido;
                                itemPedidoSave.QtdNaoAtendida += itemPedido.QtdEmpenhada;
                                itemPedidoSave.QtdEmpenhada = 0;

                                new ItemLiberacaoPedidoVendaRepository().Save(ref itemPedidoSave);

                                var DadosDoEstoque = new EstoqueRepository().GetSaldoAtualProduto(balanco.AlmoxarifadoId, item.ProdutoId, Convert.ToInt32(item.CorId), Convert.ToInt32(item.TamanhoId));
                                var Est = new Estoque();
                                if (DadosDoEstoque != null)
                                {
                                    Est.Id = int.Parse(DadosDoEstoque.Id);
                                    Est.ProdutoId = item.ProdutoId;
                                    Est.AlmoxarifadoId = balanco.AlmoxarifadoId;
                                    if (DadosDoEstoque.CorId > 0)
                                    {
                                        Est.CorId = DadosDoEstoque.CorId;
                                    }
                                    if (DadosDoEstoque.TamanhoId > 0)
                                    {
                                        Est.TamanhoId = DadosDoEstoque.TamanhoId;
                                    }
                                    Est.Empenhado = 0;
                                    Est.Saldo = DadosDoEstoque.Saldo;
                                    Est.DataAlteracao = DateTime.Now;

                                    new EstoqueRepository().Save(ref Est);

                                }
                               
                            }
                            if(itensPedidoVenda == null || itensPedidoVenda.Count() == 0)
                            {
                                var DadosDoEstoque = new EstoqueRepository().GetSaldoAtualProduto(balanco.AlmoxarifadoId, item.ProdutoId, Convert.ToInt32(item.CorId), Convert.ToInt32(item.TamanhoId));
                                var Est = new Estoque();
                                if (DadosDoEstoque != null && DadosDoEstoque.Empenhado > 0)
                                {
                                    Est.Id = int.Parse(DadosDoEstoque.Id);
                                    Est.ProdutoId = item.ProdutoId;
                                    Est.AlmoxarifadoId = balanco.AlmoxarifadoId;
                                    if (DadosDoEstoque.CorId > 0)
                                    {
                                        Est.CorId = DadosDoEstoque.CorId;
                                    }
                                    if (DadosDoEstoque.TamanhoId > 0)
                                    {
                                        Est.TamanhoId = DadosDoEstoque.TamanhoId;
                                    }
                                    Est.Empenhado = 0;
                                    Est.Saldo = DadosDoEstoque.Saldo;
                                    Est.DataAlteracao = DateTime.Now;

                                    new EstoqueRepository().Save(ref Est);

                                }
                            }
                            count++;
                        }
                    }

                    balanco.Status = 1;
                    balanco.DataFinalizacao = DateTime.Now;

                    if (usaEstoquePedido)
                    {
                        repository.UpdateEstoqueEmpenho(true);
                        itensBalanco.ForEach(item =>
                        {
                            repository.AtivarTrigger(item, balanco.AlmoxarifadoId);
                        });
                    }
                    
                }

                repository.Save(ref balanco);
                repository.CommitTransaction();                

            }
            catch (Exception ex)
            {
                repository.RollbackTransaction();
                repository.UpdateEstoqueEmpenho(true);
                throw ex;
            }
        }

        public IEnumerable<BalancoEstoqueView> GetByAlmoxarifado(int idAlmoxarifado)
        {
            using (var repository = new BalancoEstoqueRepository())
            {
                return repository.GetByAlmoxarifado(idAlmoxarifado);
            }
        }

        public void UpdateGridPedido()
        {
            using (var repository = new BalancoEstoqueRepository())
            {
                repository.UpdateGridPedido();
            }
        }

    }
}
