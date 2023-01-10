
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    
    [Tabela("Planejamento")]
    public class PlanejamentoSemanal
    {        
        [Chave]
        public int Id { get; set; }
        [Contador("Planejamento")]
        public string Referencia { get; set; }        
        [FiltroEmpresa]
        public int? EmpresaId { get; set; }
        public string Descricao { get; set; }
        public string UsuarioCriacao { get; set; }
        [DataAtual]
        public DateTime DataCriacao { get; set; }
        public string UsuarioAlteracao { get; set; }        
        public DateTime? DataAlteracao { get; set; }
        public string Observacao { get; set; }
    }
}
