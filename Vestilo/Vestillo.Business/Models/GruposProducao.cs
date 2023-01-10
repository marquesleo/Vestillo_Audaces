using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo;

namespace Vestillo.Business.Models
{
    [Vestillo.Tabela("GruposProducao")]
    public class GruposProducao
    {
        [Vestillo.Chave]
        public int Id { get; set; }
        [Vestillo.RegistroUnico]
        public string Abreviatura { get; set; }
        [Vestillo.OrderByColumn]
        [Vestillo.RegistroUnico]
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
    }
}
