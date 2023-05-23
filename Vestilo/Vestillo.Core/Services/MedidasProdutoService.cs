
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Core.Models;
using Vestillo.Core.Repositories;

namespace Vestillo.Core.Services
{
    public class MedidasProdutoService : GenericService<MedidasProduto, MedidasProdutoRepository>
    {
        public void Save(IEnumerable<MedidasProdutoView> Medidas, IEnumerable<MedidasProdutoView> MedidasExcluidas = null)
        {
            bool openTransaction = false;

            try
            {
                openTransaction = _repository.BeginTransaction();

                foreach (MedidasProdutoView view in Medidas)
                {
                    MedidasProduto medidas = (view as MedidasProduto);
                    base.Save(medidas);
                }

                foreach (MedidasProdutoView view in MedidasExcluidas)
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
        public IEnumerable<MedidasProdutoView> ListByProdutoMedidas(int ProdutoId)
        {
            return _repository.ListByProdutoMedidas(ProdutoId);
        }


    }
}

