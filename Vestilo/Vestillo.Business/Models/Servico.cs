using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("Servicos", "Servicos")]
    public class Servico
    {
        [Chave]
        public int Id { get; set; }
        [Contador("Servico")]
        [RegistroUnico]
        public String Referencia { get; set; }
        [FiltroEmpresa]
        public int IdEmpresa { get; set; }
        [RegistroUnico]
        public String Descricao { get; set; }
        public int IdGrupo { get; set; }
        public decimal Ipi { get; set; }
        public decimal Icms { get; set; }
        public decimal Custo { get; set; }
        public String Observacao { get; set; }
        public bool Ativo { get; set; }


    }
}
