using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Lib
{
    public  static class Extensions
    {
        public static TEnum ToEnum<TEnum>(this int value)
        {
            return (TEnum)Enum.Parse(typeof(TEnum), value.ToString());
        }
    }
}
