using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("PremioPartidaFuncionarios", "Premiação Funcionários")]
    public class PremioPartidaFuncionariosView : PremioPartidaFuncionarios
    {
        [NaoMapeado]
        public string Nome { get; set; }
        [NaoMapeado]
        public string Cargo { get; set; }
        [NaoMapeado]
        public bool PremioIndividual { get; set; }
        [NaoMapeado]
        public bool PremioGrupo { get; set; }
        [NaoMapeado]
        public bool PremioAssiduidade { get; set; }
        [NaoMapeado]
        public bool Selecionado { get; set; }
    }

    public class GrupoPremioPartidaDiario
    {
        public int premioId { get; set; }
        public decimal valorGrupoDiario { get; set; }
        public DateTime dia { get; set; }
    }

    public class GrupoPremioPartidaMedia
    {
        public int premioId { get; set; }
        public decimal valorGrupoMedia { get; set; }
    }
}
