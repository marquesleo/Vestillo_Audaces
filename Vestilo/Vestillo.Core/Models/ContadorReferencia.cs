using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Core.Models
{
    [Tabela("contadorescodigo")]
    public class ContadorReferencia : IModel
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
