using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo;

namespace Vestillo.Business.Models
{
    [Tabela("DespesaFixaVariavel","Despesas Fixa e Variável")]
    public class DespesaFixaVariavel : IModel
    {
        [Chave]
        public int Id { get; set; }
        [FiltroEmpresa]
        public int EmpresaId { get; set; }
        public int Ano { get; set; }
        public decimal TotalAnualPrevisto { get; set; }
        public decimal MediaAnualPrevista { get; set; }
        public decimal TotalAnualRealizado { get; set; }
        public decimal MediaAnualRealizada { get; set; }
        public bool AutomatizarContasPagar { get; set; }


        [NaoMapeado]
        public List<DespesaFixaVariavelMes> Meses { get; set; }

        public DespesaFixaVariavel()
        {
            Meses = new List<DespesaFixaVariavelMes>();
        }
    }
}
