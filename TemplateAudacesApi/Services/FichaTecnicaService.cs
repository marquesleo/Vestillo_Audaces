using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using TemplateAudacesApi.Models;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service;

namespace TemplateAudacesApi.Services
{
    public class FichaTecnicaService
    {

        private GradeDeProdutoService _produtoDetalheService;
        private GradeDeProdutoService gradeDetalheService
        {
            get
            {
                if (_produtoDetalheService == null)
                    _produtoDetalheService = new GradeDeProdutoService();

                return _produtoDetalheService;
            }
        }

        private FichaTecnicaDoMaterialItemRepository _fichaTecnicaDoMaterialRepositoryItem;
        private FichaTecnicaDoMaterialItemRepository fichaTecnicaDoMaterialRepositoryItem
        {
            get
            {
                if (_fichaTecnicaDoMaterialRepositoryItem == null)
                    _fichaTecnicaDoMaterialRepositoryItem = new FichaTecnicaDoMaterialItemRepository();
                return _fichaTecnicaDoMaterialRepositoryItem;
            }
        }

        private FichaTecnicaDoMaterialRepository _fichaTecnicaDoMaterialRepository;
        private FichaTecnicaDoMaterialRepository fichaTecnicaDoMaterialRepository
        {
            get
            {
                if (_fichaTecnicaDoMaterialRepository == null)
                    _fichaTecnicaDoMaterialRepository = new FichaTecnicaDoMaterialRepository();
                return _fichaTecnicaDoMaterialRepository;
            }
        }

        private ComposicaoService _ComposicaoService;
        private ComposicaoService ComposicaoService
        {
            get
            {
                if (_ComposicaoService == null)
                    _ComposicaoService = new ComposicaoService();
                return _ComposicaoService;
            }
        }

        private FichaTecnicaRepository _FichaTecnicaRepository;
        private FichaTecnicaRepository FichaTecnicaRepository
        {
            get
            {
                if (_FichaTecnicaRepository == null)
                    _FichaTecnicaRepository = new FichaTecnicaRepository();
                return _FichaTecnicaRepository;
            }
        }


