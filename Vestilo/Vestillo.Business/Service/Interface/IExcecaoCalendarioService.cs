
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service
{
    public interface IExcecaoCalendarioService : IService<ExcecaoCalendario, ExcecaoCalendarioRepository, ExcecaoCalendarioController>
    {
        IEnumerable<ExcecaoCalendario> GetPorReferencia(String referencia);
        IEnumerable<ExcecaoCalendario> GetPorDescricao(String desc);       
        ExcecaoCalendario GetByDataExcecao(int CalendarioId, DateTime data);
    }
}
