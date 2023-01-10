using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Lib
{
    public class XMLFormGrid
    {
        public XMLFormGrid()
        {
            Colunas = new List<XMLFormGridColuna>();
        }

        public string Id { get; set; }

        public bool PermitirCancelamentoExportaroExcel { get; set; }
        public bool TamanhoColunasAutomatico { get; set; }
        public string InformacaoSemDados { get; set; }
        public int LinhasTituloColuna { get; set; } 
        public int LinhasTituloAgrupamento { get; set; }
        public string ColunaCodigoRegistro { get; set; }
        public bool PermitirCongelarLinhas { get; set; }

        public List<XMLFormGridColuna> Colunas { get; set; }
    }
}
