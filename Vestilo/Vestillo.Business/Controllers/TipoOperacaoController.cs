
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class TipoOperacaoController : GenericController<TipoOperacao, TipoOperacaoRepository>
    {
        public IEnumerable<TipoOperacao> GetByAtivos(int AtivoInativo)
        {
            using (TipoOperacaoRepository repository = new TipoOperacaoRepository())
            {
                return repository.GetByAtivos(AtivoInativo);
            }
        }

    }
}
