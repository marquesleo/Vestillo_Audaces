using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo;

namespace Vestillo.Business.Models
{
    [Vestillo.Tabela("ItensTabelaPrecoPCP", "ItensTabelaPrecoPCP")]
    public class ItemTabelaPrecoPCP
    {
        [Vestillo.Chave]
        public int Id { get; set; }
        public int TabelaPrecoPCPId { get; set; }
        public int ProdutoId { get; set; }
        [FiltroEmpresa]
        public int EmpresaId { get; set; }
        [NaoMapeado]
        public string ProdutoReferencia { get; set; }
        [NaoMapeado]
        public string ProdutoDescricao { get; set; }

        [NaoMapeado]
        public Produto Produto { get; set; }

        public decimal PrecoSugeridoPrevisto { get; set; }
        public decimal LucroPrevisto { get; set; }
        public decimal PrecoSugeridoRealizado { get; set; }
        public decimal LucroRealizado { get; set; }
        public decimal PrecoVenda { get; set; }
        public decimal LucroBaseCustoPrevisto { get; set; }
        public decimal PercentualLucroBaseCustoPrevisto { get; set; }
        public decimal LucroBaseCustoRealizado { get; set; }
        public decimal PercentualLucroBaseCustoRealizado { get; set; }
        
        public decimal CustoPrevisto { get; set; }
        public decimal CustoRealizado { get; set; }
        public decimal CustoDespesaPrevisto { get; set; }
        public decimal CustoDespesaRealizado { get; set; }
        public decimal CustoMaterial { get; set; }
        public decimal CustoMaoDeObra { get; set; }
    }
}
