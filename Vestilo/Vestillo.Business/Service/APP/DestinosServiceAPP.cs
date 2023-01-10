using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service.APP
{
    public class DestinosServiceAPP : GenericServiceAPP<Destinos, DestinosRepository, DestinosController>, IDestinosService
    {

        public DestinosServiceAPP() : base(new DestinosController())
        {
        }

        public IEnumerable<Destinos> GetByAtivos(int AtivoInativo)
        {
            return controller.GetByAtivos(AtivoInativo);
        }

        public IEnumerable<Destinos> GetListPorDescricao(string desc)
        {
            DestinosController controller = new DestinosController();
            return controller.GetListPorDescricao(desc);
        }

        public IEnumerable<Destinos> GetListPorReferencia(string Abreviatura)
        {
            DestinosController controller = new DestinosController();
            return controller.GetListPorReferencia(Abreviatura);
        }

        public IEnumerable<Destinos> GetListById(int id)
        {
            DestinosController controller = new DestinosController();
            return controller.GetListById(id);
        }
    }
}



