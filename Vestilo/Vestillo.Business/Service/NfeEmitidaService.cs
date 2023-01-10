

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service
{
    public class NfeEmitidaService : GenericService<NfeEmitida, NfeEmitidaRepository , NfeEmitidaController>
    {
        public NfeEmitidaService()
        {
            base.RequestUri = "Nfe";
        }

        public IEnumerable<RankingVenda> GetRankingVenda(FiltroRankingVendaRelatorio filtro)
        {
            using (NfeEmitidaController repository = new NfeEmitidaController())
            {
                return repository.GetRankingVenda(filtro);
            }
        }
    }
}