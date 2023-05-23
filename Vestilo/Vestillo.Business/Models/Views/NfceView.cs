using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class NfceView
    {
        public int Id { get; set; }
        public String Referencia { get; set; }
        public string Serie { get; set; }
        public string NumeroNfce { get; set; }        
        public int IdCliente { get; set; }
        public string RefCliente { get; set; }
        public string NomeCliente { get; set; }
        public DateTime DataEmissao { get; set; }
        public DateTime? DataFinalizacao { get; set; }
        public decimal Desconto { get; set; }
        public decimal DescontoGrid { get; set; }
        public decimal TotalOriginal { get; set; }
        public decimal TotalDevolvido { get; set; }
        public decimal Total { get; set; }
        public decimal TotalItens { get; set; }
        public decimal TotalItensDevolvidos { get; set; }
        public int RecebidaSefaz { get; set; }
        public string RecebSefaz { get; set; }
        public string EmContingencia { get; set; }
        public string Status { get; set; }
        public int IdVendedor { get; set; }
        public string RefVendedor { get; set; }
        public string NomeVendedor { get; set; }
        public string TabPreco { get; set; }
        public string DescTipoNFCe { get; set; }
        public string Observacao { get; set; }
        public string NomeGuia { get; set; }
        public int IdEmpresa { get; set; }

        public decimal TotalSemDesconto
        {
            get
            {
                return (TotalOriginal - TotalDevolvido);
            }
        }

        public decimal DescontoTotal
        {
            get
            {
                return (Desconto + TotalDevolvido);
            }
        }

    }
}
