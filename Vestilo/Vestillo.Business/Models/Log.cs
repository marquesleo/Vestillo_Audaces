using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("Log","Log")]
    public class Log
    {
  
        [Chave]
        public int Id { get; set; }
        [FiltroEmpresa]
        public int EmpresaId { get; set; }
        public int UsuarioId { get; set; }
        [DataAtual]
        public DateTime Data { get; set; }
        public int Operacao { get; set; }
        public string Modulo { get; set; }
        public string DescricaoOperacao { get; set; }
        public int ObjetoId { get; set; }
        public string Objeto { get; set; }        
    }
}
