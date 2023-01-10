using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("Produtos","Itens")]
    public  class Produto
    {
        [Chave]

        public int Id { get; set; }
        public int? IdColecao { get; set; }
        public int? IdSegmento { get; set; }

        public int? IdCatalogo { get; set; }
        public int? IdEmtrega { get; set; }
        public int? IdItemVinculado { get; set; }

        public int IdAlmoxarifado { get; set; }
        public int IdUniMedida { get; set; }
        public int? IdSegUniMedida { get; set; }

        private int _IdEmpresa;

        [FiltroEmpresa]
        public int IdEmpresa { get
            {
                return _IdEmpresa;
            }
            set
            {
                _IdEmpresa = value;
            }

        }
        public int IdGrupo { get; set; }
        public int IdTipoproduto { get; set; }


        [Contador("Produto")]
        [RegistroUnico]
        public string Referencia { get; set; }
        public string Descricao { get; set; }
        public string DescricaoAlternativa { get; set; }
        public decimal FatorConversao { get; set; }
        public int TipoConversao { get; set; } // 0 = MULTIPLICAR 1 = DIVIDIR
        public bool Ativo { get; set; }
        public bool SomentePrecoVenda { get; set; }
        public string ReferenciaCli { get; set; }
        public int Ano { get; set; }
        public int Origem { get; set; }
        public int CsosnNfce { get; set; }
        public int SitTribNfce { get; set; }
        public decimal QtdPacote { get; set; }
        public decimal TempoPacote { get; set; }
        public decimal PesoLiquido { get; set; }
        public string Obs { get; set; }        
        public DateTime?  UltimaCompra  { get; set; } 
        public decimal UltimoPreco { get; set; }
        public DateTime? DataCusto { get; set; }
        public decimal MaioCusto { get; set; }
        public decimal PrecoCompra { get; set; }
        public decimal Ipi { get; set; }
        public decimal Icms { get; set; }
        public decimal Lucro { get; set; }
        public decimal Custo { get; set; }
        public decimal PrecoVenda { get; set; }
        public decimal PrecoAtacado { get; set; }
        public decimal PrecoPromocional { get; set; }
        public DateTime? InicioPromocao { get; set; }
        public DateTime? FimPromocao { get; set; }
        public decimal AliquotaNfce { get; set; }
        public string Ncm { get; set; }
        public string Fci { get; set; }
        public decimal ValorImportacao { get; set; }
        public decimal SaidaInter { get; set; }
        public decimal Ci { get; set; }
        public DateTime? DataCadastro { get; set; }
        public string CodBarrasUnico { get; set; }
        public int TipoItem { get; set; }
        public string  Cest { get; set; }
        public string  Draw { get; set; }
        public string RefFornecedor { get; set; }
        public int? TipoCalculoPreco { get; set; }
        public int TipoCustoFornecedor { get; set; }
        public decimal ValorMaterial { get; set; }
        public DateTime DataAlteracao { get; set; }
        public bool EnviarEcommerce { get; set; }
        public string CodigoBarrarEcommerce { get; set; }
        public string DescricaoMarketPlace { get; set; }
        [Vestillo.NaoMapeado]
        public string Colecao { get; set; }
        [Vestillo.NaoMapeado]
        public string Segmento { get; set; }
        [Vestillo.NaoMapeado]
        public string Grupo { get; set; }
        [Vestillo.NaoMapeado]
        public string UM { get; set; }


        [Vestillo.NaoMapeado]
        public string DescTipoItem
        {
            get
            {
                switch (TipoItem)
                {
                    case 0:
                        return "Produto";
                    case 1:
                        return "Matéria Prima";
                    case 2:
                        return "Ambos";
                    default:
                        return "Não Informado";
                }
            }
        }

        [Vestillo.NaoMapeado]
        public string PossuiFicha
        {
            get
            {
                switch (ValorMaterial)
                {
                    case 0:
                        return "Não";                
                    default:
                        return "Sim";
                }
            }
        }

        [NaoMapeado]
        public IEnumerable<ProdutoDetalhe > Grade { get; set; }

        [NaoMapeado]
        public IEnumerable<ProdutoFornecedorPreco> Fornecedor { get; set; }

        [NaoMapeado]
        public IEnumerable<Imagem> Imagem { get; set; }

        public override string ToString()
        {
            return "Id: " + Id + "  Descrição: " + Descricao;
        }

        [NaoMapeado]
        public decimal TempoOperacao { get; set; }

        [NaoMapeado]
        public decimal MaoDeObra { get; set; }

        [NaoMapeado]
        public decimal Despesa { get; set; }

        [NaoMapeado]
        public decimal Outros { get; set; }

        [NaoMapeado]
        public decimal Material { get; set; }

        public enum enuTipoItem
        {
            Produto = 0,
            MateriaPrima = 1,
            Ambos = 2
        }
    }
}
