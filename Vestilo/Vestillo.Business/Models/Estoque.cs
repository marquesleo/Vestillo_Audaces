using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("Estoque", "Estoque")]
    public class Estoque
    {
        [Chave]
        public int Id { get; set; }
        public int AlmoxarifadoId { get; set; }
        public int ProdutoId { get; set; }
        public int? CorId { get; set; }
        public int? TamanhoId { get; set; }
        public decimal Saldo { get; set; }
        public decimal Empenhado { get; set; }
        public DateTime DataAlteracao { get; set; }
    }
}
