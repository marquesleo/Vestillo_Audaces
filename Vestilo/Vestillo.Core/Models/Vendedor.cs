using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Core.Models
{
    [Tabela("Colaboradores")]
    public class Vendedor : IModel
    {
        [Chave]
        public int Id { get; set; }
        public string Nome { get; set; }
    }
}
