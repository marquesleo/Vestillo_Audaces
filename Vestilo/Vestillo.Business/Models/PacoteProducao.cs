using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Vestillo.Tabela("Pacotes", "Pacotes de Produção")]
    public class PacoteProducao
    {
        [Vestillo.Chave]
        public int Id { get; set; }
        public int GrupoPacoteId { get; set; }
        public int? CorId { get; set; }
        public int? TamanhoId { get; set; }
        public int ProdutoId { get; set; }
        public int ItemOrdemProducaoId { get; set; }
        [Vestillo.Contador("PacoteProducao")]
        public string Referencia { get; set; }
        public decimal Quantidade { get; set; }
        public int PossuiCupom { get; set; }
        public DateTime? DataEntrada { get; set; }
        public DateTime? DataSaida { get; set; }
        public decimal QtdDefeito { get; set; }
        public decimal TempoPacote { get; set; }
        public int Status { get; set; }
        public decimal QuantidadeAlternativa { get; set; }
        public int AlmoxarifadoAlternativoId { get; set; }
        public int CorAlteradaId { get; set; }
        public int TamanhoAlteradoId { get; set; }
        public int EntradaFaccao { get; set; }
        public int CupomEmGrupo { get; set; }
        public string Observacao { get; set; }

        public decimal QtdProduzida { get; set; }
        public DateTime? DataCriacaoCEP { get; set; }
        public int UsaCupom { get; set; }

        //public int QtdOperacoes { get; set; }
        //public int QtdOperacoesLancadas { get; set; }
    }
}
