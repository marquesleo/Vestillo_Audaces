using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models.Views
{
    public class FiltroRelatorioTitulos
    {
        public int[] Colaboradores { get; set; } 
        public int[] Naturezas { get; set; }
        public int[] Vendedores { get; set; }
        public int[] Titulos { get; set; }
        public string PrefixoInicial { get; set; }
        public string PrefixoFinal { get; set; }
        public int[] TipoDocumentos { get; set; }
        public int[] TipoCobranca { get; set; }
        public int[] Bancos { get; set; }
        public int[] Administradoras { get; set; }
        public DateTime? DataEmissaoInicial { get; set; }
        public DateTime? DataEmissaoFinal { get; set; }
        public DateTime? DataVencimentoInicial { get; set; }
        public DateTime? DataVencimentoFinal { get; set; }
        public DateTime? DataPagamentoInicial { get; set; }
        public DateTime? DataPagamentoFinal { get; set; }
        public bool ExibirBaixa { get; set; }
        public bool Agrupar { get; set; }
        public string ColunaParaAgrupar { get; set; }
        public int Status { get; set; }
    }
}
