using System;
using System.Collections.Generic;
using System.Linq;
using TemplateAudacesApi.Models;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

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
            
            var produto = new Produto();
            referencia = string.Empty;
            try
            {
                var variant = garment.variants[0];
                
                referencia = variant.Referencia;
                descricao = variant.description;
                produto = produtoRepository.GetByReferencia(referencia);


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
            foreach (var relacao in itensDaFichaRelacao.OrderBy(p => p.Cor_Produto_Id))
            {
                var corDoProduto = Utils.RetornarCor(relacao.Cor_Produto_Id);
                var tamanhoDoProduto = Utils.RetornarTamanho(relacao.Tamanho_Produto_Id);
                var IdCorETamanho = string.Format("{0}-{1}-{2}", corDoProduto.Id, corDoProduto.Descricao.Trim(), tamanhoDoProduto.Descricao.Trim());
                Variant variant = new Variant();
                
                variant.name = IdCorETamanho;
                //variant.value = itensFichaTecnicaDoMaterialItems.Sum(p=> p.valor);
                variant.description =  produto.Descricao;
                variant.notes = RetornarObservacao(produto);
                variant.label = corDoProduto.Id + "-" + corDoProduto.Descricao;
                // variant.author = RetornarAutor(produto);
                variant.Cor = new Color()
                {
                    type = "string",
                    description = corDoProduto.Descricao,
                    value = String.Format("{0}-{1}", corDoProduto.Id, corDoProduto.Descricao),
                    uid =  corDoProduto.Id.ToString(),
                };
                //variant.materials = null;
                variant.size = tamanhoDoProduto.Id + "-" + tamanhoDoProduto.Descricao.Trim();


                var itensDaRelacao = (from obj in itensDaFichaRelacao
                                      where obj.Cor_Produto_Id == corDoProduto.Id &&
                                            obj.Tamanho_Produto_Id == tamanhoDoProduto.Id
                                      select obj).ToList();

               
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

                        item.last_modified = produtoItem.DataAlteracao.ToString("s");
                     
                        decimal valor = (itemDaFicha.CustoCalculado > 0) ? Convert.ToDecimal(itemDaFicha.CustoCalculado) : Convert.ToDecimal(itemDaFicha.preco);
                        item.value = Convert.ToDecimal(valor);
                      
                        item.amount = itemDaFicha.quantidade;
                        
                        item.product_group = grupo?.Id + "-" + grupo?.Descricao;
                        item.supplier = fornecedor?.Id + "-" + fornecedor?.RazaoSocial;
                        item.measure_unit = uniMedida?.Id + "-" + uniMedida?.Descricao;
                       
                      
                        
                        var corMateriaPrima = Utils.RetornarCor(itemRelacao.cor_materiaprima_Id);
                        var color = new Color { description = corMateriaPrima.Descricao, uid = corMateriaPrima.Id.ToString(), value = corMateriaPrima.Id + "-" + corMateriaPrima.Descricao };
                        var tamanhoMateriaPrima = Utils.RetornarTamanho(itemRelacao.Tamanho_Materiaprima_Id);
                        
                        item.notes = produtoItem.Obs;
                        var colecao = Utils.RetornarColecao(Convert.ToInt32(produtoItem.IdColecao));
                        item.collection = colecao?.Descricao;
                        var variantMateria = new Variant();


                        variantMateria.name = corMateriaPrima.Id + "-" + corMateriaPrima.Descricao + "-" + tamanhoMateriaPrima.Descricao.Trim();
                            variantMateria.label = corMateriaPrima.Id + "-" + corMateriaPrima.Descricao + "-" + tamanhoMateriaPrima.Descricao.Trim();
                        variantMateria.value = item.value;
                        variantMateria.size = tamanhoMateriaPrima.Descricao;
                        variantMateria.Cor = color;
                        variantMateria.Color = color; 
                        
                       // item.Cor = corMateriaPrima.Id + "-" + corMateriaPrima.Descricao;
                        item.variants.Add(variantMateria);
                        item.name = produtoItem.Referencia + "|" + produtoItem.Descricao + "|" + variantMateria.name + "|";

                    }
                    if (!variant.items.Any(p => p.name == item.name))
                        variant.items.Add(item);
                }




                if (!variants.Any(p => p.name == variant.name))
                {
                    variants.Add(variant);
                }
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

                name = IdCorEDescricaoTamanho,
                //description = string.Format("{0}-{1}", produto.Referencia, produto.Descricao),
                notes = RetornarObservacao(produto),

                value = (det.custo > 0) ? det.custo : preco, // VER COM ALEX
                label = IdCorEDescricaoTamanho,
                Cor = new Color()
                {
                    description = det.DescCor,
                    value = string.Format("{0}-{1}", det.Id, det.DescCor),
                    uid = det.DescCor,
                },
                Color =  new Color()
                {
                    description = det.DescCor,
                    value = string.Format("{0}-{1}", det.Id, det.DescCor),
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
                   // size.uid = string.Format("{0}-{1}", det.IdTamanho, det.DescTamanho);
                    size.value = string.Format("{0}-{1}", det.IdTamanho, det.DescTamanho);

                    if (!sizes.Any(p => p.value == size.value.ToString()))
                        sizes.Add(size);

                }
            }

            return sizes;
        }
     
        private Garment RetornarProdutoAcabado(Produto produto)
        {


            FichaTecnicaDoMaterialItemRepository fichaTecnicaDoMaterialItemRepository = new FichaTecnicaDoMaterialItemRepository();
            var FichaTecnicaDoMaterialRepository = new FichaTecnicaDoMaterialRepository();
            var grupoDeProduto = GrupoProdutoRepository.GetById(2);//VER COM ALEX;
            var unidadeDeMedida = UniMedidaRepository.GetById(produto.IdUniMedida);
            var fornecedor = Utils.RetornarFornecedorDoProduto(produto);
            var FichaTecnicaService = new FichaTecnicaService();
            var fichaTecnicaMaterial = FichaTecnicaDoMaterialRepository.GetByProduto(produto.Id);


            if (fichaTecnicaMaterial != null && fichaTecnicaMaterial.Id > 0)
            {
                var itensDaFichaDoMaterialItem = fichaTecnicaDoMaterialItemRepository.GetAllViewByFichaTecnica(fichaTecnicaMaterial.Id).ToList();
                var itensFichaRelacao = FichaTecnicaService.RetornarItensDaFichaRelacao(fichaTecnicaMaterial);

                Destinos destinos = null;
                
                Colecao colecao = null;
                Segmento segmento = null;
                if (produto.IdColecao.HasValue)
                    colecao = Utils.RetornarColecao(produto.IdColecao.Value);

                if (produto.IdSegmento.HasValue)
                    segmento = Utils.RetornarSegmento(produto.IdSegmento.Value);


                if (produto.IdGrupo > 0)
                    grupoDeProduto = Utils.RetornarGrupo(produto.IdGrupo);

                if (itensDaFichaDoMaterialItem != null && itensDaFichaDoMaterialItem.Any())
                    destinos = Utils.RetornarDestino(itensDaFichaDoMaterialItem[0].DestinoId);
               

                decimal SomaTotalFicha = 0;
                var ProdutoAcabado = new Garment();
                
                CustomFields customFieldsColecoes = FichaModeloService.RetornarColecoesVestillo();
                if (colecao != null)
                customFieldsColecoes.value = colecao.Descricao;
                CustomFields customFieldsCores = FichaModeloService.RetornarCoresDoVestillo();
                CustomFields customFieldsTamanho = FichaModeloService.RetornarTamanhosVestillo();

                CustomFields customFieldSegmentos = FichaModeloService.RetornarSegmentosVestillo();

                if (segmento != null)
                customFieldSegmentos.value = String.Format("{0}-{1}", segmento.Id, segmento.Descricao);

                CustomFields customFieldGrupo = FichaModeloService.RetornarGrupoDeProdutoVestillo();
                customFieldGrupo.value = String.Format("{0}-{1}", grupoDeProduto.Id, grupoDeProduto.Descricao);

                CustomFields customFieldReferencia = FichaModeloService.RetornarCampoReferencia();
                customFieldReferencia.value = produto.Referencia;

                CustomFields customAnoVestillo = FichaModeloService.RetornarAnoVestillo();
                customAnoVestillo.value = produto.Ano.ToString();
                
                CustomFields customFieldDestino = FichaModeloService.RetornarDetinoDoVestillo();
                customFieldDestino.value = String.Format("{0}-{1}", destinos.Id, destinos.Descricao);


                
                ProdutoAcabado.Cor = new CustomFields();
                ProdutoAcabado.Cor = customFieldsCores;
           

                ProdutoAcabado.sizes = new Size();
                ProdutoAcabado.sizes.type = "string";
                ProdutoAcabado.sizes.editable = "true";
                ProdutoAcabado.sizes.options = customFieldsTamanho.options;

                //ProdutoAcabado.custom_fields.Add(customFieldsTamanho);
                ProdutoAcabado.custom_fields.Add(customFieldsColecoes);
                ProdutoAcabado.custom_fields.Add(customFieldSegmentos);
                ProdutoAcabado.custom_fields.Add(customFieldGrupo);
                ProdutoAcabado.custom_fields.Add(customFieldReferencia);
                ProdutoAcabado.custom_fields.Add(customAnoVestillo);
                ProdutoAcabado.custom_fields.Add(customFieldDestino);

                ProdutoAcabado.type = "finished_product";
                ProdutoAcabado.name = produto.Referencia;
                ProdutoAcabado.uid = produto.Referencia;
                ProdutoAcabado.reference = produto.Referencia;
                ProdutoAcabado.description = (!string.IsNullOrEmpty(produto.DescricaoAlternativa)) ? produto.DescricaoAlternativa : produto.Descricao;

                ProdutoAcabado.measure_unit = unidadeDeMedida?.Id + "-" + unidadeDeMedida?.Descricao;
              //  ProdutoAcabado.product_group = grupoDeProduto?.Id + "-" + grupoDeProduto?.Descricao;
                ProdutoAcabado.notes = RetornarObservacao(produto);
                ProdutoAcabado.supplier = (fornecedor != null) ? fornecedor?.Id + "-" + fornecedor?.RazaoSocial : "";
                ProdutoAcabado.last_modified = produto.DataAlteracao.ToString("s");

                //collection = (colecao != null) ? colecao?.Descricao : "",
                // author = RetornarAutor(produto),
                ProdutoAcabado.responsible = RetornarResponsavel(produto);
                ProdutoAcabado.variants = RetornarVariantsDeProdutoAcabado(produto, itensFichaRelacao, itensDaFichaDoMaterialItem, ref SomaTotalFicha);

               // ProdutoAcabado.value = Convert.ToDouble(itensDaFichaDoMaterialItem.Sum(p => p.valor));
               

                return ProdutoAcabado;
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
