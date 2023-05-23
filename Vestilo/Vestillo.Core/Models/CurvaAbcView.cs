using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Lib;

namespace Vestillo.Core.Models
{
    public class CurvaAbcView
    {
        public int IdCliente { set; get; }
        [GridColumn("Referência")]
        public string RefCliente { get; set; }
        [GridColumn("Nome Cliente")]
        public string NomeCliente { get; set; }
        [GridColumn("Estado")]
        public string Uf { get; set; }
        [GridColumn("rota")]
        public string rota { get; set; }
        [GridColumn("Nome Vendedor1")]
        public string NomeVendedor1 { get; set; }
        [GridColumn("Nome Vendedor2")]
        public string NomeVendedor2 { get; set; }
        [GridColumn("Curva")]
        public string Curva { get; set; }
        public decimal TotalProdutos { get; set; }
         [GridColumn("Total Itens")]
        public decimal TotalItens { get; set; }
        public decimal ValDesconto { get; set; }
        public decimal ValorParaCorte { get; set; }
         [GridColumn("Total C/Desconto")]
        public decimal TotalComDesconto { get; set; }
    }
}
