using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class Contador: Attribute
    {
         public string NomeContador { get; set; }

         public Contador(string nomeContador)
         {
             this.NomeContador = nomeContador;
         }
        
    }
}
