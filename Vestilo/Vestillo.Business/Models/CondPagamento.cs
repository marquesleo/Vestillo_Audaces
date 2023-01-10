using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("CondPagamentos", "Cond.Pagamento")]
    public class CondPagamento
    {

        [Chave]
        public int Id { get; set; }
        [FiltroEmpresa]
        public int IdEmpresa { get; set; }
        [Contador("CondPagamento")]
        [RegistroUnico]
        public string Referencia { get; set; }
        public string Condicao { get; set; }
        [RegistroUnico]
        public string Descricao { get; set; }
        public int DiaCondicao { get; set; }
        public decimal PercJuros { get; set; }
        public decimal TaxaPermanencia { get; set; }
        public decimal Acrescimo { get; set; }
        public int DescontoAcrescimo { get; set; }
        public decimal VlDescAcrescimo { get; set; }
        public bool Ativo { get; set; }
    }
}
