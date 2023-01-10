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

        private static FornecedorRepository _fornecedorRepository;
        private static  FornecedorRepository fornecedorRepository
        {
            get
            {
                if (_fornecedorRepository == null)
                    _fornecedorRepository = new FornecedorRepository();

                return _fornecedorRepository;
            }
        }


        public static GrupProduto RetornarGrupo(string descricao)
        {
            GrupProduto grupo = new GrupProduto();
            IEnumerable<GrupProduto> lstGrupos = null;
            string codigo = string.Empty;
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
                      

            

            return grupo;
        }


        public static Fornecedor RetornarFornecedor(string razao)
        {
            IEnumerable<Fornecedor> lstFornecedor = null;
            Fornecedor fornecedor = new Fornecedor();
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
                        fornecedor = fornecedorRepository.GetById(Convert.ToInt32(codigo));

                    }
                }
                else
                {
                    lstFornecedor  = fornecedorRepository.GetListPorDescricao(razao);
                    if (lstFornecedor != null && lstFornecedor.Any())
                    {
                        fornecedor = lstFornecedor.FirstOrDefault();
                    }
                }
            }


            return fornecedor;

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
                        codigo = vet[0];
                        descricao = vet[1];
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
