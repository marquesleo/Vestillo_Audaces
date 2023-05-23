using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Core.Models;
using Vestillo.Core.Repositories;
using System.Data;

namespace Vestillo.Core.Services
{
    public class AcompanhamentoCatalogoService : GenericService<AcompanhamentoCatalogoView, AcompanhamentoCatalogoRepository>
    {
        public IEnumerable<AcompanhamentoCatalogoView> ListRelatorioAcompanhamentoCatalgo(int catalogoId, int AlmoxarifadoId, decimal? percentualEstoqueIdeal, bool exibirCotacao = false)
        {
            return _repository.ListRelatorioAcompanhamentoCatalgo(catalogoId, AlmoxarifadoId, percentualEstoqueIdeal, exibirCotacao);
        }
    }
}


