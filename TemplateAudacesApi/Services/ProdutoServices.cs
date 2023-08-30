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

        private ProdutoDetalheRepository _produtoDetalheRepository;
        private ProdutoDetalheRepository produtoDetalheRepository
        {
            get
            {
                if (_produtoDetalheRepository == null)
                    _produtoDetalheRepository = new ProdutoDetalheRepository();

                return _produtoDetalheRepository;
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




        private ColaboradorRepository _fornecedorRepository;
        private ColaboradorRepository fornecedorRepository
        {
            get
            {
                if (_fornecedorRepository == null)
                    _fornecedorRepository = new ColaboradorRepository();

                return _fornecedorRepository;
            }
        }


        public Produto RetornarProduto(Garment garment, ref string referencia, ref string descricao)
        {
            const string erro = "Referência no formato 00000-DESCRICAO, Não declarado";
            var produto = new Produto();
            referencia = string.Empty;
            try
            {
                var variant = garment.variants[0];
                if (string.IsNullOrEmpty(variant.description))
                    throw new Exception(erro);

                var vetProduto = variant.description.Split("-");

                if (vetProduto != null && vetProduto.Length == 2)
                {
                    referencia = vetProduto[0].Trim();
                    descricao = vetProduto[1].Trim();
                    produto = produtoRepository.GetByReferencia(referencia);
                }
                else
                    throw new Exception(erro);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return produto;

        }


        public Object GetByReferencia(string referencia)
        {
            try
            {
                var produto = produtoRepository.GetByReferencia(referencia);
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

        public List<Object> GetProdutosByGrupo(string referencia,string descricao)
        {
            List<Object> lstGrupo = new List<Object>();
            try
            {
                var lstProduto = produtoRepository.GetListPorFiltros(-1, referencia, descricao, "");
                if (lstProduto != null && lstProduto.Any())
                {
                    foreach (var produto in lstProduto)
                    {

                        var grupo = RetornarGrupoDoProduto(produto);
                        lstGrupo.Add(grupo);

                    }

                }
            }

            catch (Exception ex)
            {

                throw ex;
            }
            return lstGrupo;

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

        private Object RetornarGrupoDoProduto(Produto produto)
        {
            var grupo = new Groups()
            {
                type = "group",
                uid = produto.Referencia,
                name = produto.DescricaoAlternativa,
                description = produto.Descricao,
                reference = produto.Referencia,
                last_modified = Convert.ToString(produto.DataAlteracao)
              };
            return grupo;

        }

        private Material RetornarMaterial(Produto produto)
        {
            try
            {

            var unidadeDeMedida = UniMedidaRepository.GetById(produto.IdUniMedida);
            var grupoDeProduto = GrupoProdutoRepository.GetById(produto.IdGrupo);
            var precoService = new PrecoService();
            var fornecedor = Utils.RetornarFornecedorDoProduto(produto);
            var detalhe = produtoDetalheRepository.GetListViewByProduto(produto.Id, 1);
            ICollection<Color> colors = RetornarCores(detalhe);
            ICollection<Size> sizes = RetornarTamanhos(detalhe);
            decimal preco = precoService.RetornarPrecoDoMaterial(produto);

            return new Material
            {
                type = "raw_material",
                uid = produto.Referencia,
                name = produto.Descricao,
                reference = produto.Referencia,
                description = (!string.IsNullOrEmpty(produto.DescricaoAlternativa)) ? produto.DescricaoAlternativa : produto.Descricao,
                value = Convert.ToDouble(preco),
                measure_unit = unidadeDeMedida?.Id + "-" + unidadeDeMedida?.Descricao,
                product_group = grupoDeProduto?.Id + "-" + grupoDeProduto?.Descricao,
                supplier = (fornecedor != null) ? fornecedor?.Id + "-" + fornecedor?.RazaoSocial : "",
                notes = produto.Obs,
                last_modified = produto.DataAlteracao.ToShortDateString(),
                variants = RetornarVariants(produto, detalhe, preco),
                //colors = colors,
                //sizes = sizes
            };

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }




        private List<Variant> RetornarVariantsDeProdutoAcabado(Produto produto,
                                                               decimal preco,
                                                               List<FichaTecnicaDoMaterialRelacao> itensDaFichaRelacao)
        {
            List<Variant> variants = null;
            var FichaTecnicaService = new FichaTecnicaService();



            try
            {
                if (itensDaFichaRelacao != null && itensDaFichaRelacao.Any())
                {
                    variants = new List<Variant>();
                    foreach (var relacao in itensDaFichaRelacao)
                    {

                        var corDoProduto = Utils.RetornarCor(relacao.Cor_Produto_Id);
                        var tamanhoDoProduto = Utils.RetornarTamanho(relacao.Tamanho_Produto_Id);

                        string IdCorEDescricaoETamanho = string.Format("{0}-{1}-{2}", corDoProduto.Id, corDoProduto.Descricao, tamanhoDoProduto.Descricao);

                        string IdTamanhoDescricao = String.Format("{0}-{1}", tamanhoDoProduto.Id, tamanhoDoProduto.Descricao.Trim());

                        Variant variant = CarregarVariante(produto, preco, relacao, IdCorEDescricaoETamanho, IdTamanhoDescricao, corDoProduto);

                        var itensCorETamanho = itensDaFichaRelacao.Where(p => p.cor_materiaprima_Id == relacao.cor_materiaprima_Id &&
                                                    p.Tamanho_Materiaprima_Id == relacao.Tamanho_Materiaprima_Id);


                        if (itensCorETamanho != null && itensCorETamanho.Any()
                                && !variants.Any(p => p.label == IdCorEDescricaoETamanho))
                        {
                            var itens = FichaTecnicaService.RetornarItensDoMaterial(itensCorETamanho.ToList());

                            variant.items.AddRange(itens);

                        }

                        if (!variants.Any(p => p.name == IdCorEDescricaoETamanho))
                        {
                            variants.Add(variant);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return variants;

        }


        private Variant CarregarVariante(Produto produto, decimal preco,
                                          FichaTecnicaDoMaterialRelacao det,
                                          string IdCorEDescricao,
                                          string IdTamanhoDescricao, 
                                          Cor cor)
        {
            var variant = new Variant()
            {

                name = IdCorEDescricao,
                description = string.Format("{0}-{1}", produto.Referencia, produto.Descricao),
                notes = RetornarObservacao(produto),
                
                label = cor.Id + "-" + cor.Descricao,
                author = RetornarAutor(produto),
                color = new Color()
                {
                    description = cor.Descricao,
                    value = IdCorEDescricao,
                    uid = IdCorEDescricao,
                },

                size = IdTamanhoDescricao,



            };
            return variant;
        }


        private List<Variant> RetornarVariantsDeProdutoAcabado(Produto produto,
                                                              IEnumerable<ProdutoDetalheView> detalhe,
                                                              decimal preco,
                                                              List<FichaTecnicaDoMaterialRelacao> itensDaFichaRelacao,
                                                              List<FichaTecnicaDoMaterialItem> fichaTecnicaDoMaterialItems
                                                              )
        {
            
            List<Variant> variants = null;
            var FichaTecnicaService = new FichaTecnicaService();
            try
            {
                variants = new List<Variant>();
                if (fichaTecnicaDoMaterialItems != null && fichaTecnicaDoMaterialItems.Any())
                {
                    foreach (var det in fichaTecnicaDoMaterialItems)
                    {
                        var itensDaRelacao = (from obj in itensDaFichaRelacao where obj.FichaTecnicaItemId == det.Id select obj).ToList();


                        foreach (var relacao in itensDaRelacao)
                        {
                            var corDaMateriaPrima = Utils.RetornarCor(relacao.cor_materiaprima_Id);
                            var tamanhoMateria = Utils.RetornarTamanho(relacao.Tamanho_Materiaprima_Id);
                            Cor corDoProduto = new Cor();
                            Tamanho tamanhoProduto = new Tamanho();
                            corDoProduto = Utils.RetornarCor(relacao.Cor_Produto_Id);
                            tamanhoProduto = Utils.RetornarTamanho(relacao.Tamanho_Produto_Id);
                            var cor = new Color()
                            {
                                description = corDoProduto.Descricao,
                                value = corDoProduto.Descricao,
                                uid = corDoProduto.Id.ToString()
                            };

                            var produtoItem = produtoRepository.GetById(det.MateriaPrimaId);
                            var variant = new Variant();

                            variant.name = corDaMateriaPrima.Id + "-" + corDaMateriaPrima.Descricao + tamanhoMateria.Descricao;
                            variant.label = corDaMateriaPrima.Id + "-" + corDaMateriaPrima.Descricao + tamanhoMateria.Descricao;
                                variant.color = cor;
                            variant.size = tamanhoMateria.Descricao;
                            if (!variants.Any(p => p.label == variant.label))
                            {
                                variants.Add(variant);
                               
                            }

                            
                        }



                        //var color = new Color { description = corDaMateriaPrima.Descricao, uid = corDaMateriaPrima.Id.ToString(), value = corDaMateriaPrima.Descricao };

                        //foreach (var item in lst)
                        //{
                        //    var variant = new Variant();


                        //    variant.notes = RetornarObservacao(produto);
                        //    variant.description = string.Format("{0}-{1}", det.getMateriaPrima.Referencia, det.getMateriaPrima.Descricao);
                        //    variant.author = RetornarAutor(produto);
                        //    variant.value = det.preco;

                        //    variant.name = corDoProduto.Id + "-" + corDoProduto.Descricao + "-" + tamanhoProduto.Descricao; 
                        //    variant.label = corDoProduto.Id + "-" + corDoProduto.Descricao + "-" + tamanhoProduto.Descricao; 
                        //    variant.color = new Color()
                        //    {
                        //        description = corDoProduto.Descricao,
                        //        value = corDoProduto.Descricao,
                        //        uid = corDoProduto.Id.ToString()
                        //    };
                        //    variant.size = tamanhoProduto.Id + "-" + tamanhoProduto.Descricao;
                        //    variant.items = new List<Item>();
                        //    var lstItem = new List<Item>();
                        //    foreach (var r in item.Relacao)
                        //    {                              


                        //        variant.items.Add(relacao);
                        //    }

                        //    variants.Add(variant);
                        //}








                        //var items = new List<Item>();
                        //foreach (var relacao in itensDaRelacao)
                        //{

                        //    var variant = new Variant();
                        //    Cor corDaMateriaPrima = new Cor();
                        //    Cor corDoProduto = new Cor();
                        //    Tamanho tamanhoProduto = new Tamanho();
                        //    Tamanho tamanhoMateria = new Tamanho();

                        //    corDaMateriaPrima = Utils.RetornarCor(relacao.cor_materiaprima_Id);
                        //    corDoProduto = Utils.RetornarCor(relacao.Cor_Produto_Id);
                        //    tamanhoProduto = Utils.RetornarTamanho(relacao.Tamanho_Produto_Id);
                        //    tamanhoMateria = Utils.RetornarTamanho(relacao.Tamanho_Materiaprima_Id);


                        //    variant.name = corDoProduto.Id + "-" + corDoProduto.Descricao;
                        //    variant.label = corDaMateriaPrima.Id + "-" + corDaMateriaPrima.Descricao;
                        //    variant.color = new Color()
                        //    {
                        //        description = corDoProduto.Descricao,
                        //        value = corDoProduto.Descricao,
                        //        uid = corDoProduto.Id.ToString()
                        //    };
                        //    variant.size = tamanhoProduto.Id + "-" + tamanhoProduto.Descricao;

                        //    ///item
                        //    var item = new Item();

                        //    var produtoItem = produtoRepository.GetById(relacao.MateriaPrimaId);
                        //    var fornecedor = Utils.RetornarFornecedorDoProduto(produtoItem);
                        //    UniMedida uniMedida = Utils.RetornarUnidade(produtoItem.IdUniMedida);
                        //    if (produtoItem != null)
                        //    {
                        //        var grupo = Utils.RetornarGrupo(produtoItem.IdGrupo);
                        //        item.type = "raw_material";
                        //        item.reference = produtoItem.Referencia;
                        //        item.uid = produtoItem.Referencia;
                        //        item.description = produtoItem.Descricao;
                        //        item.name = produtoItem.Descricao;
                        //        item.last_modified = produtoItem.DataAlteracao.ToString();
                        //        //item.date_register = produtoItem.DataCadastro.ToString();
                        //        item.value = produtoItem.PrecoVenda.ToString();
                        //        item.product_group = grupo?.Id + "-" + grupo?.Descricao;
                        //        item.supplier = fornecedor?.Id + "-" + fornecedor?.RazaoSocial;
                        //        item.measure_unit = uniMedida?.Id + "-" + uniMedida?.Descricao;
                        //        //item.Tamanho = item.measure_unit;



                        //        //if (!item.colors.Any(p=> p.uid == corMateriaPrima.Id.ToString()))
                        //        //     item.colors.Add(new Color { description = corMateriaPrima.Descricao, uid = corMateriaPrima.Id.ToString(), value = corMateriaPrima.Descricao });

                        //        var color = new Color { description = corDaMateriaPrima.Descricao, uid = corDaMateriaPrima.Id.ToString(), value = corDaMateriaPrima.Descricao };


                        //        var tamanho = new Size { uid = tamanhoMateria.Id.ToString(), value = tamanhoMateria.Descricao };

                        //        //if (!item.sizes.Any(p => p.uid == tamanhoMateriaPrima.Id.ToString()))
                        //        //     item.sizes.Add(new Size {  uid = tamanhoMateriaPrima.Id.ToString(), value = tamanhoMateriaPrima.Descricao });

                        //        item.notes = produtoItem.Obs;
                        //        var colecao = Utils.RetornarColecao(Convert.ToInt32(produtoItem?.IdColecao));
                        //        //item.collection = colecao?.Descricao;
                        //        var variant1 = new Variant()
                        //        {
                        //            name = corDaMateriaPrima.Id + "-" + corDaMateriaPrima.Descricao,
                        //            label = corDaMateriaPrima.Id + "-" + corDaMateriaPrima.Descricao,
                        //            color = color,
                        //            size = tamanhoMateria.Descricao

                        //        };

                        //        item.variants.Add(variant1);


                        //    }
                        //    items.Add(item);
                        //    //string IdCorEDescricaoETamanho = string.Format("{0}-{1}-{2}", det.Idcor, det.DescCor,det.DescTamanho);
                        //    //string IdTamanhoDescricao = String.Format("{0}-{1}", det.IdTamanho, det.DescTamanho.Trim());



                        //    //Variant variant = CarregarVariante(produto, preco, det, IdCorEDescricaoETamanho, IdTamanhoDescricao);

                        //    // var itensCorETamanho = itensDaFichaRelacao.Where(p => p.cor_materiaprima_Id == det.Idcor && 
                        //    // p.Tamanho_Materiaprima_Id == det.IdTamanho);




                        //    //if (itensCorETamanho != null && itensCorETamanho.Any()
                        //    //        && !variants.Any(p=>  p.label ==IdCorEDescricaoETamanho))
                        //    //{
                        //    //               var itens = FichaTecnicaService.RetornarItensDoMaterial(itensCorETamanho.ToList());

                        //    //    variant.items.AddRange(itens);
                        //    //    variants.Add(variant);
                        //    //} vV



                        //}
                        //variant.items.AddRange(items);
                        //variants.Add(variant);
                    }


                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return variants;

        }

        private Variant CarregarVariante(Produto produto, decimal preco,
                                            ProdutoDetalheView det, 
                                            string IdCorEDescricaoTamanho,
                                            string IdTamanhoDescricao)
        {
            var variant = new Variant()
            {

                name = IdCorEDescricaoTamanho ,
                //description = string.Format("{0}-{1}", produto.Referencia, produto.Descricao),
                notes = RetornarObservacao(produto),

                value = (det.custo > 0) ? det.custo : preco, // VER COM ALEX
                label = IdCorEDescricaoTamanho ,
                author = RetornarAutor(produto),
                color = new Color()
                {
                    description = det.DescCor,
                    value = det.DescCor,
                    uid = det.DescCor,
                },

                size = IdTamanhoDescricao,
                


            };
            return variant;
        }

        private List<Variant> RetornarVariants(Produto produto, IEnumerable<ProdutoDetalheView> detalhe, decimal preco)
        {
            List<Variant> variants = null;

            try
            {
                if (detalhe != null && detalhe.Any())
                {

                    variants = new List<Variant>();
                    foreach (var det in detalhe)
                    {

                        string IdCorEDescricaoETamanho = string.Format("{0}-{1}-{2}", det.Idcor, det.DescCor, det.DescTamanho);
                        string IdTamanhoDescricao = String.Format("{0}-{1}", det.IdTamanho, det.DescTamanho.Trim());
                        Variant variant = CarregarVariante(produto, preco, det, IdCorEDescricaoETamanho, IdTamanhoDescricao);

                        if (!variants.Any(p => p.name == IdCorEDescricaoETamanho))
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

        private List<Color> RetornarCores(IEnumerable<ProdutoDetalheView> detalhe)
        {
            List<Color> colors = new List<Color>();
            if (detalhe != null && detalhe.Any())
            {

                foreach (var det in detalhe)
                {
                    var cor = new Color();
                    cor.uid = string.Format("{0}-{1}", det.Idcor, det.DescCor);
                    cor.description = cor.uid;

                    if (!colors.Any(p => p.uid == cor.uid))
                        colors.Add(cor);

                }
            }
            return colors;
        }
        private List<Size> RetornarTamanhos(IEnumerable<ProdutoDetalheView> detalhe)
        {
            List<Size> sizes = new List<Size>();
            if (detalhe != null && detalhe.Any())
            {

                foreach (var det in detalhe)
                {

                    var size = new Size();
                    size.uid = string.Format("{0}-{1}", det.IdTamanho, det.DescTamanho);
                    size.value = size.uid;

                    if (!sizes.Any(p => p.uid == size.uid.ToString()))
                        sizes.Add(size);

                }
            }

            return sizes;
        }

        private Garment RetornarGrupo(GrupProduto grupo)
        {
            var item = new Garment()
            {
                type = "group",
                uid = grupo.Id.ToString(),
                name = grupo.Descricao,
                reference = grupo.Abreviatura,
                description = grupo.Descricao,
            };
            return item;
        }
        private Garment RetornarProdutoAcabado(Produto produto)
        {

            FichaTecnicaDoMaterialItemRepository  fichaTecnicaDoMaterialItemRepository = new FichaTecnicaDoMaterialItemRepository();
            var FichaTecnicaDoMaterialRepository = new FichaTecnicaDoMaterialRepository();
            var precoService = new PrecoService();
            var grupoDeProduto = GrupoProdutoRepository.GetById(2);//VER COM ALEX;
            var unidadeDeMedida = UniMedidaRepository.GetById(produto.IdUniMedida);
            var detalhe = produtoDetalheRepository.GetListViewByProduto(produto.Id, 1);
            var fornecedor = Utils.RetornarFornecedorDoProduto(produto);
            var FichaTecnicaService = new FichaTecnicaService();
           // var fichaTecnica = FichaTecnicaService.RetornarFichaTecnica(produto);
            var fichaTecnicaMaterial = FichaTecnicaDoMaterialRepository.GetByProduto(produto.Id);
            var itensDaFichaDoProduto = fichaTecnicaDoMaterialItemRepository.GetAllViewByFichaTecnica(fichaTecnicaMaterial.Id);
            var itensFichaRelacao = FichaTecnicaService.RetornarItensDaFichaRelacao(fichaTecnicaMaterial);
          

            Colecao colecao = null;
            if (produto.IdColecao.HasValue)
                colecao = Utils.RetornarColecao(produto.IdColecao.Value);

            
            ICollection<Color> colors = RetornarCores(detalhe);
            ICollection<Size> sizes = RetornarTamanhos(detalhe);

           
            decimal PrecoVenda = precoService.RetornarPrecoDeVendaProdutoAcabado(produto);

            var item = new Garment()
            {
                type = "finished_product",
                name = produto.Referencia,
                uid = produto.Referencia,
                reference = produto.Referencia,
                description = (!string.IsNullOrEmpty(produto.DescricaoAlternativa)) ? produto.DescricaoAlternativa : produto.Referencia + "-" + produto.Descricao,
                value = Convert.ToDouble(fichaTecnicaMaterial.Total),
                measure_unit = unidadeDeMedida?.Id + "-" + unidadeDeMedida?.Descricao,
                product_group = grupoDeProduto?.Id + "-" + grupoDeProduto?.Descricao,
                notes = RetornarObservacao(produto),
                supplier = (fornecedor != null) ? fornecedor?.Id + "-" + fornecedor?.RazaoSocial : "",
                last_modified = produto.DataAlteracao.ToShortDateString(),
                //colors = colors,
                //sizes = sizes,
                collection = (colecao != null) ? colecao?.Descricao : "",
                author = RetornarAutor(produto),
                responsible = RetornarResponsavel(produto),
                variants = RetornarVariantsDeProdutoAcabado(produto, PrecoVenda, itensFichaRelacao)

            };


            return item;
        }


        private string RetornarObservacao(Produto produto)
        {
            var obs = string.Empty;
            if (produto.Obs.Contains(";"))
            {
                var vetObs = produto.Obs.Split(";");

                obs = vetObs[0];
            }
            return obs;
        }
        private string RetornarResponsavel(Produto produto)
        {
            var obs = string.Empty;
            if (produto.Obs.Contains("responsavel"))
            {
                short indice = 0;
                var vetObs = produto.Obs.Split(";");
                if (vetObs.Length > 0)
                {
                    for (short i = 0; i < vetObs.Length; i++)
                    {

                        if (vetObs[i].Contains("responsavel"))
                            indice = i;
                    }

                }
                var vetResponsavel = vetObs[indice].Split("responsavel:");

                obs = vetResponsavel[1];
            }
            return obs;

        }
        private string RetornarAutor(Produto produto)
        {
            var obs = string.Empty;
            if (produto.Obs.Contains("autor"))
            {

                short indice = 0;
                var vetObs = produto.Obs.Split(";");
                if (vetObs.Length > 0)
                {
                    for (short i = 0; i < vetObs.Length; i++)
                    {

                        if (vetObs[i].Contains("autor"))
                            indice = i;
                    }

                }


                var vetAutor = vetObs[indice].Split("autor:");
                obs = vetAutor[1];
            }
            return obs;

        }

        private Item RetornarProdutoItem(Produto produto)
        {
            var precoService = new PrecoService();
            var grupoDeProduto = GrupoProdutoRepository.GetById(produto.IdGrupo);
            var unidadeDeMedida = UniMedidaRepository.GetById(produto.IdUniMedida);
            var detalhe = produtoDetalheRepository.GetListViewByProduto(produto.Id, 1);


            var fornecedor = Utils.RetornarFornecedorDoProduto(produto);

            ICollection<Color> colors = RetornarCores(detalhe);
            ICollection<Size> sizes = RetornarTamanhos(detalhe);

            decimal PrecoVenda = (produto.TipoItem == 1) ? precoService.RetornarPrecoDeVendaProdutoAcabado(produto) : precoService.RetornarPrecoDoMaterial(produto);

            var item = new Item()
            {
                type = produto.TipoItem == 0 ? "finished_product" : "raw_material",
                uid = produto.Referencia,
                name = produto.Descricao,
                reference = produto.Referencia,
                description = (!string.IsNullOrEmpty(produto.DescricaoAlternativa)) ? produto.DescricaoAlternativa : produto.Descricao,
                value = PrecoVenda.ToString(),
                measure_unit = unidadeDeMedida?.Id + "-" + unidadeDeMedida?.Descricao,
                product_group = grupoDeProduto?.Id + "-" + grupoDeProduto?.Descricao,
                notes = produto.Obs,
                supplier = fornecedor?.Id + "-" + fornecedor?.RazaoSocial,
                last_modified = produto.DataAlteracao.ToShortDateString(),
                colors = colors,
                sizes = sizes,

            };
            return item;
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
        public IEnumerable<Object> GetListMaterialPorFiltros(int tipoItem, string referencia, string descricao, string grupo, string fornecedor)
        {
            List<Object> lstMaterial = new List<Object>();
            var lstProduto = produtoRepository.GetListMaterialPorFiltros(tipoItem, referencia, descricao, grupo, fornecedor);

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
