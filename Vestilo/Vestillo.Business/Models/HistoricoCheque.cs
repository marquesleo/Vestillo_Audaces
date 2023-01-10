using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{

    [Tabela("HistoricoCheque", "HistoricoCheque")]
    public class HistoricoCheque
    {
        [Chave]
        public int Id { get; set; }
        public int Status { get; set; }
        public int ChequeId { get; set; }
        public DateTime Data { get; set; }
        public int UsuarioId { get; set; }
        public string Observacao { get; set; }
       
    }
}
