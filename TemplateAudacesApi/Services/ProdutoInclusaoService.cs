using System;
using TemplateAudacesApi.Models;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service;

namespace TemplateAudacesApi.Services
{
    public class ProdutoInclusaoService
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

        public Produto IncluirProdutoAcabado(Garment garment, ref Colaborador fornecedor, string referencia,string descricao)
        {
            Produto produto = new Produto();
            Colecao colecao = null;
            try
            {
                
                UniMedida uniMedida = Utils.RetornarUnidade("UN");//VER COM ALEX
                
                var variant = garment.variants[0];
                colecao = Utils.RetornarColecao(variant.Colecao);
                produto.Referencia = referencia;
                produto.Descricao = descricao;
                produto.IdAlmoxarifado = 1;
                produto.Ncm = "012345678";
                produto.IdTipoproduto = 1;
                produto.Origem = 0;
                produto.DescricaoAlternativa = variant.description;
                produto.DataCadastro = DateTime.Now;
                produto.Ativo = true;
                produto.TipoItem = 0;
                produto.IdUniMedida = uniMedida.Id;
                var grupo = Utils.RetornarGrupo(variant.Grupo);
                produto.IdGrupo = (grupo != null && grupo.Id != 0) ? grupo.Id : 1;
                produto.IdAlmoxarifado = 1;
                produto.PrecoVenda = 0;
                
                produto.Obs= variant.notes;
               
                produto.IdColecao =  colecao?.Id;
                produto.QtdPacote = 1;
                produto.TempoPacote = 1;
                if (!string.IsNullOrEmpty(variant.Ano))
                produto.Ano = Convert.ToInt32(variant.Ano.ToString());
                var segmento = Utils.RetornarSegmento(variant.Segmento);
                produto.IdSegmento = segmento?.Id;

                if (!string.IsNullOrEmpty(garment.responsible))
                {
                    if (!string.IsNullOrEmpty(produto.Obs))
                        produto.Obs += " \n ";

                    produto.Obs += ";responsavel:" + garment.responsible;


                }
                //if (!string.IsNullOrEmpty(garment.author))
                //{
                //    if (!string.IsNullOrEmpty(produto.Obs))
                //        produto.Obs += " \n ";

                //    produto.Obs += ";autor:" + garment.author;
                //}

                produto.IdEmpresa = Vestillo.Lib.Funcoes.GetIdEmpresaLogada;
                produtoRepository.Save(ref produto);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return produto;

        }

        public Produto AlterarProdutoAcabado(Garment garment, Produto produto,string descricao)
        {
            var variant = garment.variants[0];
            produto.Descricao = descricao;
            var grupo = Utils.RetornarGrupo(variant.Grupo);
            var colecao = Utils.RetornarColecao(variant.Colecao);
           
            produto.DescricaoAlternativa = variant.description;
            produto.DataAlteracao = DateTime.Now;
            produto.Obs = variant.notes;
            produto.PrecoVenda = 0;
            produto.IdGrupo = grupo.Id;
            produto.IdColecao = colecao.Id;
            produto.Ano = Convert.ToInt32(variant.Ano.ToString());
            var segmento = Utils.RetornarSegmento(variant.Segmento);
            produto.IdSegmento = segmento?.Id;


            if (!string.IsNullOrEmpty(garment.responsible))
            {
                if (!string.IsNullOrEmpty(produto.Obs))
                    produto.Obs += " \n ";

                produto.Obs += "responsavel:" + garment.responsible;
            }
            //if (!string.IsNullOrEmpty(garment.author))
            //{
            //    if (!string.IsNullOrEmpty(produto.Obs))
            //        produto.Obs += " \n ";

            //    produto.Obs += ";autor:" + garment.author;
            //} 

           // var colecao = Utils.RetornarColecao(garment.collection);
         //   produto.IdColecao = colecao?.Id;
            produtoRepository.Save(ref produto);
                                              
            return produto;
        }

        public Produto IncluirProdutoMaterial(Material material)
        {
            UniMedida uniMedida = Utils.RetornarUnidade(material.size);
            
            var produto = new Produto();
            produto.Descricao = material.uid;
            produto.Referencia = material.uid;
            produto.Obs = material.notes;
            produto.TipoItem = 1;
            produto.DescricaoAlternativa = material.description;
           
            produto.IdGrupo = 1;//ver com alex
            produto.IdAlmoxarifado = 1;
            produto.Ncm = "012345678";
            produto.Origem = 0;
            produto.DataCadastro = DateTime.Now;
            produto.Ativo = true;
            produto.IdUniMedida = uniMedida.Id;
            produtoRepository.Save(ref produto);
          
            return produto;
        }

        public Produto AlterarProdutoMaterial(Material material, Produto produto)
        {
            UniMedida uniMedida = Utils.RetornarUnidade(material.um);
            GrupProduto grupo = Utils.RetornarGrupo(material.group);      
            produto.Descricao = material.desc;
            produto.Referencia = material.uid;
            produto.TipoItem = 1;
            produto.DescricaoAlternativa = material.desc;
            produto.IdGrupo = grupo.Id;

            produto.IdUniMedida = uniMedida.Id;
            produtoRepository.Save(ref produto);

            return produto;
        }



    }
}
