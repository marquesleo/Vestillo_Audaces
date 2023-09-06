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

        private static ProdutoRepository _produtoRepository;
        private static ProdutoRepository produtoRepository
        {
            get
            {
                if (_produtoRepository == null)
                    _produtoRepository = new ProdutoRepository();

                return _produtoRepository;
            }
        }

        private static List<Produto> _lstProduto;
        public static List<Produto> lstProduto
        {
            get
            {
                if (_lstProduto == null)
                    _lstProduto = new List<Produto>();
                return _lstProduto;
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

        private static List<Colaborador> _lstFornecedor;
        private static List<Colaborador> lstFornecedor
        {
            get
            {
                if (_lstFornecedor == null)
                    _lstFornecedor = new List<Colaborador>();

                return _lstFornecedor;
            }
        }
        public static Colaborador RetornarFornecedorDoProduto(Produto produto)
        {
            var fornecedor = new Colaborador();
            try
            {
                var FornecedorPreco = new ProdutoFornecedorPreco();
                produto.Fornecedor = produtoFornecedorPrecoRepository.GetListByProdutoFornecedor(produto.Id);

                if (produto.Fornecedor != null && produto.Fornecedor.Any())
                {
                    FornecedorPreco = produto.Fornecedor.FirstOrDefault();

                    fornecedor = lstFornecedor.FirstOrDefault(p => p.Id == FornecedorPreco.IdFornecedor);
                    if (fornecedor == null || fornecedor.Id == 0)
                    {
                        fornecedor = ColaboradorRepository.GetById(FornecedorPreco.IdFornecedor);
                        lstFornecedor.Add(fornecedor);
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return fornecedor;
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
                        cor = lstCor.FirstOrDefault(p=> p.Id == Convert.ToInt32(codigo));
                       
                    }

                }
                else
                {
                    if (lstCor != null && lstCor.Any())
                    {
                        cor = getCor(lstCor, descricao);
                        if (cor == null || cor.Id == 0)
                            cor = lstCor.FirstOrDefault(p => p.Id == Convert.ToInt32(descricao));
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
                    _lstCor = CorRepository.GetAll().ToList();

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


        public static Produto RetornarProduto(int idProduto)
        {
            Produto produto = new Produto();


            if (idProduto > 0)
            {
                produto = lstProduto.Find(p => p.Id == idProduto);
                if (produto == null || produto.Id == 0)
                {
                    produto = produtoRepository.GetById(Convert.ToInt32(idProduto));
                    if (produto != null)
                        lstProduto.Add(produto);
                }

            }

            return produto;
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

        private static List<GrupProduto> _lstGrupo;
        public static List<GrupProduto> lstGrupo
        {
            get
            {
                if (_lstGrupo == null)
                {
                    _lstGrupo = new List<GrupProduto>();
                
                    _lstGrupo = GrupoProdutoRepository.GetAll().ToList();
                }
                return _lstGrupo;
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
                        tamanho = lstTamanho.FirstOrDefault(p => p.Id ==  Convert.ToInt32(codigo));
                    }

                }
                else
                {
                    tamanho = lstTamanho.FirstOrDefault(p => p.Descricao.Contains(descricao));
                    
                }
            }

            return tamanho;
        }


        public static Colecao RetornarColecao(int codigo)
        {

            var colecao = lstColecao.FirstOrDefault(p => p.Id == codigo);
            return colecao;
        }

        public static void IncluirColecao(string descricao, ref Colecao colecao)
        {

            try
            {
                    colecao = new Colecao();
                    colecao.Abreviatura = descricao;
                    colecao.Descricao = descricao;
                    colecao.IdEmpresa = Vestillo.Lib.Funcoes.GetIdEmpresaLogada;
                    ColecaoRepository.Save(ref colecao);
                    lstColecao.Add(colecao);

      
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public static GrupProduto RetornarGrupo(int codigo)
        {
            GrupProduto grupo = new GrupProduto();
            grupo = lstGrupo.FirstOrDefault(p => p.Id == codigo);
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

        private static List<UniMedida> _lstUnidade;
        public static List<UniMedida> lstUnidade
        {
            get
            {
                if (_lstUnidade == null)
                    _lstUnidade = new List<UniMedida>();
                return _lstUnidade;
            }
        }

        public static UniMedida RetornarUnidade(int IdUnidade)
        {
            UniMedida uniMedida = new UniMedida();

            if (IdUnidade > 0)
            {
                uniMedida = lstUnidade.FirstOrDefault(p => p.Id == IdUnidade);
                if (uniMedida == null || uniMedida.Id == 0)
                {
                    uniMedida = UniMedidaRepository.GetById(IdUnidade);
                    lstUnidade.Add(uniMedida);
                }
            }
       

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
