using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Vestillo.Tabela("AtividadeFaccao", "Atividade Facção")]
    public class AtividadeFaccao
    {
        [Vestillo.Chave]
        public int Id { get; set; }
        [Vestillo.FiltroEmpresa]
        public int Idempresa { get; set; }
        [Vestillo.Contador("AtividadeFaccao")]
        [Vestillo.RegistroUnico]
        public string Referencia { get; set; }
        [OrderByColumn]
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public decimal ValorOperacao { get; set; }
        public bool Milheiro { get; set; }
    }
}
