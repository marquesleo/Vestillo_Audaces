using System;
using System.Collections.Generic;
using System.Linq;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace TemplateAudacesApi.Services
{
    public static class Utils
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
        private static ColaboradorRepository ColaboradorRepository
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
                    if (lstCor != null && lstCor.Any())
                    {
                        cor = getCor(lstCor, descricao);
                    }

                }
            }

            return cor;
        }

        private static Cor getCor(IEnumerable<Cor> lstCor, string decricao)
        {
            Cor cor = new Cor();
            if (lstCor != null && lstCor.Any())
            {
                cor = lstCor.ToList().Find(p => p.Descricao.Contains(decricao));
            }
            return cor;
        }

        private static List<Cor> _lstCor;
        public static List<Cor> lstCor
        {
            get
            {
                if (_lstCor == null)
                {
                    _lstCor = new List<Cor>();
                    _lstCor = CorRepository.GetByAtivos(1).ToList();

                }
                return _lstCor;
            }
        }

        private static List<Colecao> _lstColecao;
        public static List<Colecao> lstColecao
        {
            get
            {
                if (_lstColecao == null)
                    _lstColecao = new List<Colecao>();
                _lstColecao = ColecaoRepository.GetAll().ToList();

                return _lstColecao;
            }
           
        }

        public static Cor RetornarCor(int idCor)
        {
            Cor cor = new Cor();


            if (idCor > 0)
            {
                cor = lstCor.Find(p => p.Id == idCor);
                if (cor == null || cor.Id == 0)
                {
                    cor = CorRepository.GetById(Convert.ToInt32(idCor));
                    lstCor.Add(cor);
                }

            }

            return cor;
        }


        public static Colecao RetornarColecao(string descricao)
        {
            Colecao colecao = null;
          
            string codigo = string.Empty;
            if (!string.IsNullOrWhiteSpace(descricao))
            {

                colecao = lstColecao.Find(p => p.Descricao.Contains(descricao));
                if (colecao != null && colecao.Id != 0)
                {
                    return colecao;
                }
                else
                {
                    IncluirColecao(descricao, ref colecao);
                }

            }

            return colecao;
        }

        public static void RetornarCorETamanhoDoTexto(string textoComCorETamanho, ref Cor cor, ref Tamanho tamanho)
        {

            try
            {

                string[] lines = textoComCorETamanho.Split(new[] { " - " }, StringSplitOptions.None);

                foreach (string line in lines)
                {
                    string[] elements = line.Split(new[] { ": " }, StringSplitOptions.None);
                    string formattedLine = string.Join(":",
                        elements[0],
                        elements[1].Replace("-", "- ").Replace("Tamanho", " Tamanho"));


                    if (elements[0] == "COR")
                        cor = RetornarCor(elements[1]);

                    if (elements[0] == "TAMANHO")
                        tamanho = RetornarTamanho(elements[1]);


                }


            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao retornar cor e tamanho do produto acabado. " + ex.Message);
            }
        
        }


        private static List<Tamanho> _lstTamanho;
        public static List<Tamanho> lstTamanho
        {
            get
            {
                if (_lstTamanho == null)
                {
                    _lstTamanho = new List<Tamanho>();
                    _lstTamanho = TamanhoRepository.GetByAtivos(1).ToList();
                }
                return _lstTamanho;
            }
        }

        public static Tamanho RetornarTamanho(int idTamanho)
        {
            var tamanho = new Tamanho();


            if (idTamanho > 0)
            {
                tamanho = lstTamanho.Find(p => p.Id == idTamanho);
                if (tamanho == null || tamanho.Id == 0)
                {
                    tamanho = TamanhoRepository.GetById(Convert.ToInt32(idTamanho));
                    lstTamanho.Add(tamanho);
                }

            }

            return tamanho;

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

                    }
                    else
                    {
                        colecao.Descricao = descricao;

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
                    lstFornecedor = ColaboradorRepository.GetPorNome(razao, "fornecedor");
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
