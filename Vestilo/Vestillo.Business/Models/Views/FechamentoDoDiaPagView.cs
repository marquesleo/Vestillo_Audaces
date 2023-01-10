using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class FechamentoDoDiaPagView
    {
        public int IdNfe { get; set; }
        public string ReferenciaFat { get; set; }
        public string NumNota { get; set; }
        public DateTime? Inclusao { get; set; }
        public DateTime? Emissao { get; set; }
        public decimal Desconto { get; set; }
        public decimal Frete { get; set; }
        public decimal Despesas { get; set; }
        public decimal Total { get; set; }
        public int IdTitulo { get; set; }
        public string RefTitulo { get; set; }
        public string ParcelaTitulo { get; set; }
        public DateTime? VencTitulo { get; set; }
        public decimal ValorParcela { get; set; }
        public string SisCob { get; set; }
        public string RefCliente { get; set; }
        public string NomeCliente { get; set; }
        public string NomeVendedor { get; set; }
        public string TabPreco { get; set; }        
    }
}

