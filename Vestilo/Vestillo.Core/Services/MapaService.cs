
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Core.Models;
using Vestillo.Core.Repositories;

namespace Vestillo.Core.Services
{
    public class MapaService : GenericService<MapaView, MapaRepository>
    {
        public IEnumerable<MapaView> ListRelatorioMapa(DateTime DaInspecao, DateTime AteInspecao, DateTime DoAgendamento, DateTime AteAgendamento, DateTime DaPrevisao, DateTime AtePrevisao, string NumeroOrdem, bool Agrupado = false, bool Agendamento = false, bool Inspecao = false, bool Previsao = false, bool SomenteAbertos = false, bool ExibirTempos = false)
        {
            return _repository.ListRelatorioMapa(DaInspecao, AteInspecao, DoAgendamento, AteAgendamento, DaPrevisao, AtePrevisao, NumeroOrdem, Agrupado, Agendamento, Inspecao, Previsao, SomenteAbertos, ExibirTempos);
        }
    }
}


