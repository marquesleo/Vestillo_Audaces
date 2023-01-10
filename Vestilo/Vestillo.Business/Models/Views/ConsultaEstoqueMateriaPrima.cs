using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models.Views
{
    public class ConsultaEstoqueMateriaPrima
    {
        public string Id { get; set; }
        public string AlmoxarifadoReferencia { get; set; }
        public int AlmoxarifadoId { get; set; }
        public int ProdutoId { get; set; }
        public string ProdutoReferencia { get; set; }
        public string ProdutoDescricao { get; set; }
        public int TamanhoId { get; set; }
        public string TamanhoAbreviatura { get; set; }
        public int CorId { get; set; }
        public string CorAbreviatura { get; set; }
        public decimal Fisico { get; set; }
        public decimal Empenhado { get; set; }
        public decimal Disponivel { get; set; }
        public decimal Custo { get; set; }
        public string Grupo { get; set; }
        public string NCM { get; set; }
        public string UM { get; set; }
        public string Segmento { get; set; }
    }
}
