using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class NaoMapeado: Attribute
    {
        public bool SomenteInsert { get; set; } 

        public NaoMapeado()
        {
            SomenteInsert = false;
        }

        public NaoMapeado(bool somenteInsert)
        {
            SomenteInsert = somenteInsert;
        }
    }
}
