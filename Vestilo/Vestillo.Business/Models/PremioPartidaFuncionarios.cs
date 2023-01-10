using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("PremioPartidaFuncionarios", "Premiação Funcionários")]
    public class PremioPartidaFuncionarios
    {
        [Chave]
        public int Id { get; set; }        
        public int IdFuncionario { get; set; }
        public int IdPremio { get; set; }
    }
}
