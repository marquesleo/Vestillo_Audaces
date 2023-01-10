using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("TipoMovimentacoes", "Tipo Movimentação")]
    public class TipoMovimentacao
    {

        [Chave]
        public int Id { get; set; }
        [FiltroEmpresa]
        public int IdEmpresa { get; set; }
        [RegistroUnico]
        [OrderByColumn]
        public string Referencia { get; set; }
        [RegistroUnico]
        public string Descricao { get; set; }
        public int IdCfop { get; set; }
        public int Tipo { get; set; }
        public int GeraDuplicata { get; set; }
        public int AtualizaEstoque { get; set; }
        public int CalculaIcms { get; set; }
        public int CalculaIpi { get; set; }
        public int CalculaIss { get; set; }
        public decimal PercentualReducaoIcms { get; set; }
        public decimal PercentualReducaoIpi { get; set; }
        public decimal PercentualReducaoIss { get; set; }
        public int ReduzAliquotaIcms { get; set; }
        public int ReduzAliquotaIpi { get; set; }
        public int ReduzAliquotaIss { get; set; }
        public int DestacaIpi { get; set; }
        public int? Csosn { get; set; }
        public int? Desoneracao { get; set; }
        public string CodBenificio { get; set; }        
        public decimal CreditoIcms { get; set; }
        public decimal IcmsDifenciado { get; set; }
        public bool PossuiIcmsDifenciado { get; set; }
        public int SubsTributaria { get; set; }
        public decimal PercSubsTributaria { get; set; }
        public string Cst { get; set; }
        public string EnquadraPis { get; set; }
        public string EnquadraCofins { get; set; }
        public string cstipi { get; set; }
        public string enquadraipi { get; set; }
        public decimal PercDiferimento { get; set; }
        public decimal PercDiferimentoFcp { get; set; }
        public int IntegraIpiIcms { get; set; }
        public int DescontaIcmsBase { get; set; }
        public decimal PercIpi { get; set; }
        public bool Ativo { get; set; }
    }
}
