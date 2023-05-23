using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class GrupoOperacoesView:GrupoOperacoes
    {
        [Vestillo.NaoMapeado]
        public string TamanhoDescricao { get; set; }
        [Vestillo.NaoMapeado]
        public string CorDescricao { get; set; }
        [Vestillo.NaoMapeado]
        public string OrdemProducaoReferencia { get; set; }
        [Vestillo.NaoMapeado]
        public string OperacaoPadraoReferencia { get; set; }
        [Vestillo.NaoMapeado]
        public string ProdutoReferencia { get; set; }
        [Vestillo.NaoMapeado]
        public string ProdutoDescricao { get; set; }
        [Vestillo.NaoMapeado]
        public DateTime DataEmissao { get; set; }
        [Vestillo.NaoMapeado]
        public decimal TempoTotal { get; set; }
        [Vestillo.NaoMapeado]
        public decimal TempoUnitario { get; set; }
        [Vestillo.NaoMapeado]
        public decimal TempoPacote { get; set; }
        [Vestillo.NaoMapeado]
        public decimal Quantidade { get; set; }
        [Vestillo.NaoMapeado]
        public string PacoteReferencia { get; set; }
        [Vestillo.NaoMapeado]
        public int PacoteStatus { get; set; }
        [Vestillo.NaoMapeado]
        public int PacoteId { get; set; }
        [Vestillo.NaoMapeado]
        public bool Quebra { get; set; }
        [Vestillo.NaoMapeado]
        public int SetorId { get; set; }
        [Vestillo.NaoMapeado]
        public string QuebraManual { get; set; }
        [Vestillo.NaoMapeado]
        public int SequenciaQuebra { get; set; }
        [Vestillo.NaoMapeado]
        public int QtdOperacao { get; set; }
        [Vestillo.NaoMapeado]
        public DateTime? DataConclusao { get; set; }
        [Vestillo.NaoMapeado]
        public string Funcionario { get; set; }
        [Vestillo.NaoMapeado]
        public string Faccao { get; set; }
        [Vestillo.NaoMapeado]
        public string FuncionarioCupom { get; set; }
                

        [Vestillo.NaoMapeado]
        public string Status
        {
            get
            {
                if (Funcionario != null && Funcionario != "")
                {
                    return "Concluida";
                }
                else
                {
                    return "Aberta";
                }
            }
        }
    }
}
