using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("NfceItem")]
    public class NfceItem
    {
        [Chave]
        public int Id { get; set; }
        public int Nfce { get; set; }
        public int Numero { get; set; }
        public int Produto { get; set; }
        public int? Cor { get; set; }
        public int? Tamanho { get; set; }
        public decimal Quantidade { get; set; }
        public decimal ValorInitario { get; set; }
        public decimal Aliquota { get; set; }
        public decimal TotalLiquido { get; set; }
    }
}
