using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Vestillo.Tabela("Premios", "Prêmios")]
    public class Premio
    {
        [Vestillo.Chave]
        public int Id { get; set; }
        [FiltroEmpresa]
        public int? EmpresaId { get; set; }
        [Vestillo.Contador("ControlePremio")]
        [Vestillo.OrderByColumn]
        public string Referencia { get; set; }
        public string Descricao { get; set; }
        public int MinutosUteis { get; set; }
        public int Dias { get; set; }
        public decimal ValorMes { get; set; }
        public int Modalidade { get; set; }

        [Vestillo.NaoMapeado]
        public List<PessoasPremioView> Pessoas { get; set; }
        [Vestillo.NaoMapeado]
        public List<FaixaPremio> Faixas { get; set; }
        [Vestillo.NaoMapeado]
        public List<ControleFalta> Faltas { get; set; }
        [Vestillo.NaoMapeado]
        public List<MesDias> MesDias { get; set; }

        [Vestillo.NaoMapeado]
        public string RefDescricao { get { return "(" + Referencia + ") " + Descricao; } }
    }
}
