using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("NaturezasFinanceiras", "Naturezas Financeiras")]
    public class NaturezaFinanceira
    {

        [Chave]
        public int Id { get; set; }
        [FiltroEmpresa]
        public int IdEmpresa { get; set; }
        [RegistroUnico]
        public string Referencia { get; set; }
        [RegistroUnico]
        public string Descricao { get; set; }
        public bool Automatico { get; set; }
        public bool CustoFixo { get; set; }
        public bool PadraoParaVendas { get; set; }

    }
}
