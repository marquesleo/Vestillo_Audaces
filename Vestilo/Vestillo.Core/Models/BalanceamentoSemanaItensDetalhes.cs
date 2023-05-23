
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Core.Models
{
    [Tabela("BalanceamentoSemanaItensDetalhes")]
    public class BalanceamentoSemanaItensDetalhes : IModel
    {
        [Chave]
        public int Id { get; set; }
        public int BalanceamentoId { get; set; }
        public string Semanas { get; set; }
        public int Jornada { get; set; }
        public int Pecas { get; set; }
        public decimal PessoasTrabalhando { get; set; }
        public int SetorId { get; set; }   
        public decimal QtdMaq { get; set; }
    }

    public class BalanceamentoSemanaItensDetalhesView
    {
        public string Semanas { get; set; }         
        public int Jornada { get; set; }
        public int Pecas { get; set; }
        public decimal PessoasTrabalhando { get; set; }
        public int SetorId { get; set; }
        [NaoMapeado]
        public string Setor { get; set; }
        public decimal QtdMaq { get; set; }
    }
}

