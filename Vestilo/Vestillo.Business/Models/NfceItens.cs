using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("NfceItens", "NfceItens")]
    public class NfceItens
    {
        [Chave]
        public int Id { get; set; }
        public int IdNfce { get; set; }
        public int Numero { get; set; }
        public int NumItem { get; set; }
        public int IdProduto { get; set; }
        public int? IdCor { get; set; }
        public int? IdTamanho { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Qtddevolvida { get; set; }
        public decimal Preco { get; set; }
        public decimal Aliquota { get; set; }
        public decimal DescPercent { get; set; }
        public decimal DescValor { get; set; }
        public decimal Total { get; set; }
        public int CalculaIcms { get; set; }
        public decimal DescontoRateado { get; set; }
        public decimal BaseIcmsRateado { get; set; }
        public decimal ValorIcmsRateado { get; set; }
        public decimal TributosRateado { get; set; }
        public decimal basefcpestado { get; set; } // Novo 4.0
        public decimal Valorfcpestado { get; set; } // Novo 4.0
        public decimal alqfcp { get; set; }   // Novo 4.0
        public int? IdItemPedidoVenda { get; set; }
        public bool Devolucao { get; set; }
        public decimal TotalComDesconto { get; set; }   // Novo 4.0       

    }
}
