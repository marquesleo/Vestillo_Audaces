using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Lib
{
    public class XMLRegistro
    {
        public object DadosRegistro { get; set; }

        public XMLRegistro(object obj)
        {
            DadosRegistro = obj;
        }

        private bool ExistePropriedade(string pNome)
        {
            return DadosRegistro.GetType().GetProperty(pNome) != null;
        }

        public object RetValor(string nome)
        {
            if (DadosRegistro == null)
                return null;

            if (!ExistePropriedade(nome))
                return null;

            return DadosRegistro.GetType().GetProperty(nome).GetValue(DadosRegistro, null);
        }

        public string RetValorString(string nome)
        {
            if (DadosRegistro == null)
                return null;

            if (!ExistePropriedade(nome))
                return null;

            return DadosRegistro.GetType().GetProperty(nome).GetValue(DadosRegistro, null).ToString();
        }
    }
}
