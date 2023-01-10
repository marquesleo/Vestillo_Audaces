using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public class RegistroUnico : Attribute
    {
        public string Mensagem { get; set; }


        public RegistroUnico()
        {
           
        }

        public RegistroUnico(string mensagem)
        {
            Mensagem = mensagem;
        }
    }
}
