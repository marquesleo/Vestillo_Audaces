using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Lib
{
    public class XMLFormTooltip
    {
        public string Id { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0} Titulo: {1} Texto: {2}", Id, Titulo, Texto);
        }
    }
}
