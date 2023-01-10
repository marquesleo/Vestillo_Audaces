using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo;

namespace Vestillo.Business.Models
{
    [Tabela("DespesaFixaVariavelMes", "Despesa Fixa E Variavel Mes")]
    public class DespesaFixaVariavelMes : IModel
    {
        [Chave]
        public int Id { get; set; }
        public int DespesaFixaVariavelId { get; set; }
        public int Mes { get; set; }
        public string Despesa { get; set; }
        public decimal ValorPrevisto { get; set; }
        public decimal ValorRealizado { get; set; }
        public int NaturezaFinanceiraId { get; set; }
    }
}
