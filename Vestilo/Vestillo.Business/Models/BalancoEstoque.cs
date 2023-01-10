using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("BalancoEstoque", "BalancoEstoque")]
    public class BalancoEstoque
    {
        [Chave]
        public int Id { get; set; }
        [Contador("BalancoEstoque")]
        [RegistroUnico]
        public string Referencia { get; set; }
        public int AlmoxarifadoId { get; set; }
        public int Status { get; set; }
        [Vestillo.DataAtual]
        public DateTime DataInicio { get; set; }
        public DateTime? DataFinalizacao { get; set; }
        public int UserId { get; set; }
        public bool ZerarEmpenho { get; set; }
        public bool QuantidadeZerada { get; set; }
        public bool Colecao { get; set; }
        public bool Catalogo { get; set; }

        [NaoMapeado]
        public List<BalancoEstoqueItensView> Itens { get; set; }
    }
}
