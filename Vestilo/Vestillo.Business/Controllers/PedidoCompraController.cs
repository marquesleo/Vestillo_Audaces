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
    public class PedidoCompraController: GenericController<PedidoCompra, PedidoCompraRepository>
    {

        public PedidoCompraView GetByIdView(int id)
        {
            using (var repository = new PedidoCompraRepository())
            {
                return repository.GetByIdView(id);
            }   
        }

        public IEnumerable<PedidoCompraView> GetAllView()
        {
            using (var repository = new PedidoCompraRepository())
            {
                return repository.GetAllView();
            }   
        }

        public IEnumerable<PedidoCompraView> GetAllViewComItens()
        {
            using (var repository = new PedidoCompraRepository())
            {
                var rep = repository.GetAllView().ToList();                
                rep.ForEach(p =>
                {
                    p.Itens = new ItemPedidoCompraRepository().GetByPedido(p.Id).ToList();
                });              
                
                return rep;
            }
        }

        public IEnumerable<PedidoCompraView> GetListPorReferencia(string referencia, string parametrosDaBusca)
        {
            using (PedidoCompraRepository repository = new PedidoCompraRepository())
            {
                return repository.GetListPorReferencia(referencia, parametrosDaBusca);
            }
        }

        public IEnumerable<PedidoCompraView> GetListPorFornecedor(string fornecedor, string parametrosDaBusca)
        {
            using (PedidoCompraRepository repository = new PedidoCompraRepository())
            {
                return repository.GetListPorFornecedor(fornecedor, parametrosDaBusca);
            }
        }

        public IEnumerable<ConsultaPedidoCompraRelatorio> GetPedidoParaRelatorio(FiltroPedidoCompra filtro)
        {
            using (PedidoCompraRepository repository = new PedidoCompraRepository())
            {
                var listaItens =  repository.GetPedidoParaRelatorio(filtro).ToList();

                if (VestilloSession.UsaOrdenacaoFixa && listaItens.Count > 0)
                {
                    List<ConsultaPedidoCompraRelatorio> listaOrdenada = new List<ConsultaPedidoCompraRelatorio>();
                    List<ConsultaPedidoCompraRelatorio> listaOrdenadaProdCor = new List<ConsultaPedidoCompraRelatorio>();

                    var anteriorCor = listaItens.First().CorProduto;
                    listaItens.ForEach(item =>
                    {
                        var corAtual = item.CorProduto;

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

        public List<ConsultaPedidoCompraRelatorio> retornaTamanhoOrdenado(List<ConsultaPedidoCompraRelatorio> listaNaoOrddenada)
        {
            List<ConsultaPedidoCompraRelatorio> listaOrdenada = new List<ConsultaPedidoCompraRelatorio>();
            var listaNumerica = listaNaoOrddenada.Where(l => int.TryParse(l.TamanhoProduto, out _))
                                                .OrderBy(l => l.RefProduto)
                                                .ThenBy(l => l.CorProduto)
                                                .ThenBy(l => l.TamanhoProduto)
                                                .ToList();

            listaOrdenada.AddRange(listaNumerica);

            var listaNaoNumerica = listaNaoOrddenada.Where(l => !int.TryParse(l.TamanhoProduto, out _))
                                                .OrderBy(l => l.RefProduto)
                                                .ThenBy(l => l.CorProduto)
                                                .ThenBy(l => l.TamanhoId)
                                                .ToList();

            listaOrdenada.AddRange(listaNaoNumerica);

            return listaOrdenada;
        }

        public override void Delete(int id)
        {
            var contasPagarRepository = new ContasPagarRepository();
            var itemPedidoCompraRepository = new ItemPedidoCompraRepository();
            var pedidoCompraOrdemRepository = new PedidoCompraOrdemProducaoRepository();

            try
            {
                itemPedidoCompraRepository.BeginTransaction();

                IEnumerable<ContasPagarView> parcelas = contasPagarRepository.GetByPedidoCompra(id);

                if (parcelas != null && parcelas.Count() > 0)
                {
                    if (parcelas.Where(x=> x.ValorPago > 0).Count() > 0)
                        throw new Exception("O Pedido não pode ser excluído pois possui parcelas baixadas.");

                    foreach (var p in parcelas)
                    {
                        contasPagarRepository.Delete(p.Id);
                    }
                }

                IEnumerable<ItemPedidoCompraView> itens = itemPedidoCompraRepository.GetByPedido(id);

                if (itens != null && itens.Count() > 0)
                {
                    foreach (var i in itens)
                    {
                        itemPedidoCompraRepository.Delete(i.Id);
                    }
                }

                IEnumerable<PedidoCompraOrdemProducao> pedidoOrdem = pedidoCompraOrdemRepository.GetByPedido(id);

                if (pedidoOrdem != null && pedidoOrdem.Count() > 0)
                {
                    foreach (var po in pedidoOrdem)
                    {
                        pedidoCompraOrdemRepository.Delete(po.Id);
                    }
                }

                base.Delete(id);

                itemPedidoCompraRepository.CommitTransaction();
            }
            catch (Exception ex)
            {
                itemPedidoCompraRepository.RollbackTransaction();
                throw ex;
            }
        }

        public override void Save(ref PedidoCompra pedido)
        {
            var itemPedidoCompraRepository = new ItemPedidoCompraRepository();
            var contasPagarController = new ContasPagarController();
            var pedidoCompraOrdemRepository = new PedidoCompraOrdemProducaoRepository();

            try
            {
                itemPedidoCompraRepository.BeginTransaction();
                base.Save(ref pedido);

                if (pedido.Itens != null && pedido.Itens.Count() > 0)
                {
                    var itens = pedido.Itens.Where(x=> x.Qtd > 0).ToList();

                    foreach (ItemPedidoCompra item in itens)
                    {
                        ItemPedidoCompra itemSave;
                        if (item.Id > 0)
                        {
                            itemSave = itemPedidoCompraRepository.GetById(item.Id);
                            itemSave.Qtd = item.Qtd;
                            itemSave.QtdUnidadeMedida2 = item.QtdUnidadeMedida2;
                            itemSave.Preco = item.Preco;
                        }
                        else
                        {
                            itemSave = item;
                        }

                        itemSave.PedidoCompraId = pedido.Id;

                        if (itemSave.Qtd != 0)
                            itemPedidoCompraRepository.Save(ref itemSave);

                        item.Id = itemSave.Id;
                    }
                }

                if (pedido.Parcelas != null && pedido.Parcelas.Count() > 0)
                {
                    foreach (ContasPagar p in pedido.Parcelas)
                    {
                        var parcela = p;
                        parcela.IdPedidoCompra = pedido.Id;

                        if (parcela.ValorPago == 0 && parcela.ValorParcela == 0)
                            contasPagarController.Delete(parcela.Id);
                        else
                        {
                            if (p.Id > 0)
                            {
                                if (parcela.ValorPago == 0)
                                {
                                    var pBd = contasPagarController.GetById(p.Id);

                                    pBd.ValorParcela = p.ValorParcela;
                                    pBd.DataVencimento = p.DataVencimento;
                                    pBd.Saldo = p.Saldo;

                                    contasPagarController.Save(ref pBd);
                                }
                            }
                            else
                                contasPagarController.Save(ref parcela);
                        }
                    }
                }

                    
                if (pedido.Itens != null && pedido.Itens.Count() > 0)
                {
                    var itens = pedido.Itens.Where(x => x.Qtd == 0).ToList();
                        
                    foreach (ItemPedidoCompra item in itens)
                    {
                        itemPedidoCompraRepository.Delete(item.Id);
                    }
                }

                if (pedido.PedidosOrdens != null && pedido.PedidosOrdens.Count() > 0)
                {
                    foreach (PedidoCompraOrdemProducao item in pedido.PedidosOrdens)
                    {
                        PedidoCompraOrdemProducao itemSave = new PedidoCompraOrdemProducao();
                        itemSave = item;
                        itemSave.PedidoCompraId = pedido.Id;
                        pedidoCompraOrdemRepository.Save(ref itemSave);
                    }
                }

                itemPedidoCompraRepository.CommitTransaction();
            }
            catch (Exception ex)
            {
                itemPedidoCompraRepository.RollbackTransaction();
                throw ex;
            }
        }

        public void FinalizarPedidoCompra(int pedidoCompraId)
        {
            var repository = new PedidoCompraRepository();
            var itensRepository = new ItemPedidoCompraRepository();
          
            try
            {
                repository.BeginTransaction();

                var pedido = repository.GetById(pedidoCompraId);

                if (pedido.Status == (int)enumStatusPedidoCompra.Faturado_Total)
                {
                    throw new Exception("O Pedido não pode ser Finalizado pois o mesmo já foi faturado total!");
                }
                else if (pedido.Status == (int)enumStatusPedidoCompra.Finalizado)
                {
                    throw new Exception("Pedido de Compra já finalizado.");
                }

                UpdateStatus(pedidoCompraId, enumStatusPedidoCompra.Finalizado);

                repository.CommitTransaction();
            }  
            catch (Exception ex)
            {
                repository.RollbackTransaction();
                throw ex;
            }
        }

        public void UpdateStatus(int pedidoCompraId, enumStatusPedidoCompra status)
        {
            using (var repository = new PedidoCompraRepository())
            {
                repository.UpdateStatus(pedidoCompraId, status);
            }   
        }

        public List<int> GetSemanas()
        {
            using (var repository = new PedidoCompraRepository())
            {
                return repository.GetSemanas();
            }
        }
    }
}
