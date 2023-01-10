using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("PremioPartida", "Prêmio Partida")]
    public class PremioPartida
    {
        [Chave]
        public int Id { get; set; }
        [FiltroEmpresa]
        public int? EmpresaId { get; set; }
        [RegistroUnico]
        public string Referencia { get; set; }
        [RegistroUnico]
        public string Descricao { get; set; }
        public bool PremioGrupo { get; set; }
        public bool PremioAssiduidade { get; set; }
        public bool PremioIndividual { get; set; }
        public int? AssTipo { get; set; }
        public int? IndTipo { get; set; }
        public int? GruTipo { get; set; }
        public decimal AssValor { get; set; }
        public decimal IndValor { get; set; }
        public decimal GruValor { get; set; }
        public decimal IndMinimo { get; set; }
        public decimal IndMaximo { get; set; }
        public decimal IndValPartida { get; set; }
        public decimal GruMinimo { get; set; }
        public decimal GruMaximo { get; set; }
        public decimal GruValPartida { get; set; }

        [Vestillo.NaoMapeado]
        public List<PremioPartidaFuncionariosView> Funcionarios { get; set; }

        [Vestillo.NaoMapeado]
        public string RefDescricao { get { return "(" + Referencia + ") " + Descricao; } }

    }

    public class SemanaAssiduidade
    {
        public DateTime Semana { get; set; }
        public bool pagar { get; set; }
    }
}
