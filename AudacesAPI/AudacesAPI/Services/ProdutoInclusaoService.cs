﻿using System;
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

        private ComposicaoService _fornecedorService;
        private ComposicaoService fornecedorService
        {
            get
            {
                if (_fornecedorService == null)
                    _fornecedorService = new ComposicaoService();
                return _fornecedorService;
            }
        }


        private ContadorCodigoRepository _contadorCodigoRepository;
        private ContadorCodigoRepository contadorCodigoRepository
        {
            get
            {
                if (_contadorCodigoRepository == null)
                    _contadorCodigoRepository = new ContadorCodigoRepository();
                return _contadorCodigoRepository;
            }
        }



        public Produto IncluirProdutoAcabado(Garment garment, ref Colaborador fornecedor, string referencia,string descricao)
        {
            Produto produto = new Produto();
            Colecao colecao = null;
            try
            {
                //GrupProduto grupo = Utils.RetornarGrupo(garment.product_group);//VER COM ALEX
                UniMedida uniMedida = Utils.RetornarUnidade("1-TU");//VER COM ALEX
                colecao = Utils.RetornarColecao(garment.collection);
                var variant = garment.variants[0];
                produto.Referencia = referencia;
                produto.Descricao = descricao;
                produto.IdAlmoxarifado = 1;
                produto.Ncm = "012345678";
                produto.Origem = 0;
                produto.DescricaoAlternativa = variant.description;
                produto.DataCadastro = DateTime.Now;
                produto.Ativo = true;
                produto.TipoItem = 0;
                produto.IdUniMedida = uniMedida.Id;
                produto.IdGrupo = 1;
                produto.IdAlmoxarifado = 1;
                produto.PrecoVenda = 0;
                produto.Obs= variant.notes;
                produto.DataAlteracao = Convert.ToDateTime(garment.last_modified);
                produto.IdColecao =  colecao?.Id;
                produto.QtdPacote = 1;
                produto.TempoPacote = 1;

                if (!string.IsNullOrEmpty(garment.responsible))
                {
                    if (!string.IsNullOrEmpty(produto.Obs))
                        produto.Obs += " \n ";

                    produto.Obs += ";responsavel:" + garment.responsible;


                }
                if (!string.IsNullOrEmpty(garment.author))
                {
                    if (!string.IsNullOrEmpty(produto.Obs))
                        produto.Obs += " \n ";

                    produto.Obs += ";autor:" + garment.author;
                }

                produto.IdEmpresa = 1;
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
            produto.DescricaoAlternativa = variant.description;
            produto.DataAlteracao = DateTime.Now;
            produto.Obs = variant.notes;
            produto.PrecoVenda = variant.value;
            if (!string.IsNullOrEmpty(garment.responsible))
            {
                if (!string.IsNullOrEmpty(produto.Obs))
                    produto.Obs += " \n ";

                produto.Obs += "responsavel:" + garment.responsible;
            }
            if (!string.IsNullOrEmpty(garment.author))
            {
                if (!string.IsNullOrEmpty(produto.Obs))
                    produto.Obs += " \n ";

                produto.Obs += ";autor:" + garment.author;
            }


            produto.Colecao = garment.collection;
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