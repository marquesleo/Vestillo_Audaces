
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Core.Models
{
    [Tabela("EtqComposicao")]
    public class Composicao : IModel
    {
        [Chave]
        public int Id { get; set; }       
        public int TipoArtigoId { get; set; }
        public int ProdutoId { get; set; }        
        public string DescComposicao { get; set; }
        [DataAtual]
        public DateTime DataCriacaoAlteracao { get; set; }       
        public int UsuarioCriacaoAlteracaoId { get; set; }
        public int Numero { get; set; }
    }
}
