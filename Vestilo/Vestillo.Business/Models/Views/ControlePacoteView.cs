using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models.Views
{
    public class ControlePacoteView
    {
        public string OrdemProducaoReferencia { get; set; }
        public string PacoteNumero { get; set; }
        public DateTime DataEmissao { get; set; }
        public string ProdutoReferencia { get; set; }
        public string CorAbreviatura { get; set; }
        public string TamanhoAbreviatura { get; set; }
        public decimal Qtd { get; set; }
        public string Sequencia { get; set; }
        public string Operacao { get; set; }
        public string Maquina { get; set; }
        public decimal TempoUnitario { get; set; }
        public decimal TempoTotal { get; set; }
        public DateTime? DataConclusao { get; set; }
        public string Funcionario { get; set; }
    }
}
