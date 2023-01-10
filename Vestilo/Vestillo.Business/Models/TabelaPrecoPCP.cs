using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo;

namespace Vestillo.Business.Models
{
    [Vestillo.Tabela("TabelaPrecoPCP", "Tabela de Preços PCP")]
    public class TabelaPrecoPCP
    {
        [Vestillo.Chave]
        public int Id { get; set; }
        [Vestillo.FiltroEmpresa]
        public int EmpresaId { get; set; }
        [Vestillo.Contador("TabPrecoPCP")]
        [Vestillo.RegistroUnico("Já existe uma tabela de preço com a referência inserida.")]
        public string Referencia { get; set; }
        [Vestillo.RegistroUnico("Já existe uma tabela de preço com a descrição inserida.")]
        public string Descricao { get; set; }
        public decimal Lucro { get; set; }
        public decimal Frete { get; set; }
        public decimal Comissao { get; set; }
        public decimal Encargos { get; set; }
        public decimal Outros { get; set; }
        [Vestillo.NaoMapeado]
        public List<ItemTabelaPrecoPCP> Itens { get; set; }
    }
}
