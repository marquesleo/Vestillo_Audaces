using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class ConsultaEstoqueView
    {
        public string Id { get; set; }
        public string AlmoxarifadoDescricao { get; set; }
        public string ProdutoReferencia { get; set; }
        public string ProdutoDescricao { get; set; }
        public string CatalogoDescricao { get; set; }
        public string ColecaoDescricao { get; set; }        
        public string Catalogo { get; set; }
        public string Colecao { get; set; }
        public string Grupo { get; set; }

        public int TipoItem { get; set; }

        [Vestillo.NaoMapeado]
        public string DescTipoItem
        {
            get
            {
                switch (TipoItem)
                {
                    case 0:
                        return "Produto";
                    case 1:
                        return "Matéria Prima";
                    case 2:
                        return "Ambos";
                    default:
                        return "Não Informado";
                }
            }
        }

        public int UnidadeMedidaId { get; set; }
        public string UnidadeMedidaAbreviatura { get; set; }

        public int TamanhoId { get; set; }
        public string TamanhoAbreviatura { get; set; }
        public int CorId { get; set; }
        public string CorAbreviatura { get; set; }
        public decimal Saldo { get; set; }
        public decimal Empenhado { get; set; }
        public decimal EmpenhadoLiberado { get; set; }
        public decimal PedidoCompra { get; set; }
        public decimal EmProducao { get; set; }

        public string Entrega { get; set; }
        public string Segmento { get; set; }
        public int Produtoid { get; set; }
        public int AlmoxarifadoId { get; set; }

    }
}
