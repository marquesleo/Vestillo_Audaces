using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{

    [Tabela("ItensPedidoCompra")]
    public class ItemPedidoCompraView : ItemPedidoCompra
    {
        [NaoMapeado]
        public string ProdutoDescricao { get; set; }
        [NaoMapeado]
        public string ProdutoReferencia { get; set; }
        [NaoMapeado]
        public string RefTipoMovimentacao { get; set; }
        [NaoMapeado]
        public string DescricaoTipoMovimentacao { get; set; }
        [NaoMapeado]
        public string TamanhoDescricao { get; set; }
        [NaoMapeado]
        public string CorDescricao { get; set; }
        [NaoMapeado]
        public string TamanhoAbreviatura{ get; set; }
        [NaoMapeado]
        public string CorAbreviatura { get; set; }
        [NaoMapeado]
        public string UMDescricao { get; set; }
        [NaoMapeado]
        public string UMDescricao2 { get; set; }
        [NaoMapeado] 
        public decimal Total
        {
            get
            {
                //Ajustar
                return (base.Preco * base.Qtd);
            }
        }
        [NaoMapeado]
        public decimal AtendidaTotal
        {
            get
            {
                decimal Exc = 0; 
                if (Excedente != null)
                {
                    Exc = Convert.ToDecimal(Excedente);
                }
                
                return (QtdAtendida + Exc);
            }
        }
    }
}
