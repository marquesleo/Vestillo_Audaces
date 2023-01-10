using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Vestillo.Tabela("FaixaPremio", "Faixa Premio")]
    public class FaixaPremio
    {
        [Vestillo.Chave]
        public int Id { get; set; }
        public decimal Minimo { get; set; }
        public decimal Maximo { get; set; }
        public decimal ValorMaximoDia { get; set; }
        public decimal ValorMaximoMes { get; set; }
        public decimal ValorMinuto { get; set; }
        public int PremioId { get; set; }
        [Vestillo.NaoMapeado]
        public decimal ValorTotal { get; set; }
    }
}
