
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
    public class BoletosGeradosServiceAPP : GenericServiceAPP<BoletosGerados, BoletosGeradosRepository, BoletosGeradosController>, IBoletosGeradosService
    {
        public BoletosGeradosServiceAPP() : base(new BoletosGeradosController())
        {

        }

        public void Save(List<BoletosGerados> boletos)
        {
            BoletosGeradosController controller = new BoletosGeradosController();
            controller.Save(boletos);
        }

        public BoletosGerados GetViewByIdTitulo(int idTitulo)
        {
            BoletosGeradosController controller = new BoletosGeradosController();
            return controller.GetViewByIdTitulo(idTitulo);
        }

        public void DeleteBoletoPeloTitulo(int idTitulo)
        {
            BoletosGeradosController controller = new BoletosGeradosController();
            controller.DeleteBoletoPeloTitulo(idTitulo);
            
        }

        public BoletosGerados GetViewByNossoNumero(string NossoNumero)
        {
            BoletosGeradosController controller = new BoletosGeradosController();
            return controller.GetViewByNossoNumero(NossoNumero);
        }

        public BoletosGerados GetViewByNumDocumento(string NumDocumento)
        {
            BoletosGeradosController controller = new BoletosGeradosController();
            return controller.GetViewByNumDocumento(NumDocumento);
        }

    }
}



