using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemplateAudacesApi.Models;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service;

namespace TemplateAudacesApi.Services
{
    public class FichaTecnicaService
    {
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

        private FornecedorService _fornecedorService;
        private FornecedorService fornecedorService
        {
            get
            {
                if (_fornecedorService == null)
                    _fornecedorService = new FornecedorService();
                return _fornecedorService;
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
            Fornecedor fornecedor = null;
            try
            {
                //retorna pela referencia o produto
                produto = produtoRepository.GetByReferencia(garment.reference);

                if (produto == null || produto.Id == 0)
                {
                  //inclui um novo produto,detalhe, fornecedor caso nao tenha
                  produto = produtoInclusaoService.IncluirProdutoAcabado(garment, ref fornecedor);
                }
                else
                {
                    //altera o produto acabado,detalhe,fornecedor
                   produto = produtoInclusaoService.AlterarProdutoAcabado(garment, produto, ref fornecedor);
                }
                
                //retorno a ficha pelo produto
              fichaVestilo  = FichaTecnicaDoMaterialRepository.GetByProduto(Convert.ToInt32(produto.Id));
               
                
                if (fichaVestilo != null && fichaVestilo.Id > 0)
                {
                    AlterarFicha(garment, fichaVestilo, produto);
                }
                else
                {
                    fichaVestilo = IncluirNovaFicha(garment, produto);
                }

            } 
            catch (Exception ex)
            {

                throw ex;
            }
            return fichaVestilo;
        }


        private List<FichaTecnicaDoMaterialItem> IncluirItensFichaTecnica(Garment garment,
                                         FichaTecnicaDoMaterial fichaTecnicaDoMaterial, 
                                         Produto produto)
        {
            List<FichaTecnicaDoMaterialItem> lstFichaTecnicaMaterialItem = new List<FichaTecnicaDoMaterialItem>();
           
            try
            {
                foreach (var item in garment.variants)
                {
                    
                    Produto produtoMaterial = null;
                    Fornecedor fornecedor = null;
                    if (item.material != null)//material
                    {
                        
                        produtoMaterial = produtoRepository.GetByReferencia(item.material.reference);//peguei o item do material pelo codigo

                        if (produtoMaterial != null && produtoMaterial.Id > 0)
                            fornecedorService.ExcluirFornecedoresDoProduto(produtoMaterial);//excluo a relacao de fornecedor e produto caso tenha
                        
                        if (produtoMaterial == null || produtoMaterial.Id == 0)
                           //inclui o item
                            produtoMaterial =  produtoInclusaoService.IncluirProdutoMaterial(item);
                        else //altera o item
                            produtoMaterial =  produtoInclusaoService.AlterarProdutoMaterial(item, produtoMaterial);
                                                  
                        //relaciona o produto, fornecedor, tamanho e cor e a grade do produto
                        fornecedor = fornecedorService.IncluirFornecedor(item.material.supplier, produtoMaterial, item.color, 
                                                                         item.size, 
                                                                         Convert.ToDecimal(item.material.value), false);//incluo a relacao de produto e fornecedor

                       
                      
                       


                    }
                    else if (item.garment != null)//produto
                    {
                       
                        produtoMaterial = produtoInclusaoService.IncluirProdutoAcabado(item.garment,ref fornecedor);
                        
                        if (produtoMaterial != null && produtoMaterial.Id > 0)
                            fornecedorService.ExcluirFornecedoresDoProduto(produtoMaterial);//excluo a relacao de fornecedor e produto caso tenha

                        if (produtoMaterial == null || produtoMaterial.Id == 0)
                            //inclui o item
                            produtoMaterial =  produtoInclusaoService.IncluirProdutoAcabado(item.garment,ref fornecedor);
                        else //altera o item
                            produtoInclusaoService.AlterarProdutoAcabado(item.garment, produtoMaterial, ref fornecedor);

                    }

                    //vai montando a ficha
                    FichaTecnicaDoMaterialItem fichaTecnicaDoMaterialItem = IncluirFichaTecnicaMaterialItem(fichaTecnicaDoMaterial,
                            produtoMaterial,
                            fornecedor,item.material.amount, item.value);


                    lstFichaTecnicaMaterialItem.Add(fichaTecnicaDoMaterialItem);



                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return lstFichaTecnicaMaterialItem;
        }

   
        private FichaTecnicaDoMaterialItem IncluirFichaTecnicaMaterialItem(FichaTecnicaDoMaterial fichaTecnicaDoMaterial,
            Produto produtoMaterial,
            Fornecedor fornecedor,
            decimal quantidade,
            decimal precoDoItem)
        {
            var itemFichaTecnicaDoMaterial = new FichaTecnicaDoMaterialItem();
            itemFichaTecnicaDoMaterial.FichaTecnicaId = fichaTecnicaDoMaterial.Id;
            itemFichaTecnicaDoMaterial.MateriaPrimaId = produtoMaterial.Id;
            itemFichaTecnicaDoMaterial.preco = precoDoItem;
            itemFichaTecnicaDoMaterial.DestinoId = 1;
            itemFichaTecnicaDoMaterial.idFornecedor = fornecedor.Id;
            itemFichaTecnicaDoMaterial.quantidade = quantidade;
           
            fichaTecnicaDoMaterialRepositoryItem.Save(ref itemFichaTecnicaDoMaterial);
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
                ficha.Observacao = garment.notes;
                ficha.UserId = 1;
                ficha.Total =Convert.ToDecimal(garment.value);
                ficha.possuiquebra = false;
                
                FichaTecnicaDoMaterialRepository.Save(ref ficha); //salvei a ficha
                
                //inclui itens da ficha
                if (garment.variants != null &&  garment.variants.Any())
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

       
        private void AlterarFicha(Garment garment,FichaTecnicaDoMaterial fichaVestilo,Produto produto)
        {
            try
            {
                List<FichaTecnicaDoMaterialItem> lstFichaTecnicaMaterialItem = new List<FichaTecnicaDoMaterialItem>();
                fichaVestilo.DataAlteracao = Convert.ToDateTime(garment.last_modified);
                fichaVestilo.Observacao = garment.notes;
                fichaVestilo.Total = 0;
                FichaTecnicaDoMaterialRepository.Save(ref fichaVestilo); //salvei a ficha

                fichaTecnicaDoMaterialRepositoryItem.ExcluirRelacao(fichaVestilo.Id);//exclui relacao


                if (garment.variants != null &&  garment.variants.Any())
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

       

      
        

    }
}
