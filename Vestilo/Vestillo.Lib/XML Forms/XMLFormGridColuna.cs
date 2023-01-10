using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Lib
{
    public class XMLFormGridColuna
    {
        public string Id { get; set; }
        public string Titulo { get; set; }
        public string Tipo { get; set; }
        public int Tamanho { get; set; }

        public bool PermitirFiltro { get; set; }
        public bool PermitirTotalizador { get; set; }
        public bool PermitirAgrupamento { get; set; }
        public bool OcultarSelecaoColunas { get; set; }
        public string TituloSelecaoColunas { get; set; }
    }
}
