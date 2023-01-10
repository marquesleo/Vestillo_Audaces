using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models.Views
{
  
    public class RelPedidoPorCliente
    {        
        public int ClienteId { get; set; }
        public string RazaoCliente { get; set; }
        public String CnpjCpf { get; set; }
        public String Endereco { get; set; }
        public int? IdEstado { get; set; }
        public int? IdMunicipio { get; set; }
        public String Cep { get; set; }
        public String Bairro { get; set; }
        public String Ddd { get; set; }
        public String Telefone { get; set; }
        public String InscEstadual { get; set; }
        public string obscliente { get; set; }
        public int ProdutoId { get; set; }
        public string RefProduto { get; set; }
        public string segmento { get; set; }
        public string catalogo { get; set; }
        public string colecao { get; set; }


        public string CorProduto { get; set; }
        public string TamanhoProduto { get; set; }
        
        public Decimal Quantidade { get; set; }
        public Decimal Empenhado { get; set; }
        public Decimal QuantidadeAtendida { get; set; }
        public Decimal SaldoCancelado { get; set; }
        public Decimal ParaAtender { get; set; }
        public Decimal Preco { get; set; }
        public Decimal ValorTotal { get; set; }        
        public int Status { get; set; }

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
    }
}
