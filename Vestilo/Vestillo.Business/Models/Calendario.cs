using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("Calendario","Calendário")]
    public class Calendario
    {

        public Calendario()
        {
            Faixas = new List<CalendarioFaixas>();
        }

        [Chave]
        public int Id { get; set; }
        [Contador("calendario")]
        public string Referencia { get; set; }
        [FiltroEmpresa]
        public int EmpresaId { get; set; }
        public string Descricao { get; set; }
        public int MinutosTotaisDaSemana { get; set; }
        public bool Ativo { get; set; }

        [NaoMapeado]
        public List<CalendarioFaixas> Faixas { get; set; }
    }

    [Tabela("CalendarioFaixas")]
    public class CalendarioFaixas
    {
        [Chave]
        public int Id { get; set; }
        public int CalendarioId { get; set; }
        public DayOfWeek Dia { get; set; }
        public string HoraInicial { get; set; }
        public string HoraFinal { get; set; }
    }
}
