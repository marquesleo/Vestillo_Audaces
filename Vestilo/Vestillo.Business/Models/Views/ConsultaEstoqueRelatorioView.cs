using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class ConsultaEstoqueRelatorioView
    {
        public string RefProduto { get; set; }
        public string DescProduto { get; set; }
        public string CorAbreviatura { get; set; }
        public string TamAbreviatura { get; set; }
        public string Colecao { get; set; }
        public string Segmento { get; set; }
        public string Catalogo { get; set; }
        public string Entrega { get; set; }
        public Decimal Saldo { get; set; }
        public Decimal Empenhado { get; set; }
        public Decimal Faturado { get; set; }
        public Decimal NaoAtendido { get; set; }
        public string AlmoxarifadoDescricao { get; set; }
    }
}
