using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class NfceItensView
    {   
        public int Id { get; set; }
        public int IdNfce { get; set; }
        public int NumItem { get; set; }
        public int IdProduto { get; set; }
        public string DescProduto { get; set; }
        public string RefProduto { get; set; }
        public int IdCor { get; set; }
        public string DescCor { get; set; }
        public string AbreviaturaCor { get; set; } 
        public int IdTamanho { get; set; }
        public string DescTamanho { get; set; }
        public string AbreviaturaTamanho { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Preco { get; set; }
        public decimal DescPercent { get; set; }
        public decimal DescValor { get; set; }
        public decimal Aliquota { get; set; }
        public decimal Total { get; set; }
        public decimal DescontoRateado { get; set; }
        public decimal BaseIcmsRateado { get; set; }
        public decimal ValorIcmsRateado { get; set; }
        public string CodBarrasGrade { get; set; }
        public string CodBarrasUnico { get; set; }
        public string Ncm { get; set; }
        public string Cest { get; set; }
        public string Unidade { get; set; }
        public int Origem { get; set; }
        public int CalculaIcms { get; set; }
        public int Csosn { get; set; }
        public decimal CreditoIcms { get; set; }
        public decimal TributosRateado { get; set; }
        public decimal Qtddevolvida { get; set; }
        public decimal basefcpestado { get; set; } // Novo 4.0
        public decimal Valorfcpestado { get; set; } // Novo 4.0
        public decimal alqfcp { get; set; }   // Novo 4.0
        public int IdItemPedidoVenda { get; set; }
        public bool Devolucao { get; set; }

        public decimal qtdSomada { get; set; } 
        public decimal totalSomado { get; set; } 
        public decimal DescValorSomado { get; set; }


        public bool salvo { get; set; }
        public decimal TotalComDesconto { get; set; }   // Novo 4.0       

    }
}
