using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Vestillo.Business.Models
{
    [Tabela("UsuariosPortal", "Usuarios do Portal")]
    public class UsuarioPortal
    {
        [Chave]
        public int Id { get; set; }
        public string Nome { get; set; }
        [RegistroUnico]
        public string Login { get; set; }
        [RegistroUnico]
        public string Senha { get; set; }
        public DateTime DataCadastro { get; set; }
        public bool Ativo { get; set; }       
        public int IdVendedor { get; set; }
    }
}