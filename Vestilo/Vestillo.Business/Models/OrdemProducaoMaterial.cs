using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Vestillo.Tabela("OrdemProducaoMateriais", "Ordem Producao Material")]
    public class OrdemProducaoMaterial
    {
        [Vestillo.Chave]
        public int Id { get; set; }

        public int ArmazemId { get; set; }
        public int CorId { get; set; }
        public int CorOriginalId { get; set; }
        public int TamanhoId { get; set; }
        public int TamanhoOriginalId { get; set; }
        public int MateriaPrimaId { get; set; }
        public int MateriaPrimaOriginalId { get; set; }
        public int OrdemProducaoId { get; set; }
        public int ItemOrdemProducaoId { get; set; }
        public decimal QuantidadeEmpenhada { get; set; }
        public decimal QuantidadeNecessaria { get; set; }
        public decimal QuantidadeBaixada { get; set; }
        public int DestinoId { get; set; }
        public int sequencia { get; set; }
        public decimal EmpenhoProducao { get; set; }
        [NaoMapeado]
        public bool Excluir { get; set; }
    }
}
