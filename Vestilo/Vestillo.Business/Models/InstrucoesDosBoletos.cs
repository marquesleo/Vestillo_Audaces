using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("InstrucoesDosBoletos", "InstrucoesDosBoletos")]
    public class InstrucoesDosBoletos
    {
        [Chave]
        public int Id { get; set; }
        [FiltroEmpresa]
        public int IdEmpresa { get; set; }
        public DateTime DataEmissao { get; set; }
        public int IdBoleto { get; set; }
        public int IdBanco { get; set; }
        public int IdInstrucao { get; set; }
        public int RemessaGerada { get; set; }

    }

    public class InstrucoesDosBoletosView
    {
       
        public int Id { get; set; }       
        public int IdEmpresa { get; set; }
        public DateTime DataEmissao { get; set; }
        public int IdBoleto { get; set; }
        public int IdTitulo { get; set; }
        public int IdBanco { get; set; }
        public int IdInstrucao { get; set; }
        public int RemessaGerada { get; set; }
        public string NumTitulo { get; set; }
        public string Parcela { get; set; }
        public string NomeCliente { get; set; }
        public string CpfCnpj { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal Valor { get; set; }
        public string NossoNumero { get; set; }        
        public string DescicaoInstrucao { get; set; }
        public string SimNaoRemessaGerada { get; set; }
       
    }

}
