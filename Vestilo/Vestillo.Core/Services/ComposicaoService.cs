using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Core.Models;
using Vestillo.Core.Repositories;

namespace Vestillo.Core.Services
{
    public class ComposicaoService : GenericService<Composicao, ComposicaoRepository>
    {
        public void Save(IEnumerable<ComposicaoView> composicoes, IEnumerable<ComposicaoView> ComposicoesExcluidas = null)
        {
            bool openTransaction = false;

            try
            {
                openTransaction = _repository.BeginTransaction();

                foreach (ComposicaoView view in composicoes)
                {
                    Composicao atividade = (view as Composicao);
                    base.Save(atividade);
                }

                foreach (ComposicaoView view in ComposicoesExcluidas)
                {
                    _repository.Delete(view.Id);
                }

                if (openTransaction)
                    _repository.CommitTransaction();
            }
            catch (Exception ex)
            {
                if (openTransaction)
                    _repository.RollbackTransaction();
                throw ex;
            }
        }
        public IEnumerable<ComposicaoView> ListByProdutoETipoComposicao(int ProdutoId)
        {
            return _repository.ListByProdutoETipoComposicao(ProdutoId);
        }


    }
}

