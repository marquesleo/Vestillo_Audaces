using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("BalancoEstoqueItens")]
    public class BalancoEstoqueItensView : BalancoEstoqueItens
    {
        [NaoMapeado]
        public string CatalogoDescricao { get; set; }
        [NaoMapeado]
        public string ColecaoDescricao { get; set; }
        [NaoMapeado]
        public string ProdutoDescricao { get; set; }
        [NaoMapeado]
        public string ProdutoReferencia { get; set; }
        [NaoMapeado]
        public string TamanhoAbreviatura { get; set; }
        [NaoMapeado]
        public string CorAbreviatura { get; set; }
        [NaoMapeado]
        public decimal Empenhado { get; set; }
        [NaoMapeado]
        public decimal Saldo { get; set; }
        [NaoMapeado]
        public bool EmpenhoDivergencia { get; set; }
        [NaoMapeado]
        public decimal DivergenciaGrid
        {
            get
            {
                if(EmpenhoDivergencia)
                    return (base.Quantidade - (Saldo + Empenhado));                
                else
                    return (base.Quantidade - this.Saldo);
            }
        }
    }
}
