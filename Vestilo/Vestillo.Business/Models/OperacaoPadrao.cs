
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Vestillo.Tabela("OperacaoPadrao", "Operacão Padrão")]
    public class OperacaoPadrao
    {
        [Vestillo.Chave]
        public int Id { get; set; }
        [Vestillo.FiltroEmpresa]
        public int Idempresa { get; set; }
        [Vestillo.Contador("OperacaoPadrao")]
        [Vestillo.RegistroUnico]
        public string Referencia { get; set; }              
        public string Descricao { get; set; }
        public decimal TempoCosturaSemAviamento { get; set;}
        public decimal TempoCosturaComAviamento { get; set; }
        public bool Ativo { get; set; }
        public bool Manual { get; set; }
        public int IdSetor { get; set; }
        public int IdBalanceamento { get; set; }
        public decimal ValorOperacao { get; set; }
        [NaoMapeado]
        public string Setor { get; set; }
        [NaoMapeado]
        public string Balanceamento { get; set; }

    }
}
