
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Lib;

namespace Vestillo.Core.Models
{
    public class ItenPorClienteView
    {
        public int IdItemNoPedido { get; set; }
        [GridColumn("Vendedor")]
        public string Vendedor { get; set; }
        [GridColumn("Nome Cliente")]
        public string RazaoSocial { get; set; }
        [GridColumn("Estado")]
        public string Uf { get; set; }
        [GridColumn("Pedido")]
        public string Pedido { get; set; }
        [GridColumn("Tabela de Preço")]
        public string TabelaPreco { get; set; }
        [GridColumn("Ref Item")]
        public string RefItem { get; set; }
        [GridColumn("Catálogo")]
        public string Catalogo { get; set; }
        [GridColumn("Grupo do Item")]
        public string Grupo { get; set; }
        [GridColumn("Cor")]
        public string Cor { get; set; }
        [GridColumn("Tamanho")]
        public string Tamanho { get; set; }        
        [GridColumn("Quantidade")]
        public decimal Quantidade { get; set; }
        [GridColumn("Preço")]
        public decimal Preco { get; set; }
        [GridColumn("Descontos")]
        public decimal Descontos { get; set; }
        [GridColumn("Total")]
        public decimal Total { get; set; }        

    }
}
