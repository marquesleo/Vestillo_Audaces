

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class NfeEmitidaController : GenericController<NfeEmitida , NfeEmitidaRepository>
    {
        public IEnumerable<RankingVenda> GetRankingVenda(FiltroRankingVendaRelatorio filtro)
        {
            using (NfRepository repository = new NfRepository())
            {
                var rep = repository.GetRankingVenda(filtro);
                if(filtro.Ordernar == "valor")
                    rep = rep.OrderByDescending(p => p.Total);
                else if (filtro.Ordernar == "quantidade")
                    rep = rep.OrderByDescending(p => p.Quantidade);

                return rep;
            }
        }
    }
}
