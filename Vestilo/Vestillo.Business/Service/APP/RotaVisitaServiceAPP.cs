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
    public class RotaVisitaServiceAPP: GenericServiceAPP<RotaVisita, RotaVisitaRepository, RotaVisitaController>, IRotaVisitaService
    {
        public RotaVisitaServiceAPP()
            : base(new RotaVisitaController())
        {
        }

        public IEnumerable<RotaVisita> GetPorReferencia(string referencia)
        {
            RotaVisitaController controller = new RotaVisitaController();
            return controller.GetPorReferencia(referencia);
        }

        public IEnumerable<RotaVisita> GetPorDescricao(string desc)
        {
            RotaVisitaController controller = new RotaVisitaController();
            return controller.GetPorDescricao(desc);
        }

        public IEnumerable<RotaVisita> GetByIdList(int id)
        {
            RotaVisitaController controller = new RotaVisitaController();
            return controller.GetByIdList(id);
        }
    }
}
