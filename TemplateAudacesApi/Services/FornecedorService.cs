using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemplateAudacesApi.Models;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace TemplateAudacesApi.Services
{
    public class FornecedorService
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

        public Fornecedor IncluirFornecedor(string supplier, Produto produto, Color color, string size, decimal preco,  bool produtoAcabado = false)
        {
            Fornecedor fornecedor = null;
          
            try
            {
                fornecedor= gravarItem(supplier, produto, color, size, preco);

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return fornecedor;
        }

        private Fornecedor gravarItem(string supplier, Produto produto, Color color, string size, decimal preco)
        {
            Fornecedor fornecedor = Utils.RetornarFornecedor(supplier);
            Cor cor = new Cor();
            Tamanho tamanho = new Tamanho();
            IncluirCor(ref cor, color);
            IncluirTamanho(size, ref tamanho);

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

            produtoFornecedorPrecoRepository.Save(ref FornecedorPreco);

            gradeDetalheService.IncluirDetalhe(produto, cor, tamanho);//vincula o produto com a grade
            return fornecedor;
        }

        public Fornecedor IncluirFornecedor(Garment garment, Produto produto, bool produtoAcabado = false)
        {
            Fornecedor fornecedor = new Fornecedor();
            try
            {
                fornecedor= Utils.RetornarFornecedor(garment.supplier);
               

                foreach (var item in garment.variants)
                {
                    Cor cor = new Cor();
                    Tamanho tamanho = new Tamanho();

                    fornecedor= gravarItem(garment.supplier, produto, item.color, item.size, item.value);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            return fornecedor;
        }

        private void IncluirTamanho(string size, ref Tamanho Tamanho)
        {
            if (!string.IsNullOrEmpty(size))
            {
                var Tamanhos = TamanhoRepository.GetListPorDescricao(size);

                if (Tamanhos!= null && Tamanhos.Any())
                    Tamanho = Tamanhos.FirstOrDefault();
                else
                {
                    Tamanho.Descricao = size;
                    Tamanho.Abreviatura = size;
                    TamanhoRepository.Save(ref Tamanho);

                }
            }

        }

        private void IncluirCor(ref Cor cor, Color color)
        {
            try
            {
                if (color != null)
                {
                    var lstCores = corRepository.GetListPorDescricao(color.value);
                    if (lstCores !=null && lstCores.Any())
                    {
                        cor = lstCores.FirstOrDefault();
                    }
                    if (color != null)
                    {
                        if (cor == null || cor.Id == 0)
                        {
                            cor = new Cor();
                            cor.Ativo = true;
                            cor.Descricao = color.value;
                            cor.Id = Convert.ToInt32(color.uid);
                            cor.Abreviatura = color.description;
                            corRepository.Save(ref cor);
                        }
                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
