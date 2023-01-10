using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo;

namespace Vestillo.Business.Models
{
    [Tabela("FichaTecnicaOperacaoMovimento")]
    public class FichaTecnicaOperacaoMovimento
    {
        [Chave]
        public int Id { get; set; }
        public int FichaTecnicaOperacaoId { get; set; }
        public int MovimentosDaOperacaoId { get; set; }
        public decimal TempoMovimento { get; set; }
    }

    public class FichaTecnicaOperacaoMovimentoView : FichaTecnicaOperacaoMovimento
    {
        public string MovimentosDaOperacaoReferencia { get; set; }
    }
}
