
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Core.Models;
using Vestillo.Core.Repositories;

namespace Vestillo.Core.Services
{
    public class BalanceamnetoSemanalItensService : GenericService<BalanceamentoSemanalItens, BalanceamnetoSemanalItensRepository>
    {
        public void Save(IEnumerable<BalanceamentoSemanalItens> Balanceamento)
        {
            bool openTransaction = false;

            try
            {
                openTransaction = _repository.BeginTransaction();

                foreach (BalanceamentoSemanalItens view in Balanceamento)
                {
                    BalanceamentoSemanalItens BalItens = (view as BalanceamentoSemanalItens);
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

        public IEnumerable<BalanceamentoSemanalItensView> ListBySetores(int BalanceamentoId)
        {
            return _repository.ListBySetores(BalanceamentoId);
        }

        public void DeleteItensBalanco(int BalanceamentoId)
        {
            _repository.DeleteItensBalanco(BalanceamentoId);

        }


    }
}

