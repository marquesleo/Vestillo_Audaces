using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Controllers;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.Interface;
using Vestillo.Lib;

namespace Vestillo.Business.Service.Web
{
    public class BoletosGeradosServiceWeb : GenericServiceWeb<BoletosGerados, BoletosGeradosRepository, BoletosGeradosController>, IBoletosGeradosService
    {

        public BoletosGeradosServiceWeb(string requestUri) : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public void Save(List<BoletosGerados> boletos)
        {
            BoletosGeradosController controller = new BoletosGeradosController();
            controller.Save(boletos);
        }

        public BoletosGerados GetViewByIdTitulo(int idTitulo)
        {
            throw new NotImplementedException();
        }

        public void DeleteBoletoPeloTitulo(int idTitulo)
        {
            throw new NotImplementedException();
        }

        public BoletosGerados GetViewByNossoNumero(string NossoNumero)
        {
            throw new NotImplementedException();
        }

        public BoletosGerados GetViewByNumDocumento(string NumDocumento)
        {
            throw new NotImplementedException();
        }
    }
}


