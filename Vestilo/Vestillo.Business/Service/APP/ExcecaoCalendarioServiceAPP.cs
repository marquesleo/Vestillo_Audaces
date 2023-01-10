
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.Interface;

namespace Vestillo.Business.Service.APP
{
    public class ExcecaoCalendarioServiceAPP: GenericServiceAPP<ExcecaoCalendario, ExcecaoCalendarioRepository, ExcecaoCalendarioController>, IExcecaoCalendarioService
    {
        public ExcecaoCalendarioServiceAPP() : base(new ExcecaoCalendarioController())
        {
        }

        public IEnumerable<ExcecaoCalendario> GetPorReferencia(string referencia)
        {
            ExcecaoCalendarioController controller = new ExcecaoCalendarioController();
            return controller.GetPorReferencia(referencia);
        }

        public IEnumerable<ExcecaoCalendario> GetPorDescricao(string desc)
        {
            ExcecaoCalendarioController controller = new ExcecaoCalendarioController();
            return controller.GetPorDescricao(desc);
        }

        public ExcecaoCalendario GetByDataExcecao(int CalendarioId, DateTime data)
        {
            ExcecaoCalendarioController controller = new ExcecaoCalendarioController();
            return controller.GetByDataExcecao(CalendarioId, data);
        }

    }
}
