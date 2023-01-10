using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("Segmentos", "Segmentos")]
    public class Segmento
    {
        [Chave]
        public int Id { get; set; }
        [FiltroEmpresa]
        public int IdEmpresa { get; set; }
        [RegistroUnico]
        public string Abreviatura { get; set; }
        [RegistroUnico]
        public string Descricao { get; set; }
    }
}
