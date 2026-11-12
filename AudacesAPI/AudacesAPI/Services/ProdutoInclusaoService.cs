using System;
using TemplateAudacesApi.Models;
using TemplateAudacesApi.Utils;
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


        public Produto IncluirProdutoAcabado(Garment garment, ref Colaborador fornecedor, string referencia,
            string descricao)
        {
            Produto produto = new Produto();
            Colecao colecao = null;
            try
            {
                //GrupProduto grupo = Utils.RetornarGrupo(garment.product_group);//VER COM ALEX
                UniMedida uniMedida = Utils.RetornarUnidade("1-TU"); //VER COM ALEX
                colecao = Utils.RetornarColecao(garment.collection);
                var variant = garment.variants[0];
                produto.Referencia = referencia;
                produto.Descricao = descricao;
                produto.IdAlmoxarifado = 1;
                produto.Id = 0;
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
                produto.Obs = variant.notes;
                produto.DataAlteracao = Convert.ToDateTime(garment.last_modified);
                produto.IdColecao = colecao?.Id;
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
                throw;
            }

            return produto;
        }

        public Produto IncluirProdutoAcabado(GarmentRequest garment, ref Colaborador fornecedor, string referencia)
        {
            Produto produto = new Produto();
            Colecao colecao = null;
            try
            {
                colecao = Utils.RetornarColecao(garment.collection);
                var variant = garment.variants[0];
                var refe = UtilService.GetCustomFieldString(variant.custom_fields, "ref");
                var unidadeDedida = UtilService.GetCustomFieldString(variant.custom_fields, "um");
                var ncm = UtilService.GetCustomFieldString(variant.custom_fields, "NCM");
                var origem = UtilService.GetCustomFieldString(variant.custom_fields, "Origem");
                var idAlmoxarifado = UtilService.GetCustomFieldString(variant.custom_fields, "Almoxarifado");
                var refDoCliente = UtilService.GetCustomFieldString(variant.custom_fields, "Referência do cliente");
                var ano = UtilService.GetCustomFieldString(variant.custom_fields, "Ano");
                var customCategoria = UtilService.GetCustomFieldString(variant.custom_fields, "Categoria");
                var customCatologo = UtilService.GetCustomFieldString(variant.custom_fields, "Catálogo");
                var customEntrega = UtilService.GetCustomFieldString(variant.custom_fields, "Entrega");
                var customEstilista = UtilService.GetCustomFieldString(variant.custom_fields, "Estilista");
                var customFamilia = UtilService.GetCustomFieldString(variant.custom_fields, "Família");
                var customGriffe = UtilService.GetCustomFieldString(variant.custom_fields, "Griffe");
                var customGroup = UtilService.GetCustomFieldString(variant.custom_fields, "group");
                var customModelista = UtilService.GetCustomFieldString(variant.custom_fields, "Modelista");
                var customSegmento = UtilService.GetCustomFieldString(variant.custom_fields, "Segmento");
                var customSubCategoria = UtilService.GetCustomFieldString(variant.custom_fields, "SubCategoria");
                var customSubgrupo = UtilService.GetCustomFieldString(variant.custom_fields, "Subgrupo");
                var customFornecedor = UtilService.GetCustomFieldString(variant.custom_fields, "supplier");
                var custoDescNFe =
                    UtilService.GetCustomFieldString(variant.custom_fields, "Descrição somente para FE-e");
                Almoxarifado almoxarifado = CacheService.BuscarAlmoxarifado(idAlmoxarifado);
                Categoria categoria = CacheService.BuscaCategoria(customCategoria);
                UniMedida uniMedida = Utils.RetornarUnidade(unidadeDedida);
                Catalogo catologo = CacheService.BuscarCatalogo(customCatologo);
                Entrega entrega = CacheService.BuscaEntrega(customEntrega);
                Estilista estilista = CacheService.BuscarEstilista(customEstilista);
                Familia familia = CacheService.BuscarFamilia(customFamilia);
                Griffe griffe = CacheService.BuscarGriffe(customGriffe);
                GrupProduto grupo = CacheService.BuscarGrupo(customGroup);
                Modelista modelista = CacheService.BuscarModelista(customModelista);
                Segmento segmento = CacheService.BuscarSegmento(customSegmento);
                SubCategoria subCategoria = CacheService.BuscaSubCategoria(customSubCategoria);
                SubGrupo subGrupo = CacheService.BuscarSubGrupo(customSubgrupo);
                fornecedor = CacheService.BuscarFornecedor(customFornecedor);


                produto.Referencia = refe;
                produto.Descricao = variant.description;
                produto.DescricaoNFE = custoDescNFe;
                produto.Ncm = ncm;
                produto.Origem = Convert.ToInt32(origem.Split('-')[0].Trim());
                produto.DescricaoAlternativa = variant.description;
                produto.Ano = !string.IsNullOrEmpty(ano) ? Convert.ToInt32(ano) : 0;
                if (categoria.Id > 0)
                    produto.IdCategoria = categoria.Id;

                if (catologo != null && catologo.Id > 0)
                    produto.IdCatalogo = catologo.Id;
                if (entrega != null && entrega.Id > 0)
                    produto.IdEmtrega = entrega.Id;
                if (estilista != null && estilista.Id > 0)
                    produto.IdEstilista = estilista.Id;
                if (familia != null && familia.Id > 0)
                    produto.IdFamilia = familia.Id;
                if (griffe != null && griffe.Id > 0)
                    produto.IdGriffe = griffe.Id;
                if (grupo != null && grupo.Id > 0)
                    produto.IdGrupo = grupo.Id;
                if (modelista != null && modelista.Id > 0)
                    produto.IdModelista = modelista.Id;
                if (segmento != null && segmento.Id > 0)
                    produto.IdSegmento = segmento.Id;
                if (subCategoria != null && subCategoria.Id > 0)
                    produto.IdSubCategoria = subCategoria.Id;
                if (subGrupo != null && subGrupo.Id > 0)
                    produto.IdSubGrupo = subGrupo.Id;

                produto.IdAlmoxarifado = almoxarifado.Id;
                produto.IdUniMedida = uniMedida.Id;
                produto.DataCadastro = DateTime.Now;
                produto.Ativo = true;
                produto.TipoItem = 0;

                produto.ReferenciaCli = refDoCliente;

                produto.PrecoVenda = Convert.ToDecimal(variant.value);
                produto.Obs = "";
                produto.DataAlteracao = Convert.ToDateTime(garment.last_modified);
                produto.IdColecao = colecao?.Id;
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

                produto.CSTNfce = "AA";
                produto.IdEmpresa = 1;
                produtoRepository.Save(ref produto);
            }
            catch (Exception ex)
            {
                throw;
            }

            return produto;
        }

        public Produto AlterarProdutoAcabado(Garment garment, Produto produto, string descricao)
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

        public Produto AlterarProdutoAcabado(GarmentRequest garment, Produto produto)
        {
            var colecao = Utils.RetornarColecao(garment.collection);
            var variant = garment.variants[0];
            var refe = UtilService.GetCustomFieldString(variant.custom_fields, "ref");
            var unidadeDedida = UtilService.GetCustomFieldString(variant.custom_fields, "um");
            var ncm = UtilService.GetCustomFieldString(variant.custom_fields, "NCM");
            var origem = UtilService.GetCustomFieldString(variant.custom_fields, "Origem");
            var idAlmoxarifado = UtilService.GetCustomFieldString(variant.custom_fields, "Almoxarifado");
            var refDoCliente = UtilService.GetCustomFieldString(variant.custom_fields, "Referência do cliente");
            var ano = UtilService.GetCustomFieldString(variant.custom_fields, "Ano");
            var customCategoria = UtilService.GetCustomFieldString(variant.custom_fields, "Categoria");
            var customCatologo = UtilService.GetCustomFieldString(variant.custom_fields, "Catálogo");
            var customEntrega = UtilService.GetCustomFieldString(variant.custom_fields, "Entrega");
            var customEstilista = UtilService.GetCustomFieldString(variant.custom_fields, "Estilista");
            var customFamilia = UtilService.GetCustomFieldString(variant.custom_fields, "Família");
            var customGriffe = UtilService.GetCustomFieldString(variant.custom_fields, "Griffe");
            var customGroup = UtilService.GetCustomFieldString(variant.custom_fields, "group");
            var customModelista = UtilService.GetCustomFieldString(variant.custom_fields, "Modelista");
            var customSegmento = UtilService.GetCustomFieldString(variant.custom_fields, "Segmento");
            var customSubCategoria = UtilService.GetCustomFieldString(variant.custom_fields, "SubCategoria");
            var customSubgrupo = UtilService.GetCustomFieldString(variant.custom_fields, "Subgrupo");
            var customFornecedor = UtilService.GetCustomFieldString(variant.custom_fields, "supplier");
            var custoDescNFe =
                UtilService.GetCustomFieldString(variant.custom_fields, "Descrição somente para FE-e");
            Almoxarifado almoxarifado = CacheService.BuscarAlmoxarifado(idAlmoxarifado);
            Categoria categoria = CacheService.BuscaCategoria(customCategoria);
            UniMedida uniMedida = Utils.RetornarUnidade(unidadeDedida);
            Catalogo catologo = CacheService.BuscarCatalogo(customCatologo);
            Entrega entrega = CacheService.BuscaEntrega(customEntrega);
            Estilista estilista = CacheService.BuscarEstilista(customEstilista);
            Familia familia = CacheService.BuscarFamilia(customFamilia);
            Griffe griffe = CacheService.BuscarGriffe(customGriffe);
            GrupProduto grupo = CacheService.BuscarGrupo(customGroup);
            Modelista modelista = CacheService.BuscarModelista(customModelista);
            Segmento segmento = CacheService.BuscarSegmento(customSegmento);
            SubCategoria subCategoria = CacheService.BuscaSubCategoria(customSubCategoria);
            SubGrupo subGrupo = CacheService.BuscarSubGrupo(customSubgrupo);
            var fornecedor = CacheService.BuscarFornecedor(customFornecedor);


            produto.Referencia = refe;
            produto.Descricao = variant.description;
            produto.DescricaoNFE = custoDescNFe;
            produto.Ncm = ncm;
            produto.Origem = Convert.ToInt32(origem.Split('-')[0].Trim());
            produto.DescricaoAlternativa = variant.description;
            produto.Ano = !string.IsNullOrEmpty(ano) ? Convert.ToInt32(ano) : 0;
            if (categoria.Id > 0)
                produto.IdCategoria = categoria.Id;

            if (catologo != null && catologo.Id > 0)
                produto.IdCatalogo = catologo.Id;
            if (entrega != null && entrega.Id > 0)
                produto.IdEmtrega = entrega.Id;
            if (estilista != null && estilista.Id > 0)
                produto.IdEstilista = estilista.Id;
            if (familia != null && familia.Id > 0)
                produto.IdFamilia = familia.Id;
            if (griffe != null && griffe.Id > 0)
                produto.IdGriffe = griffe.Id;
            if (grupo != null && grupo.Id > 0)
                produto.IdGrupo = grupo.Id;
            if (modelista != null && modelista.Id > 0)
                produto.IdModelista = modelista.Id;
            if (segmento != null && segmento.Id > 0)
                produto.IdSegmento = segmento.Id;
            if (subCategoria != null && subCategoria.Id > 0)
                produto.IdSubCategoria = subCategoria.Id;
            if (subGrupo != null && subGrupo.Id > 0)
                produto.IdSubGrupo = subGrupo.Id;
            


            produto.IdAlmoxarifado = almoxarifado.Id;
            produto.IdUniMedida = uniMedida.Id;
            produto.DataCadastro = DateTime.Now;
            produto.Ativo = true;
            produto.TipoItem = 0;

            produto.ReferenciaCli = refDoCliente;

            produto.PrecoVenda = Convert.ToDecimal(variant.value);
            produto.Obs = "";
            produto.DataAlteracao = Convert.ToDateTime(garment.last_modified);
            produto.IdColecao = colecao?.Id;
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

            produto.CSTNfce = "AA";
            produto.IdEmpresa = 1;
            
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

            produto.IdGrupo = 1; //ver com alex
            produto.IdAlmoxarifado = 1;
            produto.Ncm = "012345678";
            produto.Origem = 0;
            produto.DataCadastro = DateTime.Now;
            produto.Ativo = true;
            produto.IdUniMedida = uniMedida.Id;
            produtoRepository.Save(ref produto);

            return produto;
        }

        public Produto IncluirProdutoMaterial(MaterialRequest material)
        {
            //verificar onde pega unidade
            var uniMedida = Utils.RetornarUnidade(material.um);
            var grupo = CacheService.BuscarGrupo(material.group);
            
            var produto = new Produto();
            produto.Descricao = material.desc;
            produto.Referencia = material.uid;
            produto.Obs = material.obs;
            produto.TipoItem = 1;
            produto.DescricaoAlternativa = material.desc;
            
            if (grupo != null && grupo.Id > 0)
            produto.IdGrupo = grupo.Id;

            if (uniMedida != null && uniMedida.Id > 0)
                produto.IdUniMedida = uniMedida.Id;

            produto.IdAlmoxarifado = 1;
            
            produto.Ncm = "012345678";
            produto.Origem = 0;
            produto.DataCadastro = DateTime.Now;
            produto.Ativo = true;
         
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