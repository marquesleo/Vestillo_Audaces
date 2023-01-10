using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("ContadoresCodigo", "ContadoresCodigo")]
    public class ContadorCodigo
    {
        [Chave]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string Prefixo { get; set; }
        public int ContadorAtual { get; set; }
        public int CasasDecimais { get; set; }
        public bool Ativo { get; set; }
    }
}