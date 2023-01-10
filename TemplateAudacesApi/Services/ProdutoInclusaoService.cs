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
    public class ProdutoInclusaoService
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




        public Produto IncluirProdutoAcabado(Garment garment, ref Fornecedor fornecedor)
        {
            Produto produto = new Produto();

            try
            {
                GrupProduto grupo = Utils.RetornarGrupo(garment.product_group);
                UniMedida uniMedida = Utils.RetornarUnidade(garment.measure_unit);

                produto.Descricao = garment.name;
                produto.IdAlmoxarifado = 1;
                produto.Ncm = "012345678";
                produto.Origem = 0;
                produto.DescricaoAlternativa = garment.description;
                produto.DataCadastro = DateTime.Now;
                produto.Ativo = true;
                produto.TipoItem = 0;
                produto.IdUniMedida = uniMedida.Id;
                produto.IdGrupo = grupo.Id;
                
                produto.Referencia = garment.reference;
                produto.Obs= garment.notes;
                produto.DataAlteracao = Convert.ToDateTime(garment.last_modified);
                produto.Colecao = garment.collection;

                if (!string.IsNullOrEmpty(garment.author))
                    produto.Obs += "\n author:" + garment.author;

                if (!string.IsNullOrEmpty(garment.responsible))
                    produto.Obs += "\n responsavel:" + garment.responsible;

                
                produto.IdEmpresa = 1;
                produtoService.Save(ref produto);
                //incluir fornecedor, inclui cor, tamanho, detalhe do produto
                fornecedor =  fornecedorService.IncluirFornecedor(garment, produto, true);



            }
            catch (Exception ex)
            {
                throw ex;
            }
            return produto;

        }

        public Produto AlterarProdutoAcabado(Garment garment, Produto produto, ref Fornecedor fornecedor)
        {


            GrupProduto grupo = Utils.RetornarGrupo(garment.product_group);
            UniMedida uniMedida = Utils.RetornarUnidade(garment.measure_unit);

            produto.Descricao = garment.name;
            produto.DescricaoAlternativa = garment.description;
            produto.DataAlteracao = DateTime.Now;
            produto.IdUniMedida = uniMedida.Id;
            produto.IdGrupo = grupo.Id;
            produto.Referencia = garment.reference;
            produto.Obs= garment.notes;
            produto.Colecao = garment.collection;

            if (!string.IsNullOrEmpty(garment.author))
                produto.Obs += "\n author:" + garment.author;

            if (!string.IsNullOrEmpty(garment.responsible))
                produto.Obs += "\n responsavel:" + garment.responsible;

            produtoService.Save(ref produto);

            fornecedorService.ExcluirFornecedoresDoProduto(produto);
            gradeDetalheService.ExcluirDetalhes(produto);
            //incluir fornecedor, inclui cor, tamanho, detalhe do produto
            fornecedor =  fornecedorService.IncluirFornecedor(garment, produto, true);
            return produto;
        }

        public Produto IncluirProdutoMaterial(Variant variant)
        {
           

            GrupProduto grupo = Utils.RetornarGrupo(variant.material.product_group);
            UniMedida uniMedida = Utils.RetornarUnidade(variant.material.measure_unit);
            
            var produto = new Produto();
            produto.Descricao = variant.material.description;
            produto.Referencia = variant.material.reference;
            produto.Obs = variant.material.notes;
            produto.TipoItem = 1;
            produto.DescricaoAlternativa = variant.material.description;
           
            produto.IdGrupo = grupo.Id;
            produto.IdAlmoxarifado = 1;
            produto.Ncm = "012345678";
            produto.Origem = 0;
            produto.DataCadastro = DateTime.Now;
            produto.Ativo = true;
            produto.IdUniMedida = uniMedida.Id;
            produto.IdAlmoxarifado = 1;

            produtoService.Save(ref produto);
          
            return produto;
        }

        public Produto AlterarProdutoMaterial(Variant variant, Produto produto)
        {

            GrupProduto grupo = Utils.RetornarGrupo(variant.material.product_group);
            UniMedida uniMedida = Utils.RetornarUnidade(variant.material.measure_unit);
                      
            produto.Descricao = variant.material.description;
            produto.Referencia = variant.material.reference;
            produto.Obs = variant.material.notes;
            produto.TipoItem = 1;
            produto.DescricaoAlternativa = variant.material.description;
         
            produto.IdGrupo = grupo.Id;
            produto.IdUniMedida = uniMedida.Id;
           
            produtoService.Save(ref produto);

            return produto;
        }



    }
}
