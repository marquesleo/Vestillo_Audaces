using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
     [Tabela("ProdutoDetalhes", "ProdutoDetalhes")]
    public class ProdutoDetalhe
    {

        [Chave]
        public int Id { get; set; }
        public int IdProduto { get; set; }
        public int IdTamanho { get; set; }
        public int Idcor { get; set; }
        public string codbarras { get; set; }
        public decimal  ultpreco { get; set; }
        public decimal custo { get; set; }
        public string  referenciagradecli { get; set; }
        public bool Inutilizado { get; set; }
        public decimal EstimativaProducao { get; set; }
        public decimal TotalOp { get; set; }
        public bool TamanhoUnico { get; set; }
        public bool CorUnica { get; set; }
        public DateTime DataAlteracao { get; set; }
        public string TrayId { get; set; }
        public string BlingId { get; set; }
    }
}
