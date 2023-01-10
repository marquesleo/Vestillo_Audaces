using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo;
using Vestillo.Business.Models.Views;

namespace Vestillo.Business.Models
{
    [Vestillo.Tabela("Pacotes", "Pacotes de Produção")]
    public class PacoteProducaoView:PacoteProducao
    {
        [Vestillo.NaoMapeado]
        public string TamanhoDescricao { get; set; }
        [Vestillo.NaoMapeado]
        public string CorDescricao { get; set; }
        [Vestillo.NaoMapeado]
        public string GrupoDescricao { get; set; }
        [Vestillo.NaoMapeado]
        public string OrdemProducaoReferencia { get; set; }
        [Vestillo.NaoMapeado]
        public int OrdemProducaoId { get; set; }
        [Vestillo.NaoMapeado]
        public string ObservacaoOrdem { get; set; }        
        [Vestillo.NaoMapeado]
        public string ProdutoReferencia { get; set; }
        [Vestillo.NaoMapeado]
        public string ProdutoDescricao { get; set; }
        [Vestillo.NaoMapeado]
        public string ProdutoSegmento { get; set; }
        [Vestillo.NaoMapeado]
        public DateTime DataEmissao { get; set; }
        [Vestillo.NaoMapeado]
        public int AlmoxarifadoId { get; set; }
        [Vestillo.NaoMapeado]
        public string AlmoxarifadoDescricao { get; set; }
        [Vestillo.NaoMapeado]
        public string Usuario { get; set; }
        [Vestillo.NaoMapeado]
        public decimal Concluido { get; set; }
        [Vestillo.NaoMapeado]
        public decimal TempoTotal { get; set; }
        [Vestillo.NaoMapeado]
        public decimal TempoUnitario { get; set; }
        [Vestillo.NaoMapeado]
        public int BalanceamentoId { get; set; }

        [Vestillo.NaoMapeado]
        public string DescricaoStatus
        {
            get
            {
                switch (this.Status)
                {
                    case (int)enumStatusPacotesProducao.Aberto:
                        return "Aberto";
                    case (int)enumStatusPacotesProducao.Finalizado:
                        return "Finalizado";
                    case (int)enumStatusPacotesProducao.Concluido:
                        return "Concluido";
                    case (int)enumStatusPacotesProducao.Producao:
                        return "Producao";
                    default:
                        return "Producao";
                }
            }
        }
    }
}
