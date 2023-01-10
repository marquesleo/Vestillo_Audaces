using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class OrdemProducaoMaterialEstoqueView
    {
        public int Id { get; set; }
        public int MaterialId { get; set; }
        public int TamanhoId { get; set; }
        public int CorId { get; set; }
        public int ArmazemId { get; set; }
        public int OrdemId { get; set; }
        public string MaterialDescricao { get; set; }
        public string TamanhoDescricao { get; set; }
        public string CorDescricao { get; set; }
        public string ArmazemDescricao { get; set; }
        public string OrdemReferencia { get; set; }
        public string RazaoColaborador { get; set; }
        public decimal Empenhado { get; set; }
        public decimal QuantidadeNecessaria { get; set; }
        public decimal EmpenhoLivre { get; set; }
        public decimal Lancamento { get; set; }
        public bool LimparEmpenho { get; set; }
    }
}
