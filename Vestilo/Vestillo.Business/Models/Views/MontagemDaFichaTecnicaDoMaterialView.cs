using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class MontagemDaFichaTecnicaDoMaterialView
    {

        public MontagemDaFichaTecnicaDoMaterialView(Produto produto, Produto materiaPrima, short sequencia, int idFornecedor)
        {
            atribuir(produto, materiaPrima, sequencia, idFornecedor);
        }

        private FichaTecnicaDoMaterialItem item;
        public FichaTecnicaDoMaterialItem getFichaDoMaterialItem
        {
            get
            {
                if (item == null)
                    item = new FichaTecnicaDoMaterialItem();
                return item;
            }
        }

        private void atribuir(Produto produto, Produto materiaPrima, short sequencia, int idFornecedor)
        {
            this.produto = produto;
            this.materiaPrima = materiaPrima;
            this.MateriaPrimaId = this.materiaPrima.Id;
            this._MateriaPrimaRef = this.materiaPrima.Referencia;
            this.percentual_custo = 100;
            this.Quantidade = 1;
            this.DestinoId = 1;
            this.Sequencia = sequencia;
            this.Custo = produtoService.GetServiceFactory().GetPrecoDeCustoDoProduto(this.materiaPrima);
            this.QuantidadeSegUniMedida = SetQuantidadeSegUniMedida();
            this.idFornecedor = idFornecedor;
        }

        public MontagemDaFichaTecnicaDoMaterialView(Produto produto, Produto materiaPrima, FichaTecnicaDoMaterialItem item)
        {
            atribuir(produto, materiaPrima, item.sequencia, item.idFornecedor);
            this.item = item;
            this.Quantidade = item.quantidade;
            this.percentual_custo = item.percentual_custo;
            this.DestinoId = item.DestinoId;
            this.Custo = item.preco;
            this.QuantidadeSegUniMedida = SetQuantidadeSegUniMedida();
            this.idFornecedor = item.idFornecedor;
        }



        private Vestillo.Business.Service.GrupProdutoService _grupoService;
        private Vestillo.Business.Service.GrupProdutoService grupoService
        {
            get
            {
                if (_grupoService == null)
                    _grupoService = new Service.GrupProdutoService();
                return _grupoService;
            }
        }

        private Vestillo.Business.Service.ProdutoService _produtoService;
        private Vestillo.Business.Service.ProdutoService produtoService
        {
            get
            {
                if (_produtoService == null)
                    _produtoService = new Service.ProdutoService();
                return _produtoService;
            }
        }

        private List<MontagemFichaTecnicaDoMaterialCorView> _lstMontagemFichaTecnicaDoMaterialCorView;
        public List<MontagemFichaTecnicaDoMaterialCorView> lstMontagemFichaTecnicaDoMaterialCorView
        {
            get
            {
                if (_lstMontagemFichaTecnicaDoMaterialCorView == null)
                    _lstMontagemFichaTecnicaDoMaterialCorView = new System.Collections.Generic.List<MontagemFichaTecnicaDoMaterialCorView>();
                return _lstMontagemFichaTecnicaDoMaterialCorView;
            }
            set
            {
                _lstMontagemFichaTecnicaDoMaterialCorView = value;
            }
        }

        private List<MontagemFichaTecnicaDoMaterialTamanhoView> _lstMontagemFichaTecnicaDoMaterialTamanhoView;
        public List<MontagemFichaTecnicaDoMaterialTamanhoView> lstMontagemFichaTecnicaDoMaterialTamanhoView
        {
            get
            {
                if (_lstMontagemFichaTecnicaDoMaterialTamanhoView == null)
                    _lstMontagemFichaTecnicaDoMaterialTamanhoView = new System.Collections.Generic.List<MontagemFichaTecnicaDoMaterialTamanhoView>();
                return _lstMontagemFichaTecnicaDoMaterialTamanhoView;
            }
            set
            {
                _lstMontagemFichaTecnicaDoMaterialTamanhoView = value;
            }
        }

        private Produto produto;
        private Produto materiaPrima;

        private FichaTecnicaDoMaterial _fichaTecnicaMaterial;
        public FichaTecnicaDoMaterial fichaTecnicaMaterial
        {
            get
            {
                if (_fichaTecnicaMaterial == null)
                    _fichaTecnicaMaterial = new FichaTecnicaDoMaterial();
                return _fichaTecnicaMaterial;
            }
            set
            {
                _fichaTecnicaMaterial = value;
            }
        }

        private string _MateriaPrimaRef = "Nulo";
        public string MateriaPrimaRef
        {
            get
            {
                if (_MateriaPrimaRef == "Nulo" || this.materiaPrima == null)
                {
                    this.materiaPrima = produtoService.GetServiceFactory().GetById(MateriaPrimaId);
                    _MateriaPrimaRef = this.materiaPrima.Referencia;
                }
                return _MateriaPrimaRef;
            }
        }

        public Produto getMateriaPrima
        {

            get
            {
                return this.materiaPrima;
            }
        }

        public Produto getProduto
        {
            get
            {
                return produto;
            }
        }

        private int _MateriaPrimaId;
        public int MateriaPrimaId
        {
            set
            {
                _MateriaPrimaId = value;
            }
            get
            {
                return _MateriaPrimaId;
            }
        }

        public short Sequencia { get; set; }

        public int DestinoId { get; set; }

        public bool Cor
        {
            get
            {
                return lstMontagemFichaTecnicaDoMaterialCorView.Any(p => p.CorDiferente);
            }
        }

        public bool Tamanho
        {
            get
            {
                return lstMontagemFichaTecnicaDoMaterialTamanhoView.Any(p => p.TamanhoDiferente);
            }
        }
        public decimal Custo { get; set; }
        public int idFornecedor { get; set; }

        public decimal Valor
        {
            get
            {
                if (percentual_custo == 100)
                {
                    return (Quantidade * Custo).ToRound(5);
                }
                else
                {
                    decimal perc_valor = (Custo * percentual_custo) / 100;
                    return (perc_valor * Quantidade).ToRound(5);
                }
            }
        }

        public string ProdutoRef
        {
            get
            {
                return produto.Referencia;
            }
        }

        public string ProdutoDescricao
        {
            get
            {
                return produto.Descricao;
            }
        }

        public string Modelo
        {
            get
            {
                return produto.Referencia + " - " + produto.Descricao;
            }
        }

        public decimal percentual_custo { get; set; }
        public decimal Quantidade { get; set; }

        private GrupProduto _grupo;
        private GrupProduto grupo
        {
            get
            {
                if (_grupo == null && produto.IdGrupo > 0)
                {
                    _grupo = grupoService.GetServiceFactory().GetById(produto.IdGrupo);
                }
                if (_grupo == null)
                    _grupo = new GrupProduto();

                return _grupo;
            }
        }

        public int UnidadeDeMedida
        {
            get
            {
                return materiaPrima.IdUniMedida;
            }
        }

        private GrupProduto _grupoMateria;
        private GrupProduto grupoMateria
        {
            get
            {
                if (_grupoMateria == null && materiaPrima.IdGrupo > 0)
                {
                    _grupoMateria = grupoService.GetServiceFactory().GetById(materiaPrima.IdGrupo);

                }
                if (_grupoMateria == null)
                    _grupoMateria = new GrupProduto();
                return _grupoMateria;
            }
        }

        public string GrupoDescricao
        {
            get
            {

                return grupo.Descricao;
            }
        }

        public string GrupoDescricaoMateriaPrima
        {
            get
            {
                return grupoMateria.Descricao;
            }
        }

        public int SegUniMedida
        {
            get
            {
                return Convert.ToInt32(materiaPrima.IdSegUniMedida);
            }
        }

        public decimal FatorConversaoUM
        {
            get
            {
                if (SegUniMedida > 0)
                    return materiaPrima.FatorConversao;
                else
                    return 0;
            }
        }

        public int TipoConversaoUM
        {
            get
            {
                if (SegUniMedida > 0)
                    return materiaPrima.TipoConversao;
                else
                    return 0;
            }
        }

        public decimal QuantidadeSegUniMedida { get;set;}

        public decimal SetQuantidadeSegUniMedida()
        {
            if (this.SegUniMedida > 0 && this.Quantidade > 0)
            {
                if (this.TipoConversaoUM == 0)
                    return this.Quantidade / this.FatorConversaoUM;
                else if (this.TipoConversaoUM == 1 && this.FatorConversaoUM > 0)
                    return this.Quantidade * this.FatorConversaoUM;
                else
                    return 0;
            }
            else
                return 0;
        }

        public bool MaterialQuebra { get; set; }
        public string QuebraManual { get; set; }
        public bool QuebraDestino { get; set; }

    }
}
