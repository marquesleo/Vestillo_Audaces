using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("TabelasPreco", "Tabela de Preços")]
    public class TabelaPreco
    {
        [Chave]
        public int Id { get; set; }
        [FiltroEmpresa]
        public int EmpresaId { get; set; }
        [Contador("TabPreco")]
        [RegistroUnico]
        public string Referencia { get; set; }
        public string Descricao { get; set; }
        public decimal MargemLucro { get; set; }
        public decimal Frete { get; set; }
        public decimal Comissao { get; set; }
        public decimal OutrosEncargos { get; set; }
        public bool Ativo { get; set; }
        public bool Base { get; set; }
        public int TabelaPrecoBaseId { get; set; }
        public decimal Fator { get; set; }
        public int MetodoArredondamento { get; set; }

        [NaoMapeado]
        public List<ItemTabelaPreco> Itens { get; set; }
    }
}
