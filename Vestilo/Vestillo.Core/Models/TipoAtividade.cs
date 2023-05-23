using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Core.Models
{
    
    [Tabela("TipoAtividade")]
    public class TipoAtividade : IModel
    {
        [Chave]
        public int Id { get; set; }
        [Contador("TipoAtividade")]
        public string Referencia { get; set; }
        [FiltroEmpresa]
        public int? EmpresaId { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
    }
}
