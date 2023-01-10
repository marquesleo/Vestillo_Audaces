using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{

    [Tabela("ItensPedidoVenda")]
    public class ItemPedidoVendaView : ItemPedidoVenda
    {
        [NaoMapeado]
        public string ProdutoReferencia { get; set; }
        [NaoMapeado]
        public string RefCliente { get; set; }        
        [NaoMapeado]
        public string ProdutoDescricao { get; set; }
        [NaoMapeado]
        public string RefTipoMovimentacao { get; set; }
        [NaoMapeado]
        public string DescricaoTipoMovimentacao { get; set; }
        [NaoMapeado]
        public string TamanhoDescricao { get; set; }
        [NaoMapeado]
        public string CorDescricao { get; set; }
        [NaoMapeado]
        public string TamanhoReferencia { get; set; }
        [NaoMapeado]
        public string CorReferencia { get; set; }
        [NaoMapeado]
        public decimal QtdEstoque { get; set; }
        [NaoMapeado]
        public decimal QtdEmpenhada { get; set; }
        [NaoMapeado]
        public decimal QtdNaoAtendida{ get; set; }
        [NaoMapeado]
        public decimal PrecoItemPr { get; set; }
        [NaoMapeado]
        public int IdTemp { get; set; }
        [NaoMapeado]
        public string RefPedido { get; set; }
        [NaoMapeado] 
        public decimal Total
        {
            get
            {
                //Ajustar
                return (base.Preco * base.Qtd);
            }
        }
    }
}
