using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Vestillo.Tabela("GrupoOperacoes", "Grupo de Operações")]
    public class GrupoOperacoes
    {
        [Vestillo.Chave]
        public int Id { get; set; }
        public int SetorId { get; set; }
        public int BalanceamentoId { get; set; }
        public int GrupoPacoteId { get; set; }
        public string TituloCupom { get; set; }
        public int OperacaoPadraoId { get; set; }
        public decimal Tempo { get; set; }
        [NaoMapeado]
        public decimal TempoCalculado { get; set; }
        [NaoMapeado]
        public decimal TempoCronometrado { get; set; }
        public string Sequencia { get; set; }
        public string Texto { get; set; }
        public int IdOperadorCupomEletronico { get; set; }
    }
}
