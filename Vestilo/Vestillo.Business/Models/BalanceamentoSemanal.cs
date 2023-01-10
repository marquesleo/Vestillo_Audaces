
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("BalanceamentoSemanal", "Balanceamento Semanal")]
    public class BalanceamentoSemanal
    {
        [Chave]
        public int Id { get; set; }        
        [FiltroEmpresa]
        public int EmpresaId { get; set; }
        public int IdPlanejamento { get; set; }
        [Contador("BalancoSemanal")]
        [RegistroUnico]
        public string Referencia { get; set; }
        public string Descricao { get; set; }                
        public decimal Aproveitamento { get; set; }        
        public decimal Eficiencia { get; set; }
        public decimal Presenca { get; set; }
    }  
}

