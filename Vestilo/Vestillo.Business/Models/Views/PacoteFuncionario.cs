using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class PacoteFuncionario
    {
        public string PacoteReferencia { get; set; }
        public string Operacao { get; set; }
        public string Maquina { get; set; }
        public string Funcionario { get; set; }
        public DateTime Data { get; set; }
    }
}
