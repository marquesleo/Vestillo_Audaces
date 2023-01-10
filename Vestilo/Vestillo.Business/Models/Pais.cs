using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("Paises", "Paises")]
    public class Pais
    {
        [Chave]
        public int Id { get; set; }
        public String Nome { get; set; }
    }
}
