using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("RotaVisitas", "Rota de Visita")]
    public class RotaVisita
    {
        [Chave]
        public int Id { get; set; }
        [FiltroEmpresa]
        public int IdEmpresa { get; set; }
        [RegistroUnico]
        public String Referencia { get; set; }
        [RegistroUnico]
        [OrderByColumn]
        public String Descricao { get; set; }
        public bool Ativo { get; set; }
        public string CorDaRota { get; set; }
    }
}
