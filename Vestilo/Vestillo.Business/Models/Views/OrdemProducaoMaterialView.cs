using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Vestillo.Tabela("OrdemProducaoMateriais", "Ordem Producao Material")]
    public class OrdemProducaoMaterialView : OrdemProducaoMaterial
    {
        public enum TipoMovimento
        {
            Empenho = 0,
            Baixa = 1,
            Estorno_Empenho = 2,
            Estorno_Baixa = 3

        }

        [Vestillo.NaoMapeado]
        public string MaterialReferencia { get; set; }
        [Vestillo.NaoMapeado]
        public string MaterialDescricao { get; set; }
        [Vestillo.NaoMapeado]
        public string TamanhoDescricao { get; set; }
        [Vestillo.NaoMapeado]
        public string CorDescricao { get; set; }
        [Vestillo.NaoMapeado]
        public string ArmazemDescricao { get; set; }
        //[Vestillo.NaoMapeado]
        //public decimal QuantidadeEmpenhada { get; set; }
        [Vestillo.NaoMapeado]
        public string OrdemProducaoReferencia { get; set; }
        [Vestillo.NaoMapeado]
        public int OrdemProducaoStatus { get; set; }
        [Vestillo.NaoMapeado]
        public int CorProdutoId { get; set; }
        [Vestillo.NaoMapeado]
        public int TamanhoProdutoId { get; set; }
        [Vestillo.NaoMapeado]
        public string UM { get; set; }
        [Vestillo.NaoMapeado]
        public decimal Lancamento { get; set; }
        [Vestillo.NaoMapeado]
        public bool NaoMovimentaEstoque { get; set; }

        [Vestillo.NaoMapeado]
        public decimal Saldo { get; set; }
        [Vestillo.NaoMapeado]
        public decimal EmpenhoLivre { get; set; }
        [Vestillo.NaoMapeado]
        public decimal EmpenhoLivreUsado { get; set; }
        [Vestillo.NaoMapeado]
        public decimal Empenhado { get; set; }

        [Vestillo.NaoMapeado]
        public bool possuiquebra { get; set; }
        [Vestillo.NaoMapeado]
        public string Destino { get; set; }
        [Vestillo.NaoMapeado]
        public TipoMovimento Movimento { get; set; }
        [Vestillo.NaoMapeado]
        public string QuebraManual { get; set; }

        [Vestillo.NaoMapeado]
        public decimal Falta { get; set; }



    }
}
