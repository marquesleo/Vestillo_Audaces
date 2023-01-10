using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class CfopController : GenericController<Cfop, CfopRepository>
    {
        public IEnumerable<Cfop> GetPorReferencia(String referencia, String TipoCfop)
        {
            using (CfopRepository repository = new CfopRepository())
            {
                return repository.GetPorReferencia(referencia, TipoCfop);
            }
        }

        public IEnumerable<Cfop> GetPorDescricao(String desc, String TipoCfop)
        {
            using (CfopRepository repository = new CfopRepository())
            {
                return repository.GetPorDescricao(desc, TipoCfop);
            }
        }
    }
}
