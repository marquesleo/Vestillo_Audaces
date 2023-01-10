using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Lib
{
    public class XMLFormVinculoCampo
    {
        public string Id { get; set; }
        public string Componente { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0} Componente: {1}", Id, Componente);
        }
    }
}
