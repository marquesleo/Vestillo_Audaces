using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class OrderByColumn : Attribute
    {
        public bool Desc { get; set; }


        public OrderByColumn()
        {
           
        }

        public OrderByColumn(bool desc)
        {
            this.Desc = desc;
        }
    }
}
