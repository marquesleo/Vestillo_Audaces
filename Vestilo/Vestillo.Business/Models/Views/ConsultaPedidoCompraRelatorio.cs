using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models.Views;

namespace Vestillo.Business.Models
{
    public class ConsultaPedidoCompraRelatorio 
    {
        public int FornecedorId { get; set; }
        public int PedidoId { get; set; }
        public string RazaoSocial { get; set; }
        public string CnpjCpf { get; set; }
        public string InscEstadual { get; set; }
        public string Endereco { get; set; }
        public string Numero { get; set; }
        public string Cep { get; set; }
        public string Ddd { get; set; }
        public string Telefone { get; set; }
        public string Bairro { get; set; } 
        public string RefFornecedor { get; set; } 
        
        public string Pedido { get; set; }
        public string Contato { get; set; }
        public string Ordens { get; set; }
        public string Obs { get; set; }
        public string CodigoPedidoCliente { get; set; }
        public string SeqPedCliente { get; set; }
 

        public string RefProduto { get; set; }
        public string DescProduto { get; set; }
        public string CorProduto { get; set; }
        public string UM { get; set; }
        public string UM2 { get; set; }
        public string TamanhoProduto { get; set; }
        public int? TamanhoId { get; set; }
        public Decimal Quantidade { get; set; } 
        public Decimal QuantidadeAtendida { get; set; }
        public Decimal QuantidadeFalta
        {
            get
            {
                if (Status != (int)enumStatusPedidoCompra.Finalizado)
                    return Quantidade - QuantidadeAtendida;
                else
                    return 0;
            }
        }
        public Decimal QuantidadeFinalizada
        {
            get
            {
                if (Status == (int)enumStatusPedidoCompra.Finalizado)
                    return Quantidade - QuantidadeAtendida;
                else
                    return 0;
            }
        }
        public Decimal SaldoCancelado { get; set; }
        public Decimal Preco { get; set; }
        public Decimal ValorTotal { get; set; }
        public Decimal ValorEntregue { get; set; }
        public Decimal ValorEntregar { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime DataEntrega { get; set; }
        public int Status { get; set; }

        public int? IdEstado { get; set; }
        public int? IdMunicipio { get; set; }

        public string DescricaoStatus
        {
            get
            {
                switch (this.Status)
                {
                    case (int)enumStatusPedidoVenda.Finalizado:
                        return "Finalizado";
                    case (int)enumStatusPedidoVenda.Faturado_Parcial:
                        return "Faturado Parcial";
                    case (int)enumStatusPedidoVenda.Faturado_Total:
                        return "Faturado Total";
                    case (int)enumStatusPedidoVenda.Incluido:
                        return "Incluído";
                    case (int)enumStatusPedidoVenda.Bloqueado:
                        return "Bloqueado";
                    case (int)enumStatusPedidoVenda.Liberado:
                        return "Liberado";
                    case (int)enumStatusPedidoVenda.Conferencia:
                        return "Conferência";
                    case (int)enumStatusPedidoVenda.Conferencia_Parcial:
                        return "Conferência Parcial";
                    case (int)enumStatusPedidoVenda.Aguardando_Liberacao:
                        return "Aguardando Liberação";
                    case (int)enumStatusPedidoVenda.Bloqueado_Financeiro:
                        return "Bloqueado Financeiro";
                    case (int)enumStatusPedidoVenda.Credito_Liberado:
                        return "Crédito Liberado";
                    default:
                        return "";
                }
            }
        }

        public int Prazo { get; set; }
        public string DescPrazo
        {
            get {
                if (Prazo == 0)
                    return "-";
                else if (Prazo == 1)
                    return "À vista";
                else
                    return Prazo.ToString();
            }
        }
    }
}
