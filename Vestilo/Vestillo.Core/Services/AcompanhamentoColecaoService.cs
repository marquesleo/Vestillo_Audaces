
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
    public class AcompanhamentoColecaoService : GenericService<AcompanhamentoColecaoView, AcompanhamentoColecaoRepository>
    {
        public IEnumerable<AcompanhamentoColecaoView> ListRelatorioAcompanhamentoColecao(List<int> catalogoId, int AlmoxarifadoId, decimal? percentualEstoqueIdeal,  DateTime DataInicio, DateTime DataFim, int[] corIds, bool exibirCotacao = false)
        {
            return _repository.ListRelatorioAcompanhamentoColecao(catalogoId, AlmoxarifadoId, percentualEstoqueIdeal, DataInicio, DataFim, corIds,exibirCotacao);
        }
    }
}


