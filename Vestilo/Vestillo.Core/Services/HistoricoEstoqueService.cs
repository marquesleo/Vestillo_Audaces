
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Core.Models;
using Vestillo.Core.Repositories;

namespace Vestillo.Core.Services
{
    public class HistoricoEstoqueService : GenericService<HistoricoEstoqueView, HistoricoEstoqueRepository>
    {
        public IEnumerable<HistoricoEstoqueView> ListRelatorio(DateTime DaData, DateTime AteData, bool Agrupado = false)
        {
            return _repository.ListRelatorio(DaData, AteData, Agrupado);
        }

        public IEnumerable<HistoricoEstoqueView> ListPlanilha(DateTime DaData, DateTime AteData, int idAlmoxarifado)
        {
            return _repository.ListPlanilha(DaData, AteData, idAlmoxarifado);
        }

        public IEnumerable<UltimoPrecoMaterialView> UltimoPrecoMaterial(List<int> ids)
        {
            return _repository.UltimoPrecoMaterial(ids);
        }
    }
}


