
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("comissoesvendedor", "Comissao Vendedor")]
    public class Comissoesvendedor
    {
        [Chave]
        public int Id { get; set; }
        [FiltroEmpresa]
        public int IdEmpresa { get; set; }        
        public int Status { get; set; }
        [Contador("ComissoesVendedor")]
        public string Referencia { get; set; }
        public string parcela { get; set; } 
        public DateTime dataemissao { get; set; }
        public DateTime? dataliberacao { get; set; }
        public decimal percentual { get; set; }       
        public decimal basecalculo { get; set; }
        public decimal valor { get; set; }
        public int? idNotaFat { get; set; }
        public int? idnotaconsumidor { get; set; }
        public int? idcontasreceber { get; set; }              
        public int idvendedor { get; set; }
        public int? idGuia { get; set; }
        public int? idcontaspagar { get; set; }
        public bool Ativo { get; set; }        
        public string Obs { get; set; }
        public int ExibirTitulo { get; set; }

    }
}
