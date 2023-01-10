using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class Tabela: Attribute
    {
         public string NomeTabela { get; set; }
         public string Modulo { get; set; }

         public Tabela(string nomeTabela)
         {
             this.NomeTabela = nomeTabela;
         }

        public Tabela(string nomeTabela, string modulo)
         {
             this.NomeTabela = nomeTabela;
             this.Modulo = modulo;
         }
    }
}
