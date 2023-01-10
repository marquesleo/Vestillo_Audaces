using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("ExcecaoCalendario", "Exceção de Calendário")]
    public class ExcecaoCalendario
    {
        [Chave]
        public int Id { get; set; }
        [Contador("ExcecaoCalendario")]
        public string Referencia { get; set; }
        [FiltroEmpresa]
        public int EmpresaId { get; set; }
        public int CalendarioId { get; set; }
        public string Descricao { get; set; }
        public  DateTime DataDaExcecao { get; set; }
        public decimal MinutosDescontados { get; set;}        
    }
}