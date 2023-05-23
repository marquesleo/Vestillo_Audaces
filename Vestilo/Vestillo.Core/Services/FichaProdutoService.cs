
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Core.Models;
using Vestillo.Core.Repositories;

namespace Vestillo.Core.Services
{
    public class FichaProdutoService : GenericService<ProdutoFicha, ProdutoFichaRepository>
    {
        public void Save(IEnumerable<ProdutoFichaView> Fichas, IEnumerable<ProdutoFichaView> FichasExcluidas = null)
        {
            bool openTransaction = false;

            try
            {
                openTransaction = _repository.BeginTransaction();

                foreach (ProdutoFichaView view in Fichas)
                {
                    ProdutoFicha medidas = (view as ProdutoFicha);
                    base.Save(medidas);
                }

                foreach (ProdutoFichaView view in FichasExcluidas)
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
        public IEnumerable<ProdutoFichaView> ListByProdutoFichas(int ProdutoId)
        {
            return _repository.ListByProdutoFichas(ProdutoId);
        }

        public void AtualizaProduto(int ProdutoId)
        {
            _repository.AtualizaProduto(ProdutoId);
        }
    }
}

