using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Vestillo.Tabela("OperacaoOperadora", "OperacaoOperadora")]
    public class OperacaoOperadora
    {
        [Vestillo.Chave]
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public int FuncionarioId { get; set; }
        public int PacoteId { get; set; }
        public int OperacaoId { get; set; }
        public int FaccaoId { get; set; }
        public string usuario { get; set; }
        public string Sequencia { get; set; }
    }
}
