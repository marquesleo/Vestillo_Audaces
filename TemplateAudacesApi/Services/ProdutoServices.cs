using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemplateAudacesApi.Models;
using Vestillo.Business;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service;

namespace TemplateAudacesApi.Services
{
    public class ProdutoServices
    {


        private IProdutoService _produtoService;
        private IProdutoService produtoService
        {
            get
            {
                if (_produtoService == null)
                    _produtoService = new ProdutoService().GetServiceFactory();

                return _produtoService;
            }
        }

        private ProdutoRepository _produtoRepository;
        private ProdutoRepository produtoRepository
        {
            get
            {
                if (_produtoRepository == null)
                    _produtoRepository = new ProdutoRepository();

                return _produtoRepository;
            }
        }

        private IProdutoDetalheService _produtoDetalheService;
        private IProdutoDetalheService produtoDetalheService
        {
            get
            {
                if (_produtoDetalheService == null)
                    _produtoDetalheService = new ProdutoDetalheService().GetServiceFactory();

                return _produtoDetalheService;
            }
        }

       
   
        private GrupProdutoRepository _GrupoProdutoRepository;
        private GrupProdutoRepository GrupoProdutoRepository
        {
            get
            {
                if (_GrupoProdutoRepository == null)
                    _GrupoProdutoRepository = new GrupProdutoRepository();

                return _GrupoProdutoRepository;
            }
        }

        private UniMedidaRepository _uniMedidaRepository;
        private UniMedidaRepository UniMedidaRepository
        {
            get
            {
                if (_uniMedidaRepository == null)
                    _uniMedidaRepository = new UniMedidaRepository();
                return _uniMedidaRepository;

            }
        }


        private ItemTabelaPrecoPCPRepository _itemTabelaPrecoPCPRepository;
        private ItemTabelaPrecoPCPRepository itemTabelaPrecoPCPRepository
        {
            get
            {
                if (_itemTabelaPrecoPCPRepository ==null)
                    _itemTabelaPrecoPCPRepository = new ItemTabelaPrecoPCPRepository();
                return _itemTabelaPrecoPCPRepository;
            }
        }

        private FornecedorRepository _fornecedorRepository;
        private FornecedorRepository fornecedorRepository
        {
            get
            {
                if (_fornecedorRepository == null)
                    _fornecedorRepository = new FornecedorRepository();

                return _fornecedorRepository;
            }
        }
        private ProdutoFornecedorPrecoRepository _produtoFornecedorPrecoRepository;
        private ProdutoFornecedorPrecoRepository produtoFornecedorPrecoRepository
        {
            get
            {
                if (_produtoFornecedorPrecoRepository == null)
                    _produtoFornecedorPrecoRepository = new ProdutoFornecedorPrecoRepository();
                return _produtoFornecedorPrecoRepository;
            }
        }

