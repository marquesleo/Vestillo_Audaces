using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TemplateAudacesApi.Models;
using Vestillo.Business.Models;
using Vestillo.Business.Service;

namespace TemplateAudacesApi.Services
{
    public class GradeDeProdutoService
    {
        private IProdutoDetalheService _produtoDetalheService;
        private IProdutoDetalheService produtoDetalheService
        {
            get
            {
                if (_produtoDetalheService == null)
                    _produtoDetalheService = new ProdutoDetalheService().GetServiceFactory();

                return _produtoDetalheService;
            }
        }
              

        public void IncluirDetalhe(Produto produto,Cor cor, Tamanho tamanho)
        {
            try
            {         
               var detalhe = new ProdutoDetalhe();
               detalhe.DataAlteracao = DateTime.Now;
               detalhe.Idcor = cor.Id;
               detalhe.IdProduto = produto.Id;
               detalhe.IdTamanho = tamanho.Id;
               produtoDetalheService.Save(ref detalhe);
        
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
                var lstDetalhe = produtoDetalheService.GetListByProduto(produto.Id, 1);
                foreach (var detalhe in lstDetalhe)
                {
                    produtoDetalheService.Delete(detalhe.Id);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }



    }
}
