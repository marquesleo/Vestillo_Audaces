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

        public List<Object> GetProdutosByGrupo(string referencia, string descricao)
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
                                                               List<FichaTecnicaDoMaterialRelacao> itensDaFichaRelacao,
                                                               List<FichaTecnicaDoMaterialItem> itensFichaTecnicaDoMaterialItems,
                                                               ref decimal SomaTotalFicha)
        {
            List<Variant> variants = null;
            var FichaTecnicaService = new FichaTecnicaService();
            variants = new List<Variant>();
      

                //////////////////////////////////////////////////////////
                 foreach (var relacao in itensDaFichaRelacao.OrderBy(p=> p.Cor_Produto_Id))
                 {
                     var corDoProduto = Utils.RetornarCor(relacao.Cor_Produto_Id);
                     var tamanhoDoProduto = Utils.RetornarTamanho(relacao.Tamanho_Produto_Id);
                     var IdCorETamanho = string.Format("{0}-{1}-{2}", corDoProduto.Id, corDoProduto.Descricao.Trim(), tamanhoDoProduto.Descricao.Trim());
                     Variant variant = new Variant();

                variant.name = IdCorETamanho;
                     if (produto.Descricao.Contains("-"))
                     {
                         produto.Descricao = produto.Descricao.Replace("-", null);
                     }
                     variant.description = string.Format("{0}-{1}", produto.Referencia, produto.Descricao);
                     variant.notes = RetornarObservacao(produto);
                     variant.label = corDoProduto.Id + "-" + corDoProduto.Descricao;
                     variant.author = RetornarAutor(produto);
                     variant.color = new Color()
                     {
                         description = corDoProduto.Descricao,
                         value = corDoProduto.Descricao,
                         uid = corDoProduto.Id.ToString(),
                     };

                     variant.size = tamanhoDoProduto.Id + "-" + tamanhoDoProduto.Descricao.Trim();


                var itensDaRelacao = (from obj in itensDaFichaRelacao
                                      where obj.Cor_Produto_Id == corDoProduto.Id &&
                                            obj.Tamanho_Produto_Id == tamanhoDoProduto.Id
                                               select obj).ToList();

                //var itensDaRelacao = (from obj in itensDaFichaRelacao
                //                 where obj.Cor_Produto_Id == corDoProduto.Id &&
                //                        obj.FichaTecnicaItemId == relacao.FichaTecnicaItemId
                //                 select obj).ToList();


                foreach (var itemRelacao in itensDaRelacao)
                     {
                         var item = new Item();
                         var produtoItem = Utils.RetornarProduto(itemRelacao.MateriaPrimaId);
                         var fornecedor = Utils.RetornarFornecedorDoProduto(produtoItem);
                         UniMedida uniMedida = Utils.RetornarUnidade(produtoItem.IdUniMedida);
                         if (produtoItem != null)
                         {
                             var grupo = Utils.RetornarGrupo(produtoItem.IdGrupo);

                             var itemDaFicha = (from obj in itensFichaTecnicaDoMaterialItems
                                                where obj.MateriaPrimaId == itemRelacao.MateriaPrimaId

                                                select obj).FirstOrDefault();

                             item.type = "raw_material";
                             item.reference = produtoItem.Referencia;
                             item.uid = produtoItem.Referencia;
                             item.description = produtoItem.Descricao;

                             item.last_modified = produtoItem.DataAlteracao.ToString();
                             item.date_register = produtoItem.DataCadastro.ToString();
                             item.value = (itemDaFicha.CustoCalculado > 0) ? Convert.ToDouble(itemDaFicha.CustoCalculado) : Convert.ToDouble(itemDaFicha.preco);
                             item.product_group = grupo?.Id + "-" + grupo?.Descricao;
                             item.supplier = fornecedor?.Id + "-" + fornecedor?.RazaoSocial;
                             item.measure_unit = uniMedida?.Id + "-" + uniMedida?.Descricao;

                             var corMateriaPrima = Utils.RetornarCor(itemRelacao.cor_materiaprima_Id);
                             var color = new Color { description = corMateriaPrima.Descricao, uid = corMateriaPrima.Id.ToString(), value = corMateriaPrima.Descricao };
                             var tamanhoMateriaPrima = Utils.RetornarTamanho(itemRelacao.Tamanho_Materiaprima_Id);

                             item.notes = produtoItem.Obs;
                             var colecao = Utils.RetornarColecao(Convert.ToInt32(produtoItem.IdColecao));
                             item.collection = colecao?.Descricao;
                             var variantMateria = new Variant()
                             {
                                 name = corMateriaPrima.Id + "-" + corMateriaPrima.Descricao + "-" + tamanhoMateriaPrima.Descricao,
                                 label = corMateriaPrima.Id + "-" + corMateriaPrima.Descricao + "-" + tamanhoMateriaPrima.Descricao,
                                 color = color,
                                 size = tamanhoMateriaPrima.Descricao
                                   
                             };

                             item.variants.Add(variantMateria);
                        item.name = produtoItem.Referencia + "|" + produtoItem.Descricao + "|" + variantMateria.name + "|";
                            
                         }
                    if (!variant.items.Any(p => p.name == item.name))
                             variant.items.Add(item);
                     }



                     variant.value = variant.items.Sum(p => p.value);

                     if (!variants.Any(p => p.name == variant.name))
                     {
                         variants.Add(variant);
                     }
                 }



            return variants;

        }

            /*var itensPraSomar = new List<Item>();

            foreach (var v in variants)
            {
                itensPraSomar.AddRange(v.items);
            }
            var SomaTotais = (from o in itensPraSomar
                              group o by new { o.reference } into g
                              select new
                              {
                                  materia_prima = g.Key.reference,
                                  Total = g.ToList().FirstOrDefault().value,

                              }).ToList();


            foreach (var t in SomaTotais)
            {
                SomaTotalFicha += Convert.ToDecimal(t.Total);
            }*/
           
        //}




    
        private Variant CarregarVariante(Produto produto, decimal preco,
                                            ProdutoDetalheView det,
                                            string IdCorEDescricaoTamanho,
                                            string IdTamanhoDescricao)
        {
            var variant = new Variant()
            {

                name = IdCorEDescricaoTamanho,
                //description = string.Format("{0}-{1}", produto.Referencia, produto.Descricao),
                notes = RetornarObservacao(produto),

                value = Convert.ToDouble( (det.custo > 0) ? det.custo : preco), // VER COM ALEX
                label = IdCorEDescricaoTamanho,
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

            FichaTecnicaDoMaterialItemRepository fichaTecnicaDoMaterialItemRepository = new FichaTecnicaDoMaterialItemRepository();
            var FichaTecnicaDoMaterialRepository = new FichaTecnicaDoMaterialRepository();
            var precoService = new PrecoService();
            var grupoDeProduto = GrupoProdutoRepository.GetById(2);//VER COM ALEX;
            var unidadeDeMedida = UniMedidaRepository.GetById(produto.IdUniMedida);
            var detalhe = produtoDetalheRepository.GetListViewByProduto(produto.Id, 1);
            var fornecedor = Utils.RetornarFornecedorDoProduto(produto);
            var FichaTecnicaService = new FichaTecnicaService();
            // var fichaTecnica = FichaTecnicaService.RetornarFichaTecnica(produto);
            var fichaTecnicaMaterial = FichaTecnicaDoMaterialRepository.GetByProduto(produto.Id);


            if (fichaTecnicaMaterial != null && fichaTecnicaMaterial.Id > 0)
            {
                var itensDaFichaDoMaterialItem = fichaTecnicaDoMaterialItemRepository.GetAllViewByFichaTecnica(fichaTecnicaMaterial.Id).ToList();
                var itensFichaRelacao = FichaTecnicaService.RetornarItensDaFichaRelacao(fichaTecnicaMaterial);


                Colecao colecao = null;
                if (produto.IdColecao.HasValue)
                    colecao = Utils.RetornarColecao(produto.IdColecao.Value);


                //ICollection<Color> colors = RetornarCores(detalhe);
                //ICollection<Size> sizes = RetornarTamanhos(detalhe);


                // decimal PrecoVenda = precoService.RetornarPrecoDeVendaProdutoAcabado(produto);
                decimal SomaTotalFicha = 0;
                var item = new Garment()
                {

                    type = "finished_product",
                    name = produto.Referencia,
                    uid = produto.Referencia,
                    reference = produto.Referencia,
                    description = (!string.IsNullOrEmpty(produto.DescricaoAlternativa)) ? produto.DescricaoAlternativa : produto.Referencia + "-" + produto.Descricao,
                   
                    measure_unit = unidadeDeMedida?.Id + "-" + unidadeDeMedida?.Descricao,
                    product_group = grupoDeProduto?.Id + "-" + grupoDeProduto?.Descricao,
                    notes = RetornarObservacao(produto),
                    supplier = (fornecedor != null) ? fornecedor?.Id + "-" + fornecedor?.RazaoSocial : "",
                    last_modified = produto.DataAlteracao.ToShortDateString(),

                    collection = (colecao != null) ? colecao?.Descricao : "",
                    author = RetornarAutor(produto),
                    responsible = RetornarResponsavel(produto),
                    variants = RetornarVariantsDeProdutoAcabado(produto,  itensFichaRelacao, itensDaFichaDoMaterialItem, ref SomaTotalFicha),


                };
            
                item.value = Convert.ToDouble(SomaTotalFicha);

                return item;
            }
            return null;
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

        public IEnumerable<Object> GetListPorFiltros(int tipoItem, string referencia, string descricao, string colecao)
        {
            List<Object> lstGarment = new List<Object>();
            var lstProduto = produtoRepository.GetListPorFiltros(tipoItem, referencia, descricao, colecao);
            if (lstProduto != null && lstProduto.Any())
            {
                foreach (var item in lstProduto)
                {
                    var garment = RetornarProduto(item);
                    if (garment != null)
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
