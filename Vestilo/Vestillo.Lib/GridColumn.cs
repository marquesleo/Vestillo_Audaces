using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Lib
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class GridColumn: Attribute
    {
        public string Descricao { get; private set; }
        public bool Exibir { get; private set; }
        public bool Editavel { get; private set; }

        public GridColumn(string descricao, bool exibir = true)
        {
            Descricao = descricao;
            Exibir = exibir;
        }
    }
}