        public Object GetByReferencia(string referencia)
        {
            try
            {
                var produto = produtoService.GetByReferencia(referencia);
                if (produto != null)
                {
                    return RetornarProduto(produto);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return null;

        }

        public Object GetProdutosByGrupo(string descgrupo)
        {
            List<Object>  lstMaterial = new List<Object>();
            try
            {
                var lstProduto = produtoRepository.GetListGrupoDeProduto(descgrupo);
                if (lstProduto != null && lstProduto.Any())
                {
                    int codigoGrupo = lstProduto.First().IdGrupo;
                    var grupo = GrupoProdutoRepository.GetById(codigoGrupo);

                    if (grupo != null)
                    {
                        var cabecalho = RetornarGrupo(grupo);
                        foreach (var produto in lstProduto)
                        {
                            var item  = RetornarItem(produto);
                            cabecalho.items.Add(item);
                          
                        }
                        lstMaterial.Add(cabecalho);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return lstMaterial;

        }

        private Item RetornarItem(Produto produto)
        {
            return RetornarProdutoItem(produto);
        }
        private Object RetornarProduto(Produto produto)
        {
            if (produto.TipoItem == 0)
                return RetornarProdutoAcabado(produto);
            else
                return RetornarMaterial(produto);
        }

        private Material RetornarMaterial(Produto produto)
        {

            var unidadeDeMedida = UniMedidaRepository.GetById(produto.IdUniMedida);
            var grupoDeProduto = GrupoProdutoRepository.GetById(produto.IdGrupo);
            var FornecedorPreco = new ProdutoFornecedorPreco();
             produto.Fornecedor = produtoFornecedorPrecoRepository.GetListByProdutoFornecedor(produto.Id);
            if (produto.Fornecedor != null && produto.Fornecedor.Any())
            {
                FornecedorPreco = produto.Fornecedor.FirstOrDefault();
            }
            var fornecedor = fornecedorRepository.GetById(FornecedorPreco.IdFornecedor);
                    
            decimal preco = RetornarPrecoDoMaterial(produto);

            return new Material
            {
                type = "raw_material",
                uid = produto.Referencia,
                name =produto.Descricao,
                reference = produto.Referencia,
                description  = (!string.IsNullOrEmpty(produto.DescricaoAlternativa)) ? produto.DescricaoAlternativa : produto.Descricao,
                value        = Convert.ToDouble(preco),
                measure_unit = unidadeDeMedida?.Id + "-" + unidadeDeMedida?.Descricao,
                product_group = grupoDeProduto?.Id + "-" + grupoDeProduto?.Descricao,
                supplier =  fornecedor?.Id + "-" + fornecedor?.RazaoSocial,
                notes = produto.Obs,
                last_modified = produto.DataAlteracao.ToShortDateString(),
                variants = RetornarVariants(produto)

            };
        }

        private ICollection<Variant> RetornarVariants(Produto produto)
        {
            List<Variant> variants = null;
            try
            {
                var detalhe = produtoDetalheService.GetListViewByProduto(produto.Id, 1);
                if (detalhe != null && detalhe.Any())
                {
                    variants = new List<Variant>();
                    foreach (var det in detalhe)
                    {
                        var variant = new Variant()
                        {
                            name = det.Idcor.ToString(),
                            description = det.DescCor,
                            color = new Color()
                            {
                                description = det.DescCor,
                                value = det.AbvCor,
                                uid = det.Idcor.ToString()
                            },
                            size = det.DescTamanho,
                        };
                        variants.Add(variant);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return variants;

        }
        private Garment RetornarGrupo(GrupProduto grupo)
        {
            var item = new Garment()
            {
                type         = "group",
                uid          = grupo.Id.ToString(),
                name         = grupo.Descricao,
                reference    = grupo.Abreviatura,
                description  = grupo.Descricao,
             };
            return item;
        }
        private Garment RetornarProdutoAcabado(Produto produto)
        {

            var grupoDeProduto = GrupoProdutoRepository.GetById(produto.IdGrupo);
            var unidadeDeMedida = UniMedidaRepository.GetById(produto.IdUniMedida);
            var FornecedorPreco = new ProdutoFornecedorPreco();
            var detalhe = produtoDetalheService.GetListViewByProduto(produto.Id, 1);
            produto.Fornecedor = produtoFornecedorPrecoRepository.GetListByProdutoFornecedor(produto.Id);
            if (produto.Fornecedor != null && produto.Fornecedor.Any())
            {
                FornecedorPreco = produto.Fornecedor.FirstOrDefault();
            }
            var fornecedor = fornecedorRepository.GetById(FornecedorPreco.IdFornecedor);

            ICollection<Color> colors = new List<Color>();
            ICollection<Size> sizes = new List<Size>();
            if (detalhe !=null && detalhe.Any())
            {
                foreach (var det in detalhe)
                {
                    var cor = new Color();
                    var size = new Size();
                    cor.uid = det.Idcor.ToString();
                    cor.description = det.AbvCor;
                    cor.value = det.DescCor;
                    if (!colors.Any(p=> p.uid == det.Idcor.ToString()))
                        colors.Add(cor);
                    //size
                    size.uid = det.IdTamanho.ToString();
                    size.type = det.AbvTamanho;
                    
                    if (!sizes.Any(p=> p.uid == det.IdTamanho.ToString()))
                         sizes.Add(size);

                }
            }
            decimal PrecoVenda = RetornarPrecoDeVendaProdutoAcabado(produto); 
        
            var item = new Garment()
            {
                type         = "finished_product",
                uid          = produto.Referencia,
                name         = produto.Descricao,
                reference    = produto.Referencia,
                description  = (!string.IsNullOrEmpty(produto.DescricaoAlternativa)) ? produto.DescricaoAlternativa : produto.Descricao,
                value        = Convert.ToDouble(PrecoVenda),
                measure_unit = unidadeDeMedida?.Id + "-" + unidadeDeMedida?.Descricao,
                product_group = grupoDeProduto?.Id + "-" + grupoDeProduto?.Descricao,
                notes        = produto.Obs,
                supplier     =  fornecedor?.Id + "-" + fornecedor?.RazaoSocial,
                last_modified = produto.DataAlteracao.ToShortDateString(),
                colors = colors,
                sizes = sizes,

            };
            return item;
        }
        private decimal RetornarPrecoDeVendaProdutoAcabado(Produto produto)
        {
            var itemTabelaPreco = itemTabelaPrecoPCPRepository.GetAllByProduto(produto.Id);
            if (itemTabelaPreco != null && itemTabelaPreco.Any())
            {
                return itemTabelaPreco.First().PrecoVenda;
            }

            return 0;
        }
        private Item RetornarProdutoItem(Produto produto)
        {

            var grupoDeProduto = GrupoProdutoRepository.GetById(produto.IdGrupo);
            var unidadeDeMedida = UniMedidaRepository.GetById(produto.IdUniMedida);
            var FornecedorPreco = new ProdutoFornecedorPreco();
            var detalhe = produtoDetalheService.GetListViewByProduto(produto.Id, 1);
            produto.Fornecedor = produtoFornecedorPrecoRepository.GetListByProdutoFornecedor(produto.Id);

            if (produto.Fornecedor != null && produto.Fornecedor.Any())
            {
                FornecedorPreco = produto.Fornecedor.FirstOrDefault();
            }
            var fornecedor = fornecedorRepository.GetById(FornecedorPreco.IdFornecedor);

            ICollection<Color> colors = new List<Color>();
            ICollection<Size> sizes = new List<Size>();
            if (detalhe !=null && detalhe.Any())
            {
                foreach (var det in detalhe)
                {
                    var cor = new Color();
                    var size = new Size();
                    cor.uid = det.Idcor.ToString();
                    cor.description = det.AbvCor;
                    cor.value = det.DescCor;
                    colors.Add(cor);
                    //size
                    size.uid = det.IdTamanho.ToString();
                    size.type = det.AbvTamanho;
                    sizes.Add(size);

                }
            }
            decimal PrecoVenda =(produto.TipoItem==1) ?  RetornarPrecoDeVendaProdutoAcabado(produto): RetornarPrecoDoMaterial(produto);

            var item = new Item()
            {
                type         = produto.TipoItem == 0 ? "finished_product" : "raw_material",
                uid          = produto.Referencia,
                name         = produto.Descricao,
                reference    = produto.Referencia,
                description  = (!string.IsNullOrEmpty(produto.DescricaoAlternativa)) ? produto.DescricaoAlternativa : produto.Descricao,
                value        =  PrecoVenda.ToString(),
                measure_unit =  unidadeDeMedida?.Id + "-" + unidadeDeMedida?.Descricao,
                product_group = grupoDeProduto?.Id + "-" + grupoDeProduto?.Descricao,
                notes        =  produto.Obs,
                supplier     =  fornecedor?.Id + "-" + fornecedor?.RazaoSocial,
                last_modified = produto.DataAlteracao.ToShortDateString(),
                colors = colors,
                sizes = sizes,

            };
            return item;
        }

        private decimal RetornarPrecoDoMaterial(Produto Material)
        {
            decimal retorno = 0;
            IEnumerable<ProdutoFornecedorPreco> ret = new ProdutoFornecedorPrecoRepository().GetValoresSemInativos(Material.Id).ToList(); ;

            if (Material.TipoCalculoPreco == 2) //Pega a media
            {
                if (Material.TipoCustoFornecedor == 2)// Cor
                {
                    int count = ret.Where(x => x.PrecoCor > 0).ToList().Count();
                    if (count == 0) count = 1;
                    retorno = (ret.Sum(x => x.PrecoCor) / count);
                }
                else if (Material.TipoCustoFornecedor == 3)// Tamanho
                {
                    int count = ret.Where(x => x.PrecoTamanho > 0).ToList().Count();
                    if (count == 0) count = 1;
                    retorno = (ret.Sum(x => x.PrecoTamanho) / count);
                }
                else // Fornecedor
                {
                    int count = ret.Where(x => x.PrecoFornecedor > 0).ToList().Count();
                    if (count == 0) count = 1;
                    retorno = (ret.Sum(x => x.PrecoFornecedor) / count);
                }
            }
            else // Pega  o maior valor
            {
                if (Material.TipoCustoFornecedor == 2)// Cor
                    retorno = ret.Max(x => x.PrecoCor);
                else if (Material.TipoCustoFornecedor == 3)// Tamanho
                    retorno = ret.Max(x => x.PrecoTamanho);
                else if (ret.Any()) 
                    retorno = ret.Max(x => x.PrecoFornecedor);
            }
            return retorno;
        }
        public IEnumerable<Object> GetListPorFiltros(int tipoItem, string referencia, string descricao, string colecao)
        {
            List<Object> lstGarment = new List<Object>();
            var lstProduto = produtoRepository.GetListPorFiltros(tipoItem, referencia, descricao, colecao);
            if (lstProduto != null && lstProduto.Any())
            {
                foreach (var item in lstProduto)
                {
                    var garment = RetornarProduto(item);
                    lstGarment.Add(garment);
                }
            }
            return lstGarment;
        }
        public IEnumerable<Object> GetListMaterialPorFiltros(int tipoItem, string referencia, string descricao, string grupo,string fornecedor)
        {
            List<Object> lstMaterial = new List<Object>();
            var lstProduto = produtoRepository.GetListMaterialPorFiltros(tipoItem, referencia, descricao, grupo,fornecedor);
          
            if (lstProduto != null && lstProduto.Any())
            {
                foreach (var item in lstProduto)
                {
                    var material = RetornarProduto(item);
                    lstMaterial.Add(material);
                }
            }
            return lstMaterial;

        }
            
    }
}
