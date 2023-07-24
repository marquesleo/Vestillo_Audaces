using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace TemplateAudacesApi.Services
{
    public static  class Utils
    {
        private static UniMedidaRepository _uniMedidaRepository;
        private static UniMedidaRepository UniMedidaRepository
        {
            get
            {
                if (_uniMedidaRepository == null)
                    _uniMedidaRepository = new UniMedidaRepository();
                return _uniMedidaRepository;

            }
        }
        
        private static ColecaoRepository _ColecaoRepository;
        private static ColecaoRepository ColecaoRepository
        {
            get
            {
                if (_ColecaoRepository == null)
                    _ColecaoRepository = new ColecaoRepository();
                return _ColecaoRepository;

            }
        }

        private static GrupProdutoRepository _GrupoProdutoRepository;
        private static GrupProdutoRepository GrupoProdutoRepository
        {
            get
            {
                if (_GrupoProdutoRepository == null)
                    _GrupoProdutoRepository = new GrupProdutoRepository();

                return _GrupoProdutoRepository;
            }
        }

        private static CorRepository _CorRepository;
        private static CorRepository CorRepository
        {
            get
            {
                if (_CorRepository == null)
                    _CorRepository = new CorRepository();

                return _CorRepository;
            }
        }

        private static TamanhoRepository _TamanhoRepository;
        private static TamanhoRepository TamanhoRepository
        {
            get
            {
                if (_TamanhoRepository == null)
                    _TamanhoRepository = new TamanhoRepository();

                return _TamanhoRepository;
            }
        }

        private static ColaboradorRepository _ColaboradorRepository;
        private static  ColaboradorRepository ColaboradorRepository
        {
            get
            {
                if (_ColaboradorRepository == null)
                    _ColaboradorRepository = new ColaboradorRepository();

                return _ColaboradorRepository;
            }
        }

        private static ProdutoFornecedorPrecoRepository _produtoFornecedorPrecoRepository;
        private static ProdutoFornecedorPrecoRepository produtoFornecedorPrecoRepository
        {
            get
            {
                if (_produtoFornecedorPrecoRepository == null)
                    _produtoFornecedorPrecoRepository = new ProdutoFornecedorPrecoRepository();
                return _produtoFornecedorPrecoRepository;
            }
        }


        public static Colaborador RetornarFornecedorDoProduto(Produto produto)
        {
            try
            {
                var FornecedorPreco = new ProdutoFornecedorPreco();
                produto.Fornecedor = produtoFornecedorPrecoRepository.GetListByProdutoFornecedor(produto.Id);

                if (produto.Fornecedor != null && produto.Fornecedor.Any())
                {
                    FornecedorPreco = produto.Fornecedor.FirstOrDefault();
                }
                var fornecedor = ColaboradorRepository.GetById(FornecedorPreco.IdFornecedor);
                return fornecedor;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public static GrupProduto RetornarGrupo(string descricao)
        {
            GrupProduto grupo = new GrupProduto();
            IEnumerable<GrupProduto> lstGrupos = null;
            string codigo = string.Empty;
            if (!string.IsNullOrWhiteSpace(descricao))
            {
                if (descricao.Contains("-"))
                {
                    var vet = descricao.Split('-');
                    if (vet != null && vet.Any())
                    {
                        codigo = vet[0];
                        descricao = vet[1];
                        grupo = GrupoProdutoRepository.GetById(Convert.ToInt32(codigo));
                    }

                }
                else
                {
                    lstGrupos = GrupoProdutoRepository.GetListPorDescricao(descricao);
                    if (lstGrupos != null && lstGrupos.Any())
                    {
                        grupo = lstGrupos.FirstOrDefault();
                    }
                }
            }
     
            return grupo;
        }

        public static Cor RetornarCor(string descricao)
        {
            Cor cor = new Cor();
            IEnumerable<Cor> lstCor = null;
            string codigo = string.Empty;
            if (!string.IsNullOrWhiteSpace(descricao))
            {
                if (descricao.Contains("-"))
                {
                    var vet = descricao.Split('-');
                    if (vet != null && vet.Any())
                    {
                        codigo = vet[0];
                        descricao = vet[1];
                        cor = CorRepository.GetById(Convert.ToInt32(codigo));
                    }

                }
                else
                {
                    lstCor = CorRepository.GetListPorDescricao(descricao);
                    if (lstCor != null && lstCor.Any())
                    {
                        cor = lstCor.FirstOrDefault();
                    }
                }
            }

            return cor;
        }

        public static Colecao RetornarColecao(string descricao)
        {
            Colecao colecao = null;
            IEnumerable<Colecao> lstColecao = null;
            string codigo = string.Empty;
            if (!string.IsNullOrWhiteSpace(descricao))
            {
                if (descricao.Contains("-"))
                {
                    var vet = descricao.Split('-');
                    if (vet != null && vet.Any())
                    {
                        codigo = vet[0];
                        descricao = vet[1];
                        colecao = ColecaoRepository.GetById(Convert.ToInt32(codigo));
                    }

                }
                else
                {
                    lstColecao = ColecaoRepository.GetListPorDescricao(descricao);
                    if (lstColecao != null && lstColecao.Any())
                    {
                        colecao = lstColecao.FirstOrDefault();
                    }
                }
            }

            return colecao;
        }

        public static Tamanho RetornarTamanho(string descricao)
        {
            Tamanho tamanho = new Tamanho();
            IEnumerable<Tamanho> lstTamanho = null;
            string codigo = string.Empty;
            if (!string.IsNullOrWhiteSpace(descricao))
            {
                if (descricao.Contains("-"))
                {
                    var vet = descricao.Split('-');
                    if (vet != null && vet.Any())
                    {
                        codigo = vet[0];
                        descricao = vet[1];
                        tamanho = TamanhoRepository.GetById(Convert.ToInt32(codigo));
                    }

                }
                else
                {
                    lstTamanho = TamanhoRepository.GetListPorDescricao(descricao);
                    if (lstTamanho != null && lstTamanho.Any())
                    {
                        tamanho = lstTamanho.FirstOrDefault();
                    }
                }
            }

            return tamanho;
        }


        public static Colecao RetornarColecao(int codigo)
        {
            Colecao colecao = new Colecao();
            colecao = ColecaoRepository.GetById(codigo);
            return colecao;
        }

        public static void IncluirColecao(string descricao, ref Colecao colecao)
        {
           
            try
            {
                colecao = RetornarColecao(descricao);
                if (colecao == null || colecao.Id == 0)
                {
                    var vetColecao = descricao.Split("-");
                    if (vetColecao != null && vetColecao.Length == 2)
                    {
                        colecao = new Colecao();
                        colecao.Id = Convert.ToInt32(vetColecao[0].ToString());
                        colecao.Descricao = vetColecao[1];
                       
                    }else
                    {   colecao.Descricao = descricao;
                  
                    }
                    colecao.Abreviatura = colecao.Descricao;
                    colecao.IdEmpresa = 1;
                    ColecaoRepository.Save(ref colecao);

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static GrupProduto RetornarGrupo(int codigo)
        {
            GrupProduto grupo = new GrupProduto();
            grupo = GrupoProdutoRepository.GetById(codigo);
            return grupo;
        }


        public static Colaborador RetornarFornecedor(string razao)
        {
            IEnumerable<Colaborador> lstFornecedor = null;
            Colaborador fornecedor = new Colaborador();
            string codigo = string.Empty;
            if (!string.IsNullOrEmpty(razao))
            {
                if (razao.Contains("-"))
                {
                    var vet = razao.Split("-");
                    if (vet != null && vet.Any())
                    {
                        codigo = vet[0];
                        razao = vet[1];
                        fornecedor = ColaboradorRepository.GetById(Convert.ToInt32(codigo));

                    }
                }
                else
                {
                    lstFornecedor  = ColaboradorRepository.GetPorNome(razao, "fornecedor");
                    if (lstFornecedor != null && lstFornecedor.Any())
                    {
                        fornecedor = lstFornecedor.FirstOrDefault();
                    }
                }
            }


            return fornecedor;

        }

        public static UniMedida RetornarUnidade(int IdUnidade)
        {
            UniMedida uniMedida = new UniMedida();
         
            uniMedida = UniMedidaRepository.GetById(IdUnidade);
                 
            return uniMedida;
        }

        public static UniMedida RetornarUnidade(string descricao)
        {
            UniMedida uniMedida = new UniMedida();
            IEnumerable<UniMedida> lstUnidade = null;
            string codigo = string.Empty;
            if (descricao != null)
            {
              if (descricao.Contains("-"))
                {
                    var vet = descricao.Split('-');
                    if (vet != null && vet.Any())
                    {
                        codigo = vet[0].Trim();
                        descricao = vet[1].Trim();
                        uniMedida = UniMedidaRepository.GetById(Convert.ToInt32(codigo));
                    }

                }
                else
                {
                    lstUnidade = UniMedidaRepository.GetListPorDescricao(descricao);
                    if (lstUnidade != null && lstUnidade.Any())
                    {
                        uniMedida = lstUnidade.FirstOrDefault();
                    }
                }
                
            }
            return uniMedida;
        }
    }
}
