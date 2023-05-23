
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Core.Models;
using Vestillo.Core.Repositories;

namespace Vestillo.Core.Services
{
    public class OrdemProducaoCoreService : GenericService<AcompanhamentoOrdemView, OrdemProducaoCoreRepository>
    {
       
        public IEnumerable<AcompanhamentoOrdemView> ListDadosOrdem(DateTime DataInicio, DateTime DataFim,string CorTalao)
        {
            return _repository.ListDadosOrdem(DataInicio, DataFim, CorTalao);
        }

    }
}

