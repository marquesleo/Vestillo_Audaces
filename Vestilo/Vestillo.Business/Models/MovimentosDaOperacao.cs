using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Vestillo.Tabela("MovimentosDaOperacao", "Movimentos da Operacao")]
    public class MovimentosDaOperacao
    {
        [Vestillo.Chave]
        public int Id {get;set;}
        [Vestillo.RegistroUnico]
        public string Referencia {get;set;}
        [Vestillo.RegistroUnico]
        public string Descricao  {get;set;}
        public decimal Tempo {get;set;}
        [Vestillo.RegistroUnico]
        public int Ordem { get; set; }
        public bool Ativo { get; set; }
    }
}
