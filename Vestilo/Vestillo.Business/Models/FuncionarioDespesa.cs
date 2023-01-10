using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("FuncionarioDespesa")]
    public class FuncionarioDespesa
    {
        [Chave]
        public int Id { get; set; }
        public int FuncionarioId { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public bool IncideNaFolha { get; set; }
    }
}