        private FichaTecnicaDoMaterialRepository _FichaTecnicaDoMaterialRepository;
        private FichaTecnicaDoMaterialRepository FichaTecnicaDoMaterialRepository
        {
            get
            {
                if (_FichaTecnicaDoMaterialRepository == null)
                    _FichaTecnicaDoMaterialRepository = new FichaTecnicaDoMaterialRepository();
                return _FichaTecnicaDoMaterialRepository;
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

        private ProdutoInclusaoService _produtoInclusaoService;
        private ProdutoInclusaoService produtoInclusaoService
        {
            get
            {
                if (_produtoInclusaoService == null)
                    _produtoInclusaoService = new ProdutoInclusaoService();
                return _produtoInclusaoService;
            }
        }





        /// <summary>
        /// Inclui a ficha tecnica pelo produto
        /// </summary>
        /// <param name="garment"></param>
        public FichaTecnicaDoMaterial Incluir(Garment garment)
        {
            FichaTecnicaDoMaterial fichaVestiloMaterial = null;
            FichaTecnica fichaTecnica = null;
            Produto produto = null;
            Colaborador fornecedor = null;
            string referencia = string.Empty;
            string descricao = string.Empty;
            try
            {
                produtoRepository.BeginTransaction();
                //retorna pela referencia o produto
                produto = new ProdutoServices().RetornarProduto(garment, ref referencia, ref descricao);

                if (produto == null || produto.Id == 0)
                {
                    //inclui um novo produto,detalhe, fornecedor caso nao tenha
                    produto = produtoInclusaoService.IncluirProdutoAcabado(garment, ref fornecedor, referencia, descricao);
                }
                else
                {
                    //altera o produto acabado
                    produto = produtoInclusaoService.AlterarProdutoAcabado(garment, produto, descricao);
                }

                fichaTecnica = RetornarFichaTecnica(produto);
                fichaVestiloMaterial = fichaTecnicaDoMaterialRepository.GetById(fichaTecnica.Id);


                if (fichaVestiloMaterial != null && fichaVestiloMaterial.Id > 0)
                {
                    AlterarFicha(garment, fichaVestiloMaterial, produto);
                }
                else
                {
                    fichaVestiloMaterial = PrepararParaInclusaoDeNovaFicha(garment, produto, fichaVestiloMaterial);
                }
                produtoRepository.CommitTransaction();
            }
            catch (Exception ex)
            {
                ExcluirRelacaoQuandoDaProblema(produto);
                produtoRepository.RollbackTransaction();
                throw ex;
            }
            return fichaVestiloMaterial;
        }

        public FichaTecnica RetornarFichaTecnica(Produto produto)
        {
            //retorno a ficha pelo produto
            var fichaTecnicaRepository = new FichaTecnicaRepository();
            return fichaTecnicaRepository.GetByProduto(Convert.ToInt32(produto.Id));
        }


        private void ExcluirRelacaoQuandoDaProblema(Produto produto)
        {
            try
            {
                var fichatecnicaRelacaoRepository = new FichaTecnicaDoMaterialRelacaoRepository();

                var ficha = RetornarFichaTecnica(produto);
                if (ficha != null && ficha.Id > 0)
                {
                    fichatecnicaRelacaoRepository.ExcluirRelacao(ficha.Id);
                    fichaTecnicaDoMaterialRepositoryItem.ExcluirRelacao(ficha.Id);
                    this.FichaTecnicaDoMaterialRepository.Delete(ficha.Id);
                }
                gradeDetalheService.ExcluirDetalhes(produto);

                produtoRepository.Delete(produto.Id);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        private void GravarFicha(Produto produtoAcabado,
                                 List<string> lstUid,
                                 Variant variant,
                                 FichaTecnicaDoMaterial fichaTecnicaDoMaterial,
                                 List<FichaTecnicaDoMaterialItem> lstFichaTecnicaMaterialItem,
                                 Cor corDoProduto,
                                 Tamanho tamanhoDoProduto,
                                 ref short sequencia)
        {
            Produto produtoMaterial = null;
            //foreach (var uid in lstUid)
            //  {
            produtoMaterial = produtoRepository.GetByReferencia(variant.materials[0].uid);
            var itemFichaTecnicaDoMaterial = new FichaTecnicaDoMaterialItem();
            itemFichaTecnicaDoMaterial.FichaTecnicaId = fichaTecnicaDoMaterial.Id;
            itemFichaTecnicaDoMaterial.MateriaPrimaId = produtoMaterial.Id;

            itemFichaTecnicaDoMaterial.DestinoId = 1;

            itemFichaTecnicaDoMaterial.quantidade = variant.materials.Sum(p => p.amount);

            itemFichaTecnicaDoMaterial.preco = variant.value * itemFichaTecnicaDoMaterial.quantidade;
            itemFichaTecnicaDoMaterial.sequencia = sequencia;

            fichaTecnicaDoMaterialRepositoryItem.Save(ref itemFichaTecnicaDoMaterial);


            foreach (var material in variant.materials)
            {
                //produtoMaterial = produtoRepository.GetByReferencia(material.uid);
                //var itemFichaTecnicaDoMaterial = new FichaTecnicaDoMaterialItem();
                //itemFichaTecnicaDoMaterial.FichaTecnicaId = fichaTecnicaDoMaterial.Id;
                //itemFichaTecnicaDoMaterial.MateriaPrimaId = produtoMaterial.Id;

                //itemFichaTecnicaDoMaterial.DestinoId = 1;

                //itemFichaTecnicaDoMaterial.quantidade = variant.materials.Sum(p => p.amount);

                //itemFichaTecnicaDoMaterial.preco = variant.value * itemFichaTecnicaDoMaterial.quantidade;
                //itemFichaTecnicaDoMaterial.sequencia = sequencia;

                //fichaTecnicaDoMaterialRepositoryItem.Save(ref itemFichaTecnicaDoMaterial);




                //if (produtoMaterial != null && produtoMaterial.Id > 0)
                //ComposicaoService.ExcluirFornecedoresDoProduto(produtoMaterial);//excluo a relacao de fornecedor e produto caso tenha

                if (produtoMaterial == null || produtoMaterial.Id == 0)
                    //inclui o item
                    produtoMaterial = produtoInclusaoService.IncluirProdutoMaterial(material);



                //ComposicaoService.GravarGradeDeProduto(produto, cor, tamanho);

                //relaciona o produto, fornecedor, tamanho e cor e a grade do produto
                //produtoFornecedorPreco = ComposicaoService.GravarItemDaComposicao(material.supplier, produtoMaterial, cor,
                //                                                         tamanho,
                //                                                         Convert.ToDecimal(material.cost), false);//incluo a relacao de produto e fornecedor*/


                /* 3814 - ACO - UNICO
                   3814 - BRANCO - UNICO
                   147  - ROXO - G 



                   26 - ACO -  UNICO   |   3814 - ACO - UNICO
                   26 - BRANCO - UNICO |   3814 - BRANCO - UNICO
                   26 - ROXO -  G      |   3814 - 3814 - ACO - UNICO



                 */


                //vai montando a ficha


                string descCor = RetornarCor(variant, material);
                string descTamanho = RetornarTamanho(variant, material);

                var corDoMaterial = Utils.RetornarCor(descCor);
                var tamanhoDoMaterial = Utils.RetornarTamanho(descTamanho);


                FichaTecnicaDoMaterialItem fichaTecnicaDoMaterialItem = IncluirFichaTecnicaMaterialItem(produtoAcabado,
                corDoProduto,
                tamanhoDoProduto,
                fichaTecnicaDoMaterial,
                itemFichaTecnicaDoMaterial,
                produtoMaterial,
                corDoMaterial,
                tamanhoDoMaterial
               );
                lstFichaTecnicaMaterialItem.Add(fichaTecnicaDoMaterialItem);





                //}
                sequencia += 1;
            }
        }




        private List<FichaTecnicaDoMaterialItem> IncluirItensFichaTecnica(Garment garment,
                                         FichaTecnicaDoMaterial fichaTecnicaDoMaterial,
                                         Produto produto,
                                         Variant variant,
                                         ref short sequencia)
        {
            List<FichaTecnicaDoMaterialItem> lstFichaTecnicaMaterialItem = new List<FichaTecnicaDoMaterialItem>();

            try
            {

                ComposicaoService.ExcluirGradeDeProduto(produto);
                // var variant = garment.variants[0];


                List<string> lstUid = new List<string>();
                Produto produtoMaterial = null;

                //peguei o item do material pelo codigo


                var itensRepetidos = variant.materials
                    .GroupBy(item => item.produto)
                    .Where(group => group.Count() > 1)
                    .Select(group => group.ToList()).ToList();

                var novaVariant = new Variant();
                novaVariant = variant.Clone(variant);
                novaVariant.materials.Clear();
                var materialRepetido = new Material();
                var listaDeUid = new List<string>();

                var CorDoProduto = new Cor();
                var tamanhoDoProduto = new Tamanho();

                Utils.RetornarCorETamanhoDoTexto(variant.name, ref CorDoProduto, ref tamanhoDoProduto);


                if (itensRepetidos != null && itensRepetidos.Any())
                {
                    materialRepetido = itensRepetidos.ToList()[0][0];
                    novaVariant.materials.Add(materialRepetido);
                    lstUid.Add(materialRepetido.uid);
                    GravarFicha(produto, lstUid, novaVariant, fichaTecnicaDoMaterial, lstFichaTecnicaMaterialItem, CorDoProduto, tamanhoDoProduto, ref sequencia);
                    var materials = variant.materials.Where(p => p.produto != materialRepetido.produto).ToList();
                    novaVariant.materials.AddRange(materials);

                }
                else
                {
                    novaVariant = variant;
                }
                lstUid = (from obj in novaVariant.materials select obj.uid).Distinct().ToList();

                GravarFicha(produto, lstUid, novaVariant, fichaTecnicaDoMaterial, lstFichaTecnicaMaterialItem, CorDoProduto, tamanhoDoProduto, ref sequencia);


                ComposicaoService.gradeDetalheService.lstDetalhe.Clear();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return lstFichaTecnicaMaterialItem;
        }

        private String RetornarCor(Variant variant, Material material)
        {
            if (material.color != null && material.color.GetType().FullName == "System.Text.Json.JsonElement")
            {
                if (!string.IsNullOrEmpty(material.color.ToString()))
                {
                    var vet = material.color.ToString().Split(":");
                    return vet[0].ToString();
                }


            }
            if (variant.custom_fields != null && variant.custom_fields.color != null)
            {
                return variant.custom_fields.color.value;
            }
            return "";
        }

        private String RetornarTamanho(Variant variant, Material material)
        {
            if (!string.IsNullOrEmpty(material.size))
                return material.size.ToString();

            if (!string.IsNullOrEmpty(variant.size))
            {
                return variant.size;
            }


            return "";
        }


        private FichaTecnicaDoMaterialItem IncluirFichaTecnicaMaterialItem(Produto produtoHeader,
            Cor corDoProduto,
            Tamanho tamanhoDoProduto,
            FichaTecnicaDoMaterial fichaTecnicaDoMaterial,
            FichaTecnicaDoMaterialItem itemFichaTecnicaDoMaterial,
            Produto produtoMaterial,
            Cor corDoMaterial,
            Tamanho tamanho
            )
        {


            var fichatecnicaRelacaoRepository = new FichaTecnicaDoMaterialRelacaoRepository();
            var fichatecnicarRelacao = new FichaTecnicaDoMaterialRelacao();

            fichatecnicarRelacao.FichaTecnicaItemId = itemFichaTecnicaDoMaterial.Id;
            fichatecnicarRelacao.FichaTecnicaId = fichaTecnicaDoMaterial.Id;
            fichatecnicarRelacao.ProdutoId = produtoHeader.Id;

            fichatecnicarRelacao.MateriaPrimaId = produtoMaterial.Id;

            fichatecnicarRelacao.Cor_Produto_Id = Convert.ToInt32(corDoProduto.Id);
            fichatecnicarRelacao.Tamanho_Produto_Id = Convert.ToInt32(tamanhoDoProduto.Id);


            fichatecnicarRelacao.cor_materiaprima_Id = Convert.ToInt32(corDoMaterial.Id);
            fichatecnicarRelacao.Tamanho_Materiaprima_Id = Convert.ToInt32(tamanho.Id);


            fichatecnicaRelacaoRepository.Save(ref fichatecnicarRelacao);

            return itemFichaTecnicaDoMaterial;
        }


        private FichaTecnicaDoMaterial GravarFichaTecnicaHeader(Garment garment, Produto produto)
        {
            var fichaTecnica = new FichaTecnica();
            var fichaDoMaterial = new FichaTecnicaDoMaterial();
            try
            {
                fichaDoMaterial.EmpresaId = Vestillo.Lib.Funcoes.GetIdEmpresaLogada;
                fichaDoMaterial.ProdutoId = produto.Id;
                fichaDoMaterial.Ativo = true;
                fichaDoMaterial.DataAlteracao = Convert.ToDateTime(garment.last_modified);
                fichaDoMaterial.Observacao = produto.Obs;
                fichaDoMaterial.UserId = 1;
                fichaDoMaterial.Total = Convert.ToDecimal(garment.variants.Sum(p => p.value));
                fichaDoMaterial.possuiquebra = false;

                fichaTecnica.ProdutoId = produto.Id;
                fichaTecnica.Observacao = produto.Obs;
                fichaTecnica.Ativo = true;
                fichaTecnica.EmpresaId = Vestillo.Lib.Funcoes.GetIdEmpresaLogada;

                FichaTecnicaRepository.Save(ref fichaTecnica);
                FichaTecnicaDoMaterialRepository.Save(ref fichaDoMaterial); //salvei a ficha
            }
            catch (Exception ex)
            {

                throw new Exception("Erro ao gravar GravarFichaTecnicaHeader " + ex.Message);
            }
            return fichaDoMaterial;

        }
        private FichaTecnicaDoMaterial PrepararParaInclusaoDeNovaFicha(Garment garment, 
                                    Produto produto, FichaTecnicaDoMaterial ficha)
        {
           
            List<FichaTecnicaDoMaterialItem> lstFichaTecnicaMaterialItem = new List<FichaTecnicaDoMaterialItem>();
            try
            {
                //inclui itens da ficha
                if (garment.variants != null && garment.variants.Any() && garment.variants[0].materials != null && garment.variants[0].materials.Any())
                {
                    if (ficha == null || ficha.Id==0)
                    ficha = GravarFichaTecnicaHeader(garment, produto);

                    short sequencia = 1;

                    List<Models.ItemPraGravar> lstItemPraGravar = RetornarPossiveisCombinacoes(garment);

                    List<Models.Ficha> lstFicha = RetornarListaASerGravada(ficha, out sequencia, lstItemPraGravar);

                    GravarFichaDeMaterialItemERelacao(produto, ficha, lstFichaTecnicaMaterialItem, lstFicha);

                }

            }

            catch (Exception ex)
            {

                throw ex;
            }
            return ficha;
        }

        private void GravarFichaDeMaterialItemERelacao(Produto produto, 
                                                        FichaTecnicaDoMaterial ficha, 
                                                        List<FichaTecnicaDoMaterialItem> lstFichaTecnicaMaterialItem,
                                                        List<Models.Ficha> lstFicha)
        {
            try
            {
                foreach (var itemFicha in lstFicha)
                {
                    var produtoMaterial = produtoRepository.GetByReferencia(itemFicha.uidMateriaPrima);
                    var itemFichaTecnicaDoMaterial = new FichaTecnicaDoMaterialItem();
                    itemFichaTecnicaDoMaterial.FichaTecnicaId = itemFicha.idFicha;
                    itemFichaTecnicaDoMaterial.MateriaPrimaId = produtoMaterial.Id;

                    itemFichaTecnicaDoMaterial.DestinoId = 1;

                    itemFichaTecnicaDoMaterial.quantidade = itemFicha.quantidade;

                    itemFichaTecnicaDoMaterial.preco = itemFicha.valor;
                    itemFichaTecnicaDoMaterial.sequencia = itemFicha.sequencia;

                    var corDoProduto = new Cor();
                    var tamanhoDoProduto = new Tamanho();

                    corDoProduto = Utils.RetornarCor(itemFicha.corProduto);


                    fichaTecnicaDoMaterialRepositoryItem.Save(ref itemFichaTecnicaDoMaterial);
                    foreach (var itemRelacao in itemFicha.Itens)
                    {


                        tamanhoDoProduto = Services.Utils.RetornarTamanho(itemRelacao.tamanhoProduto);

                        var corDoMaterial = Utils.RetornarCor(itemRelacao.descCorMateriaPrima);
                        var tamanhoDoMaterial = Utils.RetornarTamanho(itemRelacao.descTamanhoMateriaPrima);

                        FichaTecnicaDoMaterialItem fichaTecnicaDoMaterialItem =
                            IncluirFichaTecnicaMaterialItem(produto,
                        corDoProduto,
                        tamanhoDoProduto,
                        ficha,
                        itemFichaTecnicaDoMaterial,
                        produtoMaterial,
                        corDoMaterial,
                        tamanhoDoMaterial
                        );
                        lstFichaTecnicaMaterialItem.Add(fichaTecnicaDoMaterialItem);
                    }

                }
            }
            catch (Exception ex)
            {

                throw new Exception("Erro ao gravar ficha de material item e relacao " + ex.Message);
            }
        }

        private List<Models.Ficha> RetornarListaASerGravada(FichaTecnicaDoMaterial ficha, out short sequencia, List<Models.ItemPraGravar> lstItemPraGravar)
        {
            try
            {
                List<Models.Ficha> lstFicha = null;
                var lst = from p in lstItemPraGravar
                          group p by new { p.corProduto, p.uidMateriaPrima }
                             into g
                          orderby g.Key.corProduto
                          select new Models.Ficha
                          {
                              corProduto = g.Key.corProduto,
                              uidMateriaPrima = g.Key.uidMateriaPrima,
                              Itens = g.ToList()
                          };

                sequencia = 1;

                lstFicha = new List<Models.Ficha>();
                foreach (var item in lst)
                {
                    var NovaFicha = new Models.Ficha();
                    NovaFicha.idFicha = ficha.Id;
                    NovaFicha.corProduto = item.corProduto;
                    NovaFicha.uidMateriaPrima = item.uidMateriaPrima;
                    NovaFicha.sequencia = sequencia;
                                     
                    NovaFicha.quantidade = item.Itens.Sum(p => p.quantidade);
                    NovaFicha.valor = NovaFicha.quantidade * item.Itens.Sum(p => p.valor);
                    NovaFicha.Itens = item.Itens;
                    lstFicha.Add(NovaFicha);
                    sequencia += 1;
                }
                return lstFicha;
            }
            catch (Exception ex)
            {

                throw new Exception("Erro ao retornar lista a ser gravada RetornarListaASerGravada " + ex.Message);
            }
        }

        private List<Models.ItemPraGravar> RetornarPossiveisCombinacoes(Garment garment)
        {
            var lstItemPraGravar = new List<Models.ItemPraGravar>();
            try
            {
                foreach (var variant in garment.variants)
                {

                    foreach (var material in variant.materials)
                    {
                        var itemPraGravar = new Models.ItemPraGravar();

                        itemPraGravar.uidMateriaPrima = material.uid;
                        itemPraGravar.corProduto = variant.CorDaVariant;
                        itemPraGravar.tamanhoProduto = variant.TamanhoVariant;
                        itemPraGravar.quantidade = material.amount;
                        itemPraGravar.descCorMateriaPrima = RetornarCor(variant, material);
                        itemPraGravar.descTamanhoMateriaPrima = RetornarTamanho(variant, material);
                        itemPraGravar.valor = Convert.ToDecimal(material.total);
                        lstItemPraGravar.Add(itemPraGravar);


                    }

                }
            }
            catch (Exception ex)
            {

                throw new Exception("Erro ao retornar possiveis combinacoes RetornarPossiveisCombinacoes " + ex.Message);
            }

            return lstItemPraGravar;
        }

        private void AlterarFicha(Garment garment, FichaTecnicaDoMaterial fichaVestilo, Produto produto)
        {
            try
            {
                List<FichaTecnicaDoMaterialItem> lstFichaTecnicaMaterialItem = new List<FichaTecnicaDoMaterialItem>();
                fichaVestilo.DataAlteracao = Convert.ToDateTime(garment.last_modified);
                fichaVestilo.Observacao = produto.Obs;
                fichaVestilo.Total = 0;
                FichaTecnicaDoMaterialRepository.Save(ref fichaVestilo); //salvei a ficha

                fichaTecnicaDoMaterialRepositoryItem.ExcluirRelacao(fichaVestilo.Id);//exclui relacao
                var fichatecnicaRelacaoRepository = new FichaTecnicaDoMaterialRelacaoRepository();
                fichatecnicaRelacaoRepository.ExcluirRelacao(fichaVestilo.Id);

                if (garment.variants != null && garment.variants.Any())
                {
                    //inclui itens da ficha
                     PrepararParaInclusaoDeNovaFicha(garment,produto, fichaVestilo);

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<Material> RetornarItensDaFichaTecnicaDeMaterial(Produto produto)
        {
            List<Material> itens = new List<Material>();
            try
            {
                var fichaTecnica = FichaTecnicaDoMaterialRepository.GetByProduto(produto.Id);
                if (fichaTecnica != null)
                {
                    var precoService = new PrecoService();
                    var itensDaFicha = fichaTecnicaDoMaterialRepositoryItem.GetAllViewByFichaTecnica(fichaTecnica.Id);
                    if (itensDaFicha != null && itensDaFicha.Any())
                    {
                        foreach (var itemFicha in itensDaFicha)
                        {
                            RetornarItemDeMaterial(produto, itens, itemFicha);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return itens;
        }


        public List<FichaTecnicaDoMaterialRelacao> RetornarItensDaFichaRelacao(FichaTecnicaDoMaterial ficha)
        {
            var fichatecnicaRelacaoRepository = new FichaTecnicaDoMaterialRelacaoRepository();

          
            if (ficha != null)
                return fichatecnicaRelacaoRepository.GetAllViewByFichaTecnica(ficha.Id).ToList();
            else
                throw new Exception($"Produto:{ficha.ProdutoId}, não possui ficha tecnica");

        }

        //public List<Item> RetornarItensDaFichaTecnica(Produto produto, List<Color> colors, List<Size> size)
        //{
        //    List<Item> itens = new List<Item>();
        //    try
        //    {
        //        var fichaTecnica = FichaTecnicaDoMaterialRepository.GetByProduto(produto.Id);
        //        if (fichaTecnica != null)
        //        {
        //            var precoService = new PrecoService();
        //           var itensDaFicha = fichaTecnicaDoMaterialRepositoryItem.GetAllViewByProdutoId(produto.Id).ToList();
        //            if (itensDaFicha != null && itensDaFicha.Any())
        //            {
        //                foreach (var itemFicha in itensDaFicha)
        //                {
        //                    RetornarItem(produto, itens, itemFicha, colors, size);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return itens;
        //}


        public List<Item> RetornarItensDoMaterial(List<FichaTecnicaDoMaterialRelacao> itemDaRelacao)
        {
            var itens = new List<Item>();
            try
            {

                foreach (var item in itemDaRelacao)
                {
                    RetornarItem(itens, item);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return itens;
        }

        private void RetornarItem(List<Item> itens, FichaTecnicaDoMaterialRelacao itemFicha)
        {
            var item = new Item();
            var produtoItem = produtoRepository.GetById(itemFicha.MateriaPrimaId);
            var fornecedor = Utils.RetornarFornecedorDoProduto(produtoItem);
            UniMedida uniMedida = Utils.RetornarUnidade(produtoItem.IdUniMedida);
            if (produtoItem != null)
            {
                var grupo = Utils.RetornarGrupo(produtoItem.IdGrupo);
                item.type = "raw_material";
                item.reference = produtoItem.Referencia;
                item.uid = produtoItem.Referencia;
                item.description = produtoItem.Descricao;
                item.name = produtoItem.Descricao;
                item.last_modified = produtoItem.DataAlteracao.ToString();
                item.date_register = produtoItem.DataCadastro.ToString();
                item.value = produtoItem.PrecoVenda.ToString();
                item.product_group = grupo?.Id + "-" + grupo?.Descricao;
                item.supplier = fornecedor?.Id + "-" + fornecedor?.RazaoSocial;
                item.measure_unit = uniMedida?.Id + "-" + uniMedida?.Descricao;
                item.Tamanho = item.measure_unit;

                var corMateriaPrima = Utils.RetornarCor(itemFicha.cor_materiaprima_Id);

                //if (!item.colors.Any(p=> p.uid == corMateriaPrima.Id.ToString()))
                //     item.colors.Add(new Color { description = corMateriaPrima.Descricao, uid = corMateriaPrima.Id.ToString(), value = corMateriaPrima.Descricao });

                var color = new Color { description = corMateriaPrima.Descricao, uid = corMateriaPrima.Id.ToString(), value = corMateriaPrima.Descricao };

                var tamanhoMateriaPrima = Utils.RetornarTamanho(itemFicha.Tamanho_Materiaprima_Id);
                //  var tamanho = new Size { uid = tamanhoMateriaPrima.Id.ToString(), value = tamanhoMateriaPrima.Descricao };

                //if (!item.sizes.Any(p => p.uid == tamanhoMateriaPrima.Id.ToString()))
                //     item.sizes.Add(new Size {  uid = tamanhoMateriaPrima.Id.ToString(), value = tamanhoMateriaPrima.Descricao });

                item.notes = produtoItem.Obs;
                item.collection = produtoItem.Colecao;
                var variant = new Variant()
                {
                    name = corMateriaPrima.Id + "-" + corMateriaPrima.Descricao + "-" + tamanhoMateriaPrima.Descricao,
                    label = corMateriaPrima.Id + "-" + corMateriaPrima.Descricao + "-" + tamanhoMateriaPrima.Descricao,
                    color = color,
                    size = tamanhoMateriaPrima.Descricao

                };

                item.variants.Add(variant);
                itens.Add(item);
            }
        }


        private void RetornarItemDeMaterial(Produto produto, List<Material> itens, FichaTecnicaDoMaterialItem itemFicha)
        {
            var unidadeDeMedida = Utils.RetornarUnidade(produto.IdUniMedida);
            var grupoDeProduto = Utils.RetornarGrupo(produto.IdGrupo);
            var precoService = new PrecoService();
            var fornecedor = Utils.RetornarFornecedorDoProduto(produto);


            decimal preco = precoService.RetornarPrecoDoMaterial(produto);

            var material = new Material
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

            };
            itens.Add(material);

        }

    }
}
