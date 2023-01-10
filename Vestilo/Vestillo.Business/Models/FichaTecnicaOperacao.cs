using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo;

namespace Vestillo.Business.Models
{
    [Tabela("FichaTecnicaOperacao", "Ficha Técnica Operações")]
    public class FichaTecnicaOperacao
    {
        public FichaTecnicaOperacao()
        {
            Movimentos = new List<FichaTecnicaOperacaoMovimento>();
        }

        [Chave]
        public int Id { get; set; }
        public int FichaTecnicaId { get; set; }
        public int OperacaoPadraoId { get; set; }
        public string OperacaoPadraoDescricao { get; set; }
        public int Numero { get; set; }
        public int SetorId { get; set; }
        public int BalanceamentoId { get; set; }
        public bool UtilizaAviamento { get; set; }
        public decimal TempoCosturaSemAviamento { get; set; }
        public decimal TempoCosturaComAviamento { get; set; }
        public decimal Pontadas { get; set; }
        public decimal? TempoCronometrado { get; set; }
        public decimal TempoCalculado { get; set; }
        public decimal TempoEmSegundos { get; set; }
        public int CapacidadePecas { get; set; }
        public decimal Diferenca { get; set; }
        public bool Ativo { get; set; }
        public string DescricaoOperacao { get; set; }

        [NaoMapeado]
        public List<FichaTecnicaOperacaoMovimento> Movimentos { get; set; }
    }

    public class FichaTecnicaOperacaoView 
    {
        public int Id { get; set; }
        public int FichaTecnicaId { get; set; }
        public int OperacaoPadraoId { get; set; }
        public string OperacaoPadraoDescricao { get; set; }
        public int Numero { get; set; }
        public string SetorDescricao { get; set; }
        public int SetorId { get; set; }
        public string BalanceamentoDescricao { get; set; }
        public int BalanceamentoId { get; set; }
        public bool UtilizaAviamento { get; set; }
        public decimal TempoCosturaSemAviamento { get; set; }
        public decimal TempoCosturaComAviamento { get; set; }
        public decimal Pontadas { get; set; }
        public decimal? TempoCronometrado { get; set; }
        public decimal TempoCalculado { get; set; }
        public decimal TempoEmSegundos { get; set; }
        public int CapacidadePecas { get; set; }
        public decimal Diferenca { get; set; }
        public bool Ativo { get; set; }
        public bool Manual { get; set; }
        public string Maquina { get; set; }
        public string DescricaoOperacao { get; set; }
        public List<FichaTecnicaOperacaoMovimentoView> Movimentos { get; set; }
    }

    public class FichaTecnicaOperacaoProdutoView
    {
        public int Id { get; set; }
        public int FichaTecnicaId { get; set; }
        public int OperacaoPadraoId { get; set; }
        public string OperacaoPadraoDescricao { get; set; }
        public int Numero { get; set; }
        public string SetorDescricao { get; set; }
        public int SetorId { get; set; }
        public string BalanceamentoDescricao { get; set; }
        public int BalanceamentoId { get; set; }
        public bool UtilizaAviamento { get; set; }
        public decimal TempoCosturaSemAviamento { get; set; }
        public string ProdutoDescricao { get; set; }
        public string ProdutoReferencia { get; set; }
        public decimal TempoCosturaComAviamento { get; set; }
        public decimal Pontadas { get; set; }
        public decimal? TempoCronometrado { get; set; }
        public decimal TempoCalculado { get; set; }
        public decimal TempoEmSegundos { get; set; }
        public int CapacidadePecas { get; set; }
        public decimal Diferenca { get; set; }
        public bool Ativo { get; set; }
        public bool Manual { get; set; }
        public string Maquina { get; set; }
        public string DescricaoOperacao { get; set; }
        public List<FichaTecnicaOperacaoMovimentoView> Movimentos { get; set; }
    }

}
