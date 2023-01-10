using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models.Views;

namespace Vestillo.Business.Models
{
    [Tabela("Balanceamentos", "Balanceamentos")]
    public class Balanceamento
    {
        [Chave]
        public int Id { get; set; }
        [FiltroEmpresa]
        public int EmpresaId { get; set; }
        [Contador("PlanoAnual")]
        [RegistroUnico]
        public string Referencia { get; set; }
        public string Usuario { get; set; }
        public string Descricao { get; set; }
        [DataAtual]
        public DateTime Data { get; set; }
        public string Ordens { get; set; }
        public string Pacotes { get; set; }
        public string Produtos { get; set; }

        [NaoMapeado]
        public List<BalanceamentoProducaoView> BalanceamentosProducao { get; set; }
        [NaoMapeado]
        public List<BalanceamentoProdutoView> BalanceamentosProduto { get; set; }
    }
}
