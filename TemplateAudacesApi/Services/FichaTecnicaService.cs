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
            FichaTecnicaDoMaterial fichaVestilo = null;
            Produto produto = null;
            Colaborador fornecedor = null;
            string referencia = string.Empty;
            string descricao = string.Empty;
            try
            {
                produtoRepository.BeginTransaction();
                //retorna pela referencia o produto
                produto = new ProdutoServices().RetornarProduto(garment, ref referencia,ref descricao);

                if (produto == null || produto.Id == 0)
                {
                    //inclui um novo produto,detalhe, fornecedor caso nao tenha
                    produto = produtoInclusaoService.IncluirProdutoAcabado(garment, ref fornecedor, referencia,descricao);
                }
                else
                {
                    //altera o produto acabado
                    produto = produtoInclusaoService.AlterarProdutoAcabado(garment, produto,descricao);
                }

                fichaVestilo = RetornarFichaTecnica(produto);

                if (fichaVestilo != null && fichaVestilo.Id > 0)
                {
                    AlterarFicha(garment, fichaVestilo, produto);
                }
                else
                {
                    fichaVestilo = IncluirNovaFicha(garment, produto);
                }
                produtoRepository.CommitTransaction();
            }
            catch (Exception ex)
            {
                ExcluirRelacaoQuandoDaProblema(produto);
                produtoRepository.RollbackTransaction();
                throw ex;
            }
            return fichaVestilo;
        }

        public FichaTecnicaDoMaterial RetornarFichaTecnica(Produto produto)
        {
            //retorno a ficha pelo produto
            return FichaTecnicaDoMaterialRepository.GetByProduto(Convert.ToInt32(produto.Id));
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



        private List<FichaTecnicaDoMaterialItem> IncluirItensFichaTecnica(Garment garment,
                                         FichaTecnicaDoMaterial fichaTecnicaDoMaterial,
                                         Produto produto)
        {
            List<FichaTecnicaDoMaterialItem> lstFichaTecnicaMaterialItem = new List<FichaTecnicaDoMaterialItem>();

            try
            {

                ComposicaoService.ExcluirGradeDeProduto(produto);
                foreach (var variant in garment.variants)
                {
                    short sequencia = 1;
                    foreach (var material in variant.materials)
                    {
                        Produto produtoMaterial = null;
                        ProdutoFornecedorPreco produtoFornecedorPreco = null;

                        produtoMaterial = produtoRepository.GetByReferencia(material.uid);//peguei o item do material pelo codigo

                        //if (produtoMaterial != null && produtoMaterial.Id > 0)
                        //ComposicaoService.ExcluirFornecedoresDoProduto(produtoMaterial);//excluo a relacao de fornecedor e produto caso tenha

                        if (produtoMaterial == null || produtoMaterial.Id == 0)
                            //inclui o item
                            produtoMaterial = produtoInclusaoService.IncluirProdutoMaterial(material);

                        string cor = RetornarCor(variant, material);
                        string tamanho = RetornarTamanho(variant, material);

                        ComposicaoService.GravarGradeDeProduto(produto, cor, tamanho);
                       
                        //relaciona o produto, fornecedor, tamanho e cor e a grade do produto
                        produtoFornecedorPreco = ComposicaoService.GravarItemDaComposicao(material.supplier, produtoMaterial, cor,
                                                                                 tamanho,
                                                                                 Convert.ToDecimal(material.cost), false);//incluo a relacao de produto e fornecedor*/






                        //vai montando a ficha
                        FichaTecnicaDoMaterialItem fichaTecnicaDoMaterialItem = IncluirFichaTecnicaMaterialItem(produto,
                            fichaTecnicaDoMaterial,
                            produtoMaterial,
                            produtoFornecedorPreco,
                            material.amount,
                            material.cost,
                            sequencia
                            );
                        sequencia += 1;

                        lstFichaTecnicaMaterialItem.Add(fichaTecnicaDoMaterialItem);


                    }
                }
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
            if (material.color != null &&  material.color.GetType().FullName == "System.Text.Json.JsonElement")
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
            FichaTecnicaDoMaterial fichaTecnicaDoMaterial,
            Produto produtoMaterial,
            ProdutoFornecedorPreco produtoFornecedorPreco,
            decimal quantidade,
            decimal precoDoItem,
            short sequencia)
        {
            var itemFichaTecnicaDoMaterial = new FichaTecnicaDoMaterialItem();
            itemFichaTecnicaDoMaterial.FichaTecnicaId = fichaTecnicaDoMaterial.Id;
            itemFichaTecnicaDoMaterial.MateriaPrimaId = produtoMaterial.Id;
            itemFichaTecnicaDoMaterial.preco = precoDoItem;
            itemFichaTecnicaDoMaterial.DestinoId = 1;
            itemFichaTecnicaDoMaterial.idFornecedor = produtoFornecedorPreco.IdFornecedor;
            itemFichaTecnicaDoMaterial.quantidade = quantidade;
            itemFichaTecnicaDoMaterial.sequencia = sequencia;

            fichaTecnicaDoMaterialRepositoryItem.Save(ref itemFichaTecnicaDoMaterial);

            var fichatecnicaRelacaoRepository = new FichaTecnicaDoMaterialRelacaoRepository();
            var fichatecnicarRelacao = new FichaTecnicaDoMaterialRelacao();

            fichatecnicarRelacao.FichaTecnicaItemId = itemFichaTecnicaDoMaterial.Id;
            fichatecnicarRelacao.FichaTecnicaId = fichaTecnicaDoMaterial.Id;
            fichatecnicarRelacao.ProdutoId = produtoHeader.Id;
            fichatecnicarRelacao.MateriaPrimaId = produtoMaterial.Id;
            fichatecnicarRelacao.Cor_Produto_Id = Convert.ToInt32(produtoFornecedorPreco.IdCor);
            fichatecnicarRelacao.cor_materiaprima_Id = Convert.ToInt32(produtoFornecedorPreco.IdCor);
            fichatecnicarRelacao.Tamanho_Materiaprima_Id = Convert.ToInt32(produtoFornecedorPreco.IdTamanho);
            fichatecnicarRelacao.Tamanho_Produto_Id = Convert.ToInt32(produtoFornecedorPreco.IdTamanho); ;

            fichatecnicaRelacaoRepository.Save(ref fichatecnicarRelacao);

            return itemFichaTecnicaDoMaterial;
        }

        private FichaTecnicaDoMaterial IncluirNovaFicha(Garment garment, Produto produto)
        {
            var ficha = new FichaTecnicaDoMaterial();
            List<FichaTecnicaDoMaterialItem> lstFichaTecnicaMaterialItem = new List<FichaTecnicaDoMaterialItem>();
            try
            {
                ficha.EmpresaId = 1;
                ficha.ProdutoId = produto.Id;
                ficha.Ativo = true;
                ficha.DataAlteracao = Convert.ToDateTime(garment.last_modified);
                ficha.Observacao = produto.Obs;
                ficha.UserId = 1;
                ficha.Total = Convert.ToDecimal(garment.variants.Sum(p => p.value));
                ficha.possuiquebra = false;

                FichaTecnicaDoMaterialRepository.Save(ref ficha); //salvei a ficha

                //inclui itens da ficha
                if (garment.variants != null && garment.variants.Any() && garment.variants[0].materials != null && garment.variants[0].materials.Any())
                {
                    lstFichaTecnicaMaterialItem = IncluirItensFichaTecnica(garment, ficha, produto);
                }

            }

            catch (Exception)
            {

                throw;
            }
            return ficha;
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
                    lstFichaTecnicaMaterialItem = IncluirItensFichaTecnica(garment, fichaVestilo, produto);

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


        public List<FichaTecnicaDoMaterialRelacao> RetornarItensDaFichaRelacao(Produto produto)
        {
            var fichatecnicaRelacaoRepository = new FichaTecnicaDoMaterialRelacaoRepository();
           
            var fichatecnica = RetornarFichaTecnica(produto);
            if (fichatecnica != null)
                return fichatecnicaRelacaoRepository.GetAllViewByFichaTecnica(fichatecnica.Id).ToList();
            else
                throw new Exception($"Produto:{produto.Referencia}, não possui ficha tecnica");
          
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
                item.value  = produtoItem.PrecoVenda.ToString();
                item.product_group = grupo?.Id + "-" + grupo?.Descricao;
                item.supplier = fornecedor?.Id + "-" + fornecedor?.RazaoSocial;
                item.measure_unit = uniMedida?.Id + "-" + uniMedida?.Descricao;
                item.Tamanho = item.measure_unit;
                item.notes = produtoItem.Obs;
                item.collection = produtoItem.Colecao;
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
