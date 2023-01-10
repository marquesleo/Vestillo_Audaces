using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Lib
{
    public class XMLFormValidacao
    {
        public string Id { get; set; }
        public string Requerido { get; set; }
        public int MinimoCaracteres { get; set; }
        public string MsgMinimoCaracteres { get; set; }
        public string EMailValido { get; set; }
        public string MsgMaiorQueZero  { get; set; }
        public int MaiorQueValor { get; set; }
        public string MsgMaiorQueValor { get; set; }
    }
}
