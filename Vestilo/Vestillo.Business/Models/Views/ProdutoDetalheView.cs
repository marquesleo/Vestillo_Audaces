
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    
    public class ProdutoDetalheView
    {
       
        public int Id { get; set; }
        public int IdProduto { get; set; }
        public int IdTamanho { get; set; }
        public int Idcor { get; set; }
        public string codbarras { get; set; }
        public decimal  ultpreco { get; set; }
        public decimal custo { get; set; }
        public string  referenciagradecli { get; set; }
        public bool Inutilizado { get; set; }
        public string DescTamanho { get; set; }
        public string DescCor { get; set; }        
        public string AbvCor { get; set; }
        public string AbvTamanho { get; set; }
        public string RefProduto { get; set; }
        public string DescProduto { get; set; }
        public decimal Qtd { get; set; }
        public decimal? QtdPedido { get; set; }
        public string PedidoCliente { get; set; }
        public string SeqPedCliente { get; set; }
        public decimal PerctIpiItem { get; set; }
        public decimal EstimativaProducao { get; set; }
        public decimal TotalOp { get; set; }
        public decimal VrIpiDevolvido { get; set; }
        public bool TamanhoUnico { get; set; }
        public bool CorUnica { get; set; }
        public string TrayId { get; set; }
        public string BlingId { get; set; }

    }
}