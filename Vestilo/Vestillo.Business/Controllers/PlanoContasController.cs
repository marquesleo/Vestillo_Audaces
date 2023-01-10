using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class PlanoContasController: GenericController<PlanoContas, PlanoContasRepository>
    {
        public override void Delete(int id)
        {
            PlanoContasNaturezasFinanceirasRepository planoContasNaturezasFinanceirasRepository = new PlanoContasNaturezasFinanceirasRepository();

            try
            {
                planoContasNaturezasFinanceirasRepository.BeginTransaction();
                planoContasNaturezasFinanceirasRepository.DeleteByPlanoContas(id);
                base.Delete(id);
                planoContasNaturezasFinanceirasRepository.CommitTransaction();
            }
            catch (Exception ex)
            {
                planoContasNaturezasFinanceirasRepository.RollbackTransaction();
                throw ex;
            }
        }

        public void Save(ref PlanoContas planoContas, PlanoContas planoContasPai)
        {
            PlanoContasNaturezasFinanceirasRepository planoContasNaturezasFinanceirasRepository = new PlanoContasNaturezasFinanceirasRepository();
            bool openTransaction = false;

            try
            {
                openTransaction = planoContasNaturezasFinanceirasRepository.BeginTransaction();

                if (planoContasPai != null)
                {
                    planoContasNaturezasFinanceirasRepository.DeleteByPlanoContas(planoContasPai.Id);
                }
                
                Save(ref planoContas);

                if (openTransaction)
                    planoContasNaturezasFinanceirasRepository.CommitTransaction();
            }
            catch (Exception ex)
            {
                if (openTransaction)
                    planoContasNaturezasFinanceirasRepository.RollbackTransaction();

                throw ex;
            }
        }

        public override void Save(ref PlanoContas planoContas)
        {
            PlanoContasNaturezasFinanceirasRepository planoContasNaturezasFinanceirasRepository = new PlanoContasNaturezasFinanceirasRepository();
            bool openTransaction = false;

            try
            {
                openTransaction = planoContasNaturezasFinanceirasRepository.BeginTransaction();
                base.Save(ref planoContas);

                if (planoContas.Naturezas != null && planoContas.Naturezas.Count > 0)
                {
                    planoContasNaturezasFinanceirasRepository.DeleteByPlanoContas(planoContas.Id);
                    
                    foreach (NaturezaFinanceira natureza in planoContas.Naturezas)
                    {
                        PlanoContasNaturezasFinanceiras planoNatureza = new PlanoContasNaturezasFinanceiras();
                        planoNatureza.NaturezaFinanceiraId = natureza.Id;
                        planoNatureza.PlanoContasId = planoContas.Id;
                        planoContasNaturezasFinanceirasRepository.Save(ref planoNatureza);
                    }
                }
                
                if (openTransaction)
                    planoContasNaturezasFinanceirasRepository.CommitTransaction();
            }
            catch (Exception ex)
            {
                if(openTransaction)
                    planoContasNaturezasFinanceirasRepository.RollbackTransaction();
                
                throw ex;
            }
            
        }

        public IEnumerable<RelatorioPlanoContasView> GetRelatorioDespesasXReceitas(DateTime dataInicial, DateTime dataFinal)
        {
            return _repository.GetRelatorioDespesasXReceitas(dataInicial, dataFinal);
        }
    }
}
