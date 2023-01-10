using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("MovimentacaoEstoque", "MovimentacaoEstoque")]
    public class MovimentacaoEstoque
    {
        [Chave]
        public int Id { get; set; }
        [NaoMapeado]
        public int AlmoxarifadoId { get; set; }
        [NaoMapeado]
        public int ProdutoId { get; set; }
        public int EstoqueId { get; set; }
        [NaoMapeado]
        public int? TamanhoId { get; set; }
        public int UsuarioId { get; set; }
        [NaoMapeado]
        public int? CorId { get; set; }
        public DateTime DataMovimento { get; set; }
        public decimal Saida { get; set; }
        public decimal Entrada { get; set; }
        public bool Empenho { get; set; }
        public bool Baixa { get; set; }
        [NaoMapeado]
        public bool SoEmpenho { get; set; }
        public string Observacao { get; set; }
        public int? IdPacote { get; set; }
        public bool CancelamentoPacote { get; set; }
    }
}
