using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("BalanceamentoProducao", "Balanceamento de Produção")]
    public class BalanceamentoProducao
    {
        [Chave]
        public int Id { get; set; }
        public int SetorId { get; set; }
        public int BalanceamentoId { get; set; }
        public int Status { get; set; }
        public decimal? Total { get; set; }
        public decimal? Operadoras { get; set; }
        public decimal? Eficiencia { get; set; }
        public decimal? MinutoProducao { get; set; }
    }
}

