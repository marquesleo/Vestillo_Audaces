using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class ItemLiberacaoPedidoVendaController : GenericController<ItemLiberacaoPedidoVenda, ItemLiberacaoPedidoVendaRepository>
    {

        public IEnumerable<ItemLiberacaoPedidoVendaView> GetListByItensLiberacaoView(int IdPedido)
        {
            using (ItemLiberacaoPedidoVendaRepository repository = new ItemLiberacaoPedidoVendaRepository())
            {
                return repository.GetListByItensLiberacaoView(IdPedido);
            }
        }

        public IEnumerable<LiberacaoPedidoVenda> GetLiberacaoPedidoVenda(int IdPedido)
        {
            using (ItemLiberacaoPedidoVendaRepository repository = new ItemLiberacaoPedidoVendaRepository())
            {
                return repository.GetLiberacaoPedidoVenda(IdPedido);
            }
        }


        public IEnumerable<ItemLiberacaoPedidoVenda> GetLiberacaoPedidoVendaPorPedidoProdutoEGrade(int IdPedido, int idProduto, int idCor, int idTamanho)
        {
            using (ItemLiberacaoPedidoVendaRepository repository = new ItemLiberacaoPedidoVendaRepository())
            {
                return repository.GetLiberacaoPedidoVendaPorPedidoProdutoEGrade(IdPedido, idProduto, idCor, idTamanho);
            }
        }

        public IEnumerable<ItemLiberacaoPedidoVenda> GetByItemPedidoVenda(int itemPedidoVendaId)
        {
            using (ItemLiberacaoPedidoVendaRepository repository = new ItemLiberacaoPedidoVendaRepository())
            {
                return repository.GetByItemPedidoVenda(itemPedidoVendaId);
            }
        }

        public IEnumerable<ItemLiberacaoPedidoVendaView> GetListByItensLiberacaoParaConferenciaView(List<int> itemPedidoVendaId)
        {
            using (ItemLiberacaoPedidoVendaRepository repository = new ItemLiberacaoPedidoVendaRepository())
            {
                return repository.GetListByItensLiberacaoParaConferenciaView(itemPedidoVendaId);
            }
        }

        public void Save(ref List<ItemLiberacaoPedidoVenda> itensLiberacao)
        {
            var repository = new ItemLiberacaoPedidoVendaRepository();
            var estoqueController = new EstoqueController();
            var itemVendaController = new ItemPedidoVendaController();

            try 
	        {	        
                repository.BeginTransaction();

                foreach(var item in itensLiberacao)
                {
                    var itemSave = item;

                    var itemPedidoVenda = itemVendaController.GetById(item.ItemPedidoVendaId);

                    if (itemPedidoVenda.CorId == null)
                        itemPedidoVenda.CorId = 0;

                    if (itemPedidoVenda.TamanhoId == null)
                        itemPedidoVenda.TamanhoId = 0;


                    var estoque = estoqueController.GetEstoque(item.AlmoxarifadoId, itemPedidoVenda.ProdutoId, (int)itemPedidoVenda.CorId, (int)itemPedidoVenda.TamanhoId,true);

                    if (estoque != null)
                    {
                        estoque.Saldo -= item.Qtd;
                        estoque.Empenhado += item.Qtd;
                        estoqueController.Save(ref estoque);
                    }

                    repository.Save(ref itemSave);
                    item.Id = itemSave.Id;
                }

		        repository.CommitTransaction();
	        }
	        catch (Exception ex)
	        {
		        repository.RollbackTransaction();
		        throw ex;
	        }
        }

        public List<ItemLiberacaoPedidoVenda> GetByPedidoVenda(int pedidoVendaId)
        {
            using (var repository = new ItemLiberacaoPedidoVendaRepository())
            {
                return repository.GetByPedidoVenda(pedidoVendaId);
            }
        }

        public IEnumerable<ItemLiberacaoPedidoVendaView> GetItensLiberacaoParcialSemEstoque()
        {
            var repository = new ItemLiberacaoPedidoVendaRepository();
            var itensLiberacao = repository.GetItensLiberacao();
            var estoqueController = new EstoqueController();
            var itemVendaController = new ItemPedidoVendaController();
            foreach (ItemLiberacaoPedidoVenda itemLiberacao in itensLiberacao)
            {
                var itemSave = itemLiberacao;
                var itemPedidoVenda = itemVendaController.GetById(itemLiberacao.ItemPedidoVendaId);

                if (itemPedidoVenda.CorId == null)
                    itemPedidoVenda.CorId = 0;

                if (itemPedidoVenda.TamanhoId == null)
                    itemPedidoVenda.TamanhoId = 0;


                var estoque = estoqueController.GetEstoque(itemLiberacao.AlmoxarifadoId, itemPedidoVenda.ProdutoId, (int)itemPedidoVenda.CorId, (int)itemPedidoVenda.TamanhoId,true);

                if (estoque != null && estoque.Saldo > 0)
                {
                    if (estoque.Saldo >= itemLiberacao.QtdNaoAtendida)
                    {
                        estoque.Saldo -= itemLiberacao.QtdNaoAtendida;
                        estoque.Empenhado += itemLiberacao.QtdNaoAtendida;
                        estoqueController.Save(ref estoque);
                        itemSave.QtdNaoAtendida = 0;
                        itemSave.Status = (int)enumStatusLiberacaoPedidoVenda.Atendido;
                    }
                    else
                    {
                        itemSave.QtdNaoAtendida -= estoque.Saldo;
                        itemSave.Status = (int)enumStatusLiberacaoPedidoVenda.Atendido_Parcial;
                        estoque.Empenhado += estoque.Saldo;
                        estoque.Saldo = 0;
                        estoqueController.Save(ref estoque);
                    }
                    repository.Save(ref itemSave);

                }
            }
            var itensLiberacaoPedidoVenda = repository.GetItensLiberacaoParcialSemEstoque();
            return itensLiberacaoPedidoVenda;
        }

        public IEnumerable<ItemLiberacaoPedidoVendaView> GetListByConferencia(int conferenciaId)
        {
            return _repository.GetListByConferencia(conferenciaId);
        }

        public IEnumerable<ItemLiberacaoPedidoVendaView> GetItensLiberacaoParcialSemEstoque(bool fichaTecnicaCompleta)
        {
            var repository = new ItemLiberacaoPedidoVendaRepository();
            var itensLiberacao = repository.GetItensLiberacao();
            var estoqueController = new EstoqueController();
            var itemVendaController = new ItemPedidoVendaController();

            //COMENTADO PORQUE NÃO FAZ SENTIDO TESTAR ESTOQUE ANTES DE ABRIR OS PEDIDOS PARA PRODUZIR 19-11-2020
            /* 
            foreach (ItemLiberacaoPedidoVenda itemLiberacao in itensLiberacao)
            {
                var itemSave = itemLiberacao;
                var itemPedidoVenda = itemVendaController.GetById(itemLiberacao.ItemPedidoVendaId);

                if (itemPedidoVenda.CorId == null)
                    itemPedidoVenda.CorId = 0;

                if (itemPedidoVenda.TamanhoId == null)
                    itemPedidoVenda.TamanhoId = 0;


                var estoque = estoqueController.GetEstoque(itemLiberacao.AlmoxarifadoId, itemPedidoVenda.ProdutoId, (int)itemPedidoVenda.CorId, (int)itemPedidoVenda.TamanhoId,true);

                if (estoque != null && estoque.Saldo > 0)
                {
                    if(estoque.Saldo >= itemLiberacao.QtdNaoAtendida)
                    {
                        estoque.Saldo -= itemLiberacao.QtdNaoAtendida;
                        estoque.Empenhado += itemLiberacao.QtdNaoAtendida;
                        estoqueController.Save(ref estoque);
                        itemSave.QtdNaoAtendida = 0;
                        itemSave.Status = (int)enumStatusLiberacaoPedidoVenda.Atendido;
                    }else{
                        itemSave.QtdNaoAtendida -= estoque.Saldo;
                        itemSave.Status = (int)enumStatusLiberacaoPedidoVenda.Atendido_Parcial;
                        estoque.Empenhado += estoque.Saldo;
                        estoque.Saldo = 0;
                        estoqueController.Save(ref estoque);
                    }
                    repository.Save(ref itemSave); 
                    
                } 
            }
            */
            var itensLiberacaoPedidoVenda = repository.GetItensLiberacaoParcialSemEstoque(fichaTecnicaCompleta);
            return itensLiberacaoPedidoVenda;
        }

        public IEnumerable<ItemLiberacaoPedidoVendaView> GetItensLiberacaoParaConferenciaSemEmpenho(List<int> IdPedido, bool visualizar)
        {
            using (ItemLiberacaoPedidoVendaRepository repository = new ItemLiberacaoPedidoVendaRepository())
            {
                return repository.GetItensLiberacaoParaConferenciaSemEmpenho(IdPedido, visualizar);
            }
        }
    }
}
