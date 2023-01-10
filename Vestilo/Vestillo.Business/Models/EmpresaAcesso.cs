using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("EmpresaAcesso")]
    public class EmpresaAcesso
    {
        [Chave]
        public int Id { get; set; }
        public int EmpresaId { get; set; }
        public int EmpresaAcessoId { get; set; }
        public string Tabela { get; set; }
    }
}
