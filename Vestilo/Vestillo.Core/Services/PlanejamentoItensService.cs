
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Core.Models;
using Vestillo.Core.Repositories;

namespace Vestillo.Core.Services
{
    public class PlanejamentoItensService : GenericService<PlanejamentoItens, PlanejamentoItensRepository>
    {
        public void Save(IEnumerable<PlanejamentoItens> Medidas, IEnumerable<PlanejamentoItens> MedidasExcluidas = null)
        {
            bool openTransaction = false;

            try
            {
                openTransaction = _repository.BeginTransaction();

                foreach (PlanejamentoItens view in Medidas)
                {
                    PlanejamentoItens PlanItens = (view as PlanejamentoItens);
                    base.Save(PlanItens);
                }

                foreach (PlanejamentoItens view in MedidasExcluidas)
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
        public IEnumerable<PlanejamentoItens> ListBySemanas(int PlanejamentoId)
        {
            return _repository.ListBySemanas(PlanejamentoId);
        }


    }
}

