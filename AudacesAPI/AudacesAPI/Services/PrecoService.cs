using System.Collections.Generic;
using System.Linq;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace TemplateAudacesApi.Services
{
    public class PrecoService
    {

        private ItemTabelaPrecoPCPRepository _itemTabelaPrecoPCPRepository;
        private ItemTabelaPrecoPCPRepository itemTabelaPrecoPCPRepository
        {
            get
            {
                if (_itemTabelaPrecoPCPRepository == null)
                    _itemTabelaPrecoPCPRepository = new ItemTabelaPrecoPCPRepository();
                return _itemTabelaPrecoPCPRepository;
            }
        }

        public decimal RetornarPrecoDeVendaProdutoAcabado(Produto produto)
        {
            var itemTabelaPreco = itemTabelaPrecoPCPRepository.GetAllByProduto(produto.Id);
            if (itemTabelaPreco != null && itemTabelaPreco.Any())
            {
                return itemTabelaPreco.First().PrecoVenda;
            }

            return 0;
        }

        public decimal RetornarPrecoDoMaterial(Produto Material)
        {
            decimal retorno = 0;
            IEnumerable<ProdutoFornecedorPreco> ret = null;
            try
            {
                ret = new ProdutoFornecedorPrecoRepository().GetValoresSemInativos(Material.Id).ToList(); ;
            }
            catch (System.Exception)
            {              
            }

            if (ret != null && ret.Any())
            {
                if (Material.TipoCalculoPreco == 2) //Pega a media
                {
                    if (Material.TipoCustoFornecedor == 2)// Cor
                    {
                        int count = ret.Where(x => x.PrecoCor > 0).ToList().Count();
                        if (count == 0) count = 1;
                        retorno = (ret.Sum(x => x.PrecoCor) / count);
                    }
                    else if (Material.TipoCustoFornecedor == 3)// Tamanho
                    {
                        int count = ret.Where(x => x.PrecoTamanho > 0).ToList().Count();
                        if (count == 0) count = 1;
                        retorno = (ret.Sum(x => x.PrecoTamanho) / count);
                    }
                    else // Fornecedor
                    {
                        int count = ret.Where(x => x.PrecoFornecedor > 0).ToList().Count();
                        if (count == 0) count = 1;
                        retorno = (ret.Sum(x => x.PrecoFornecedor) / count);
                    }
                }
                else // Pega  o maior valor
                {
                    if (Material.TipoCustoFornecedor == 2)// Cor
                        retorno = ret.Max(x => x.PrecoCor);
                    else if (Material.TipoCustoFornecedor == 3)// Tamanho
                        retorno = ret.Max(x => x.PrecoTamanho);
                    else if (ret.Any())
                        retorno = ret.Max(x => x.PrecoFornecedor);
                }
            }
            return retorno;
        }
    }
}
