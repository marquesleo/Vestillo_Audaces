using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Vestillo.Tabela("Produtividade", "Produtividade")]
    public class Produtividade
    {
        [Vestillo.Chave]
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public int FuncionarioId { get; set; }
        public decimal Tempo { get; set; }
        public decimal Jornada { get; set; }
    }
}
