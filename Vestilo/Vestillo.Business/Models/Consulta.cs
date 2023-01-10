using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("Consultas")]
    public class Consulta
    {
        [Chave]
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string SQL { get; set; }
        public bool Ativo { get; set; }
        public int IdForm { get; set; }

        public override string ToString()
        {
            return string.Format("Id {0} Titulo {1} FormId {2} SQL {3}", Id, Titulo, IdForm, SQL);
        }
    }
}