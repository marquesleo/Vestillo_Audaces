
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Lib;

namespace Vestillo.Core.Models
{

    public class FuncionariosRelCracha
    {
        public int idFuncionario { get; set; }
        public string codPonto { get; set; }
        public string Nome { get; set; }
        public string Cargo { get; set; }
        public string Fantasia { get; set; }
        public string Telefone { get; set; }
        public byte[] Foto { get; set; }

    }
}