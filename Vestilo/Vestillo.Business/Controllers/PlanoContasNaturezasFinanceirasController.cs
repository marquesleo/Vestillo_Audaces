using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class PlanoContasNaturezasFinanceirasController : GenericController<PlanoContasNaturezasFinanceiras, PlanoContasNaturezasFinanceirasRepository>
    {
        public void Save(List<PlanoContasNaturezasFinanceiras> planoContasNaturezasFinanceiras, int planoContasId)
        {
            PlanoContasNaturezasFinanceirasRepository repository = new PlanoContasNaturezasFinanceirasRepository();
            bool openTransaction = false;

            try
            {
                openTransaction = repository.BeginTransaction();

                repository.DeleteByPlanoContas(planoContasId);

                planoContasNaturezasFinanceiras = planoContasNaturezasFinanceiras ?? new List<PlanoContasNaturezasFinanceiras>();

                foreach (PlanoContasNaturezasFinanceiras planoNatureza in planoContasNaturezasFinanceiras)
                {
                    var p = planoNatureza;
                    repository.Save(ref p);
                }
                
                if (openTransaction)
                    repository.CommitTransaction();
            }
            catch (Exception ex)
            {
                if (openTransaction)
                    repository.RollbackTransaction();
                
                throw ex;
            }
            finally
            {
                repository.Dispose();
            }
        }

        public IEnumerable<PlanoContasNaturezasFinanceirasView> GetRelatorioPlanoContas(DateTime dataInicial, DateTime dataFinal)
        {
            using (PlanoContasNaturezasFinanceirasRepository repository = new PlanoContasNaturezasFinanceirasRepository())
            {
                return repository.GetRelatorioPlanoContas(dataInicial, dataFinal);
            }
        }
    }
}
