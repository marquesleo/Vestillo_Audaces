using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class ConsultaEstoqueProdutoroduzidoView
    {
        public string Id { get; set; }
        public string AlmoxarifadoReferencia { get; set; }
        public int AlmoxarifadoId { get; set; }
        public int ProdutoId { get; set; }
        public string ProdutoReferencia { get; set; }
        public string ProdutoDescricao { get; set; }
        public int TamanhoId { get; set; }
        public string TamanhoAbreviatura { get; set; }
        public int CorId { get; set; }
        public string CorAbreviatura { get; set; }
        public decimal Fisico { get; set; }
        public decimal Empenhado { get; set; }
        public decimal Disponivel { get; set; }
        public int Inutilizado { get; set; }

        public decimal ProduzidoParaEstoque
        {
            get
            {
                return Itens.Where(i => i.PedidoVendaId == null || i.PedidoVendaId == 0).Sum(t => t.Quantidade);
            }
        }

        public decimal ProduzidoParaPedido
        {
            get
            {
                return Itens.Where(i => i.PedidoVendaId > 0).Sum(t => t.Quantidade);
            }
        }

        public decimal PedidoSemEmpenho
        {
           
            get
            {
                return ItensLiberacao.Where(p => p.Id > 0).Sum(t => t.QtdNaoAtendida) + ItensPedido.Where(p => p.Id > 0).Sum(t => t.Qtd);
            }
        }

        public decimal PedidoSemLiberacao
        {
            get
            {
                if (ItensPedidoIncluido != null)
                {
                    return ItensPedidoIncluido.Where(p => p.Id > 0).Sum(t => t.Qtd);
                }
                else
                {
                    return 0;
                }
            }
        }

        public decimal Saldo
        {
            get
            {
                return ((Disponivel + ProduzidoParaEstoque) - PedidoSemEmpenho - PedidoSemLiberacao);
            }
        }

        public decimal SaldoSemLiberacao
        {
            get
            {
                return ((Disponivel + ProduzidoParaEstoque + ProduzidoParaPedido) - PedidoSemEmpenho - PedidoSemLiberacao);
            }
        }

        public string Colecao { get; set; }
        public string Grupo { get; set; }

        public decimal QuantidadeProducao { get; set; }
        public decimal QuantidadeProduzida { get; set; }

        public List<ItemOrdemProducaoView> Itens { get; set; }
        public List<ItemLiberacaoPedidoVenda> ItensLiberacao { get; set; }
        public List<ItemPedidoVenda> ItensPedido { get; set; }
        public List<ItemPedidoVenda> ItensPedidoIncluido { get; set; }

    }
}
