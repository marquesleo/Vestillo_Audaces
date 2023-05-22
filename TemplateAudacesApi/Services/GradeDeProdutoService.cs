using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemplateAudacesApi.Models;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service;

namespace TemplateAudacesApi.Services
{
    public class GradeDeProdutoService
    {
        private  ProdutoDetalheRepository _produtoDetalheRepository;
        private ProdutoDetalheRepository produtoDetalheRepository
        {
            get
            {
                if (_produtoDetalheRepository == null)
                    _produtoDetalheRepository = new ProdutoDetalheRepository();

                return _produtoDetalheRepository;
            }
        }

        public List<ProdutoDetalhe> lstDetalhe = new List<ProdutoDetalhe>();
        public void IncluirDetalhe(Produto produto, Cor cor, Tamanho tamanho)
        {
            try
            {
                if (!lstDetalhe.Any(p => p.Idcor == cor.Id && p.IdTamanho == tamanho.Id))
                {
                    var detalhe = new ProdutoDetalhe();
                    detalhe.DataAlteracao = DateTime.Now;
                    detalhe.Idcor = cor.Id;
                    detalhe.IdProduto = produto.Id;
                    detalhe.IdTamanho = tamanho.Id;
                    detalhe.custo = produto.Custo;

                    produtoDetalheRepository.Save(ref detalhe);
                    lstDetalhe.Add(detalhe);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ExcluirDetalhes(Produto produto)
        {
            try
            {
                var lstDetalhe = produtoDetalheRepository.GetListByProduto(produto.Id, 1);
                foreach (var detalhe in lstDetalhe)
                {
                    produtoDetalheRepository.Delete(detalhe.Id);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



    }
}
