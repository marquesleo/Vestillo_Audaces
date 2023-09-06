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

                var fichaTecnicaMaterial = FichaTecnicaDoMaterialRepository.GetByProduto(produto.Id);
                if (fichaTecnicaMaterial != null && fichaTecnicaMaterial.Id > 0)
                {
                    fichaVestiloMaterial = AlterarFicha(garment, fichaTecnicaMaterial, produto);
                }
                else
                {
                    fichaVestiloMaterial = PrepararParaInclusaoDeNovaFicha(garment, produto, fichaVestiloMaterial);
                }
                produtoRepository.CommitTransaction();
            }
            catch (Exception ex)
            {
                // ExcluirRelacaoQuandoDaProblema(produto);
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
            itemFichaTecnicaDoMaterial.percentual_custo = 100;
            itemFichaTecnicaDoMaterial.DestinoId = 1;

            itemFichaTecnicaDoMaterial.quantidade = variant.materials.Sum(p => p.amount);
            var total = Convert.ToDecimal(variant.value) * itemFichaTecnicaDoMaterial.quantidade;
            itemFichaTecnicaDoMaterial.preco = total;
            itemFichaTecnicaDoMaterial.valor = total;
            itemFichaTecnicaDoMaterial.sequencia = sequencia;

            fichaTecnicaDoMaterialRepositoryItem.Save(ref itemFichaTecnicaDoMaterial);


            foreach (var material in variant.materials)
            {


                if (produtoMaterial == null || produtoMaterial.Id == 0)

                    produtoMaterial = produtoInclusaoService.IncluirProdutoMaterial(material);






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

                sequencia += 1;
            }
        }




     

        private String RetornarCor(Variant variant, Material material)
        {
            if (material.color != null && material.color.GetType().FullName == "System.Text.Json.JsonElement")
            {
                if (!string.IsNullOrEmpty(material.color.ToString()))
                {
                    var vet = material.color.ToString().Split(":");
                    var codigo = 0;
                    if (int.TryParse(vet[0].ToString(),out codigo))
                    {
                        var cor =Utils.RetornarCor(codigo);
                        return cor.Descricao;
                    }else
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
                fichaDoMaterial.Total = Convert.ToDecimal(garment.variants[0].value);
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

           
            try
            {
                //inclui itens da ficha
                if (garment.variants != null && garment.variants.Any() && garment.variants[0].materials != null && garment.variants[0].materials.Any())
                {
                    bool inclusao = false;
                    if (ficha == null || ficha.Id == 0)
                    {
                        ficha = GravarFichaTecnicaHeader(garment, produto);
                        inclusao = true;

                    }   
                 

                     GravarFichaTecnica(garment, ficha, produto, inclusao);

                    //List<Models.Ficha> lstFicha = RetornarListaASerGravada(ficha, out sequencia, lstItemPraGravar);

                    //                    GravarFichaDeMaterialItemERelacao(produto, ficha, lstFichaTecnicaMaterialItem, lstFicha);

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
                fichaTecnicaDoMaterialRepositoryItem.ExcluirRelacao(ficha.Id);
                var fichatecnicaRelacaoRepository = new FichaTecnicaDoMaterialRelacaoRepository();
                fichatecnicaRelacaoRepository.ExcluirRelacao(ficha.Id);
                var lstMateriaPrimaGrava = new List<FichaTecnicaDoMaterialItem>();
                short sequencia = 1;
                int ultimoCodigoItemMateriaPrima = 0;

                foreach (var itemFicha in lstFicha.OrderBy(p => p.uidMateriaPrima))
                {
                    var produtoMaterial = produtoRepository.GetByReferencia(itemFicha.uidMateriaPrima);
                    var itemFichaTecnicaDoMaterial = new FichaTecnicaDoMaterialItem();
                    itemFichaTecnicaDoMaterial.FichaTecnicaId = itemFicha.idFicha;
                    itemFichaTecnicaDoMaterial.MateriaPrimaId = produtoMaterial.Id;

                    itemFichaTecnicaDoMaterial.DestinoId = 1;

                    itemFichaTecnicaDoMaterial.quantidade = itemFicha.quantidade;

                    itemFichaTecnicaDoMaterial.preco = itemFicha.valor;
                    itemFichaTecnicaDoMaterial.valor = itemFicha.valor;
                    itemFichaTecnicaDoMaterial.sequencia = sequencia;

                    var corDoProduto = new Cor();
                    var tamanhoDoProduto = new Tamanho();

                    corDoProduto = Utils.RetornarCor(itemFicha.corProduto);


                    if (!lstMateriaPrimaGrava.Any(p => p.MateriaPrimaId == produtoMaterial.Id))
                    {
                        fichaTecnicaDoMaterialRepositoryItem.Save(ref itemFichaTecnicaDoMaterial);
                        lstMateriaPrimaGrava.Add(itemFichaTecnicaDoMaterial);
                        ultimoCodigoItemMateriaPrima = itemFichaTecnicaDoMaterial.Id;
                        sequencia += 1;
                    }
                    foreach (var itemRelacao in itemFicha.Itens)
                    {

                        itemFichaTecnicaDoMaterial.Id = ultimoCodigoItemMateriaPrima;
                        tamanhoDoProduto = Utils.RetornarTamanho(itemRelacao.tamanhoProduto);

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
                              //tamanhoProduto = g.Key.tamanhoProduto,
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

                    NovaFicha.quantidade = lst.Count(p => p.uidMateriaPrima == item.uidMateriaPrima);
                    NovaFicha.valor = item.Itens.FirstOrDefault().valor;

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


        public class ItemQSeRepete
        {
            public string descricaoItem { get; set; }
            public int item { get; set; }
        }

        public class CorPraGravar
        {
            public string Cor { get; set; }
        }
        public class ItemDaFichaTecnica
        {
            public string Cor { get; set; }
            public string Tamanho { get; set; }
            public string IdMateriaPrima { get; set; }
            public string descricaoToda => IdMateriaPrima + "|" + Cor + "|" + Tamanho;
            public short sequencia { get; set; }
            public decimal quantidade { get; set; }
            public double valor { get; set; }
            public List<Models.ItemPraGravar> variacoes { get; set; } = new List<Models.ItemPraGravar>();
        }

        public class PossiveisVariacoes
        {
            public string corDoProduto { get; set; }
            public string corDaMateriaPrima { get; set; }
            public string tamanhoMateriaPrima { get; set; }
            public string tamanhoProduto { get; set; }
        }

        private void GravarFichaTecnica(Garment garment,FichaTecnicaDoMaterial ficha, 
                                                 Produto produto, 
                                                 bool inclusao)
        {
            var lstTodasVariantesFormadas = new List<Models.ItemPraGravar>();
            var item = 1;
            //carrego toda lista
            try
            {
                lstTodasVariantesFormadas = RetornarTodasVariantesPossiveis(garment);
                item = 0;

                var CoresESeusItens = (from o in lstTodasVariantesFormadas
                                       group o by new { o.corProduto } into g
                                       select new
                                       {
                                           cor_produto = g.Key.corProduto,
                                           Itens = g.ToList()

                                       });


                IncluirClasseDeProduto(produto, lstTodasVariantesFormadas);

                List<ItemDaFichaTecnica> lstItemFichaTecnica;
                List<CorPraGravar> lstCorPraGravar;
                RetornarItensParaGravarNaFichaTecnicaMaterialItemECores(garment, out item, out lstItemFichaTecnica, out lstCorPraGravar);



                //////////////// verifico aqui os itens q se repetem /////////////////////////////////
                var lstItemQueSeRepete = new List<ItemQSeRepete>();

                foreach (var s in CoresESeusItens)
                {
                    //vejo se ja tem alguma materia-prima repetida dentro de cada cor
                    var materiaRepetida = (from obj in s.Itens
                                           group obj by new { obj.descricaoToda, obj.item } into g
                                           select new
                                           {
                                               materia_prima_toda = g.Key.descricaoToda,
                                               item = g.Key.item,
                                               qtd = g.Count(),
                                               Itens = g.ToList()
                                           });

                    //se tiver
                    foreach (var m in materiaRepetida)
                    {
                        //tendo eu verifico se for mais de uma, e nao exista na lista de repetidas eu adiciono
                        if (m.qtd > 1 && !lstItemQueSeRepete.Any(p => p.descricaoItem == m.materia_prima_toda))
                        {

                            if (!lstItemFichaTecnica.Any(p => p.sequencia == m.item))
                            {
                                var itemQSeRepete = new ItemQSeRepete();
                                itemQSeRepete.item = m.item;
                                itemQSeRepete.descricaoItem = m.materia_prima_toda;
                                lstItemQueSeRepete.Add(itemQSeRepete);
                            }
                        }
                    }
                }





                // vejo o item q se repete e adiciono ele no restante do item
                AdicionarItensRepetidosAItensParaGravarNaFichaTecnicaDoMaterialItem(lstTodasVariantesFormadas, lstItemFichaTecnica, lstItemQueSeRepete);



                //// excluo a relacao toda do banco antes de incluir ou alterar
                var fichatecnicaRelacaoRepository = new FichaTecnicaDoMaterialRelacaoRepository();
                fichatecnicaRelacaoRepository.ExcluirRelacao(ficha.Id);
                fichaTecnicaDoMaterialRepositoryItem.ExcluirRelacao(ficha.Id);

                //aqui eu gravo a ficha

                foreach (var itemFicha in lstItemFichaTecnica)
                {
                    var produtoMaterial = produtoRepository.GetByReferencia(itemFicha.IdMateriaPrima);
                    var itemFichaTecnicaDoMaterial = new FichaTecnicaDoMaterialItem();
                    itemFichaTecnicaDoMaterial.FichaTecnicaId = ficha.Id;
                    itemFichaTecnicaDoMaterial.MateriaPrimaId = produtoMaterial.Id;

                    itemFichaTecnicaDoMaterial.DestinoId = 1;

                    itemFichaTecnicaDoMaterial.quantidade = itemFicha.quantidade;//tenho q ver;

                    itemFichaTecnicaDoMaterial.preco = Convert.ToDecimal(itemFicha.valor);
                    itemFichaTecnicaDoMaterial.valor = Convert.ToDecimal(itemFicha.valor);
                    itemFichaTecnicaDoMaterial.sequencia = itemFicha.sequencia;


                    fichaTecnicaDoMaterialRepositoryItem.Save(ref itemFichaTecnicaDoMaterial); //gravo a ficha tecnica material item


                    foreach (var minhaCor in lstCorPraGravar)
                    {
                        //retorno a combinacao daquele item
                        var combinacao = (from obj in lstTodasVariantesFormadas
                                          where
                                                     itemFicha.IdMateriaPrima == obj.uidMateriaPrima &&
                                                     minhaCor.Cor == obj.corProduto &&
                                                     itemFicha.descricaoToda == obj.descricaoToda
                                          select obj).ToList();

                        //se existem itens repetidos pode trazer mais itens, entao agrupo pela cor e tamanho 
                        var combinacaoPorTamanhoECor = (from o in combinacao
                                                        group o by new { o.corProduto, o.tamanhoProduto, o.descCorMateriaPrima, o.descTamanhoMateriaPrima } into g
                                                        select new
                                                        {
                                                            corProduto = g.Key.corProduto,
                                                            tamanhoProduto = g.Key.tamanhoProduto,
                                                            descCorMateriaPrima = g.Key.descCorMateriaPrima,
                                                            descTamanhoMateriaPrima = g.Key.descTamanhoMateriaPrima,
                                                            Itens = g.ToList()

                                                        });

                        var corDoProduto = new Cor();
                        var tamanhoDoProduto = new Tamanho();
                        corDoProduto = Utils.RetornarCor(minhaCor.Cor);

                        if (combinacaoPorTamanhoECor.Any())
                        {


                            var lstMateriaPrimaGrava = new List<FichaTecnicaDoMaterialItem>();


                            itemFichaTecnicaDoMaterial.FichaTecnicaId = ficha.Id;
                            itemFichaTecnicaDoMaterial.MateriaPrimaId = produtoMaterial.Id;

                            itemFichaTecnicaDoMaterial.DestinoId = 1;

                            itemFichaTecnicaDoMaterial.quantidade = itemFicha.quantidade;//tenho q ver;

                            itemFichaTecnicaDoMaterial.preco = Convert.ToDecimal(itemFicha.valor);
                            itemFichaTecnicaDoMaterial.valor = Convert.ToDecimal(itemFicha.valor);
                            itemFichaTecnicaDoMaterial.sequencia = itemFicha.sequencia;




                            //gravo a relacao
                            foreach (var itemRelacao in combinacaoPorTamanhoECor)
                            {
                                tamanhoDoProduto = Utils.RetornarTamanho(itemRelacao.tamanhoProduto);

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

                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {

                throw new Exception("Erro ao retornar possiveis combinacoes RetornarPossiveisCombinacoes " + ex.Message);
            }

           
        }

        private static void AdicionarItensRepetidosAItensParaGravarNaFichaTecnicaDoMaterialItem(List<Models.ItemPraGravar> lstTodasVariantesFormadas, List<ItemDaFichaTecnica> lstItemFichaTecnica, List<ItemQSeRepete> lstItemQueSeRepete)
        {
            if (lstItemQueSeRepete.Any())
            {
                foreach (var r in lstItemQueSeRepete)
                {
                    var itemRepetido = (from obj in lstItemFichaTecnica
                                        where obj.descricaoToda == r.descricaoItem
                                        select obj).FirstOrDefault();
                    if (itemRepetido != null)
                    {
                        var novoItem = new ItemDaFichaTecnica();
                        novoItem.Tamanho = itemRepetido.Tamanho;
                        novoItem.IdMateriaPrima = itemRepetido.IdMateriaPrima;
                        novoItem.Cor = itemRepetido.Cor;
                        novoItem.sequencia = Convert.ToInt16(lstItemFichaTecnica.Count + 1);
                        var itemComQtdEValor = (from obj in lstTodasVariantesFormadas
                                                where obj.item == r.item && r.descricaoItem == obj.descricaoToda
                                                select obj).FirstOrDefault();
                        novoItem.quantidade = itemComQtdEValor.quantidade;
                        novoItem.valor = Convert.ToDouble(itemComQtdEValor.valor);
                        lstItemFichaTecnica.Add(novoItem);
                    }

                }
            }
        }

        private void RetornarItensParaGravarNaFichaTecnicaMaterialItemECores(Garment garment, out int item, out List<ItemDaFichaTecnica> lstItemFichaTecnica, out List<CorPraGravar> lstCorPraGravar)
        {

            //aqui separo o item pra ser gravado na ficha tecnica do material item
            lstItemFichaTecnica = new List<ItemDaFichaTecnica>();
            lstCorPraGravar = new List<CorPraGravar>();
            item = 1;
            foreach (var variant in garment.variants)
            {

                foreach (var material in variant.materials)
                {

                    string cor = RetornarCor(variant, material);
                    if (!lstItemFichaTecnica.Any(p => p.descricaoToda == material.uid.Trim() + "|" + cor.Trim() + "|" + material.size.Trim()))
                    {
                        var ItemDaFichaTecnica = new ItemDaFichaTecnica();
                        ItemDaFichaTecnica.Cor = cor.Trim();
                        ItemDaFichaTecnica.Tamanho = material.size.Trim();
                        ItemDaFichaTecnica.IdMateriaPrima = material.uid.Trim();
                        ItemDaFichaTecnica.sequencia = Convert.ToInt16(item);
                        ItemDaFichaTecnica.quantidade = material.amount;

                        decimal valor = 0;
                        if (material.total > 0)
                            valor = material.total;
                        else if (material.cost > 0)
                            valor = material.cost;
                        else
                            valor = Convert.ToDecimal(variant.value);


                        ItemDaFichaTecnica.valor = Convert.ToDouble(valor);
                        lstItemFichaTecnica.Add(ItemDaFichaTecnica);
                        item += 1;
                    }

                }


                if (!lstCorPraGravar.Any(p => p.Cor == variant.CorDaVariant))
                {
                    var corPraGravar = new CorPraGravar();
                    corPraGravar.Cor = variant.CorDaVariant.Trim();
                    lstCorPraGravar.Add(corPraGravar);
                }
            }
        }

        private List<Models.ItemPraGravar>  RetornarTodasVariantesPossiveis(Garment garment)
        {
            var lstTodasVariantesFormadas = new List<Models.ItemPraGravar>();
            int item = 1;
            foreach (var variant in garment.variants)
            {
                foreach (var material in variant.materials)
                {
                    var itemPraGravar = new Models.ItemPraGravar();

                    itemPraGravar.uidMateriaPrima = material.uid;
                    itemPraGravar.corProduto = variant.CorDaVariant.Trim();
                    itemPraGravar.item = item;
                    itemPraGravar.tamanhoProduto = variant.TamanhoVariant.Trim();
                    itemPraGravar.quantidade = material.amount;
                    itemPraGravar.descCorMateriaPrima = RetornarCor(variant, material).Trim();
                    itemPraGravar.descTamanhoMateriaPrima = RetornarTamanho(variant, material).Trim();
                    decimal valor = 0;
                    if (material.total > 0)
                        valor = material.total;
                    else if (material.cost > 0)
                        valor = material.cost;
                    else
                        valor = Convert.ToDecimal(variant.value);
                    itemPraGravar.valor = valor;
                    lstTodasVariantesFormadas.Add(itemPraGravar);

                }
                item += 1;

            }

            return lstTodasVariantesFormadas;
        }

        private void IncluirClasseDeProduto(Produto produto, List<Models.ItemPraGravar> lstItensAGravar)
        {
            try
            {
                var GradeDeProdutoService = new GradeDeProdutoService();
                var lstDetalhesDoProduto = GradeDeProdutoService.RetornarDetalhesDoProduto(produto.Id);

                GradeDeProdutoService.ExcluirDetalhes(produto);
           
                var lstCoresETamanhos = (from obj in lstItensAGravar
                                         group obj by new { obj.corProduto, obj.tamanhoProduto } into g
                                         select new
                                         {
                                             corProduto = g.Key.corProduto,
                                             tamanhoProduto = g.Key.tamanhoProduto,
                                             qtd = g.Count(),
                                             Itens = g.ToList()
                                         });

                if (lstCoresETamanhos != null && lstCoresETamanhos.Any())
                {
                    foreach (var item in lstCoresETamanhos)
                    {
                        var cor = Utils.RetornarCor(item.corProduto);
                        var tamanho = Utils.RetornarTamanho(item.tamanhoProduto);
                        GradeDeProdutoService.IncluirDetalhe(produto, cor, tamanho);

                    }
                }

                if (lstDetalhesDoProduto != null && lstDetalhesDoProduto.Any())
                foreach (var item in lstDetalhesDoProduto)
                {
                    var cor = Utils.RetornarCor(item.Idcor);
                    var tamanho = Utils.RetornarTamanho(item.IdTamanho);
                    GradeDeProdutoService.IncluirDetalhe(produto, cor, tamanho);
                }
            }
            catch (Exception ex)
            {

                throw new Exception("Erro ao Incluir Classe de Produto [IncluirClasseDeProduto]  " + ex.Message);
            }
        }


        private FichaTecnicaDoMaterial AlterarFicha(Garment garment, FichaTecnicaDoMaterial fichaVestilo, Produto produto)
        {
            try
            {
                List<FichaTecnicaDoMaterialItem> lstFichaTecnicaMaterialItem = new List<FichaTecnicaDoMaterialItem>();
                fichaVestilo.DataAlteracao = Convert.ToDateTime(garment.last_modified);
                fichaVestilo.Observacao = produto.Obs;
                fichaVestilo.Total = Convert.ToDecimal(garment.variants.Sum(p => p.value));
                FichaTecnicaDoMaterialRepository.Save(ref fichaVestilo); //salvei a ficha

                fichaTecnicaDoMaterialRepositoryItem.ExcluirRelacao(fichaVestilo.Id);//exclui relacao

                var fichatecnicaRelacaoRepository = new FichaTecnicaDoMaterialRelacaoRepository();
                fichatecnicaRelacaoRepository.ExcluirRelacao(fichaVestilo.Id);

                if (garment.variants != null && garment.variants.Any())
                {
                    //inclui itens da ficha
                    fichaVestilo = PrepararParaInclusaoDeNovaFicha(garment, produto, fichaVestilo);

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return fichaVestilo;
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
                item.value = Convert.ToDouble(produtoItem.PrecoVenda);
                item.product_group = grupo?.Id + "-" + grupo?.Descricao;
                item.supplier = fornecedor?.Id + "-" + fornecedor?.RazaoSocial;
                item.measure_unit = uniMedida?.Id + "-" + uniMedida?.Descricao;


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
