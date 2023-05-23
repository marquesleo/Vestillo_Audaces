

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Core.Models;
using Vestillo.Core.Repositories;

namespace Vestillo.Core.Services
{
    public class BalanceamentoSemanaItensDetalhesService : GenericService<BalanceamentoSemanaItensDetalhes, BalanceamentoSemanaItensDetalhesRepository>
    {
        public void Save(IEnumerable<BalanceamentoSemanaItensDetalhes> Balanceamento)
        {
            bool openTransaction = false;

            try
            {
                openTransaction = _repository.BeginTransaction();

                foreach (BalanceamentoSemanaItensDetalhes view in Balanceamento)
                {
                    BalanceamentoSemanaItensDetalhes BalItens = (view as BalanceamentoSemanaItensDetalhes);
                    base.Save(BalItens);
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

        public IEnumerable<BalanceamentoSemanaItensDetalhesView> ListBySetoresDetalhes(int BalanceamentoId)
        {
            return _repository.ListBySetoresDetalhes(BalanceamentoId);
        }

        public void DeleteItensDetalhesBalanco(int BalanceamentoId)
        {
            _repository.DeleteItensDetalhesBalanco(BalanceamentoId);

        }


    }
}

