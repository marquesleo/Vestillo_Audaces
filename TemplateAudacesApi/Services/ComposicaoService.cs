using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemplateAudacesApi.Models;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace TemplateAudacesApi.Services
{
    public class ComposicaoService
    {

        private GradeDeProdutoService _produtoDetalheService;
        public GradeDeProdutoService gradeDetalheService
        {
            get
            {
                if (_produtoDetalheService == null)
                    _produtoDetalheService = new GradeDeProdutoService();

                return _produtoDetalheService;
            }
        }



        private FornecedorRepository _fornecedorRepository;
        private FornecedorRepository fornecedorRepository
        {
            get
            {
                if (_fornecedorRepository == null)
                    _fornecedorRepository = new FornecedorRepository();

                return _fornecedorRepository;
            }
        }
        private ProdutoFornecedorPrecoRepository _produtoFornecedorPrecoRepository;
        private ProdutoFornecedorPrecoRepository produtoFornecedorPrecoRepository
        {
            get
            {
                if (_produtoFornecedorPrecoRepository == null)
                    _produtoFornecedorPrecoRepository = new ProdutoFornecedorPrecoRepository();
                return _produtoFornecedorPrecoRepository;
            }
        }
        private TamanhoRepository _TamanhoRepository;
        private TamanhoRepository TamanhoRepository
        {
            get
            {
                if (_TamanhoRepository == null)
                    _TamanhoRepository = new TamanhoRepository();
                return _TamanhoRepository;
            }
        }

        private CorRepository _corDoProdutoRepository;
        private CorRepository corRepository
        {
            get
            {
                if (_corDoProdutoRepository == null)
                    _corDoProdutoRepository = new CorRepository();
                return _corDoProdutoRepository;
            }
        }


        public void ExcluirFornecedoresDoProduto(Produto produto)
        {
            var lstItem = produtoFornecedorPrecoRepository.GetListByProdutoFornecedor(produto.Id);
            try
            {
                if (lstItem != null && lstItem.Any())
                {
                    foreach (var item in lstItem)
                    {
                        produtoFornecedorPrecoRepository.Delete(item.Id);
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public ProdutoFornecedorPreco GravarItemDaComposicao(string supplier,
                                      Produto produto, 
                                      string color, 
                                      string size,
                                      decimal preco, 
                                      bool produtoAcabado = false)
        {
            ProdutoFornecedorPreco produtoFornecedorPreco = null;
          
            try
            {
                produtoFornecedorPreco = RelacionarProdutoFornecedorCorETamanho(supplier, produto, color, size, preco);

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return produtoFornecedorPreco;
        }

        private ProdutoFornecedorPreco RelacionarProdutoFornecedorCorETamanho(string supplier, 
                                                                   Produto produto, 
                                                                   string color,
                                                                   string size, 
                                                                   decimal preco)
        {
            
            Cor cor;
            Tamanho tamanho;
            GravarCorETamanho(color, size, out cor, out tamanho);


            Colaborador fornecedor = Utils.RetornarFornecedorDoProduto(produto);
            if (fornecedor == null || fornecedor.Id == 0)
                throw new Exception($"Material:{produto.Referencia} não possui fornecedor!");
            
            var FornecedorPreco = new ProdutoFornecedorPreco();
            FornecedorPreco.IdProduto = produto.Id;
            FornecedorPreco.IdFornecedor = fornecedor.Id;
            FornecedorPreco.IdTamanho = tamanho.Id;
            FornecedorPreco.IdCor = cor.Id;
            //tem q ver qual tipo e gravar

            if (produto.TipoCustoFornecedor == 2)// Cor
                FornecedorPreco.PrecoCor = preco;
            else if (produto.TipoCustoFornecedor == 3)// Tamanho
                FornecedorPreco.PrecoTamanho = preco;
            else
                FornecedorPreco.PrecoFornecedor = preco;

          //  produtoFornecedorPrecoRepository.Save(ref FornecedorPreco);
           

            return FornecedorPreco;
        }

        public void GravarGradeDeProduto(Produto produto, string color, string size)
        {
            Cor cor;
            Tamanho tamanho;
            GravarCorETamanho(color, size, out cor, out tamanho);
            gradeDetalheService.IncluirDetalhe(produto, cor, tamanho);//vincula o produto com a grade
        }

        public void ExcluirGradeDeProduto(Produto produto)
        {
            try
            {
                gradeDetalheService.ExcluirDetalhes(produto);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void GravarCorETamanho(string color, string size, out Cor cor, out Tamanho tamanho)
        {
            cor = new Cor();
            tamanho = new Tamanho();
            IncluirCor(ref cor, color);
            IncluirTamanho(size, ref tamanho);

        }

       
    



        private void IncluirTamanho(string size, ref Tamanho Tamanho)
        {
            if (!string.IsNullOrEmpty(size))
            {
                Tamanho = Utils.RetornarTamanho(size);
            }

        }

        private void IncluirCor(ref Cor cor, string  color)
        {
            try
            {
                if (color != null)
                {
                    cor = Utils.RetornarCor(color);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
