using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Service.Web
{
    public class AlmoxarifadoServiceWeb : GenericServiceWeb<Almoxarifado, AlmoxarifadoRepository, AlmoxarifadoController>, IAlmoxarifadoService
    {

        public AlmoxarifadoServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<Almoxarifado> GetListPorDescricao(string Descricao)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Almoxarifado> GetByEmpresa()
        {
            throw new NotImplementedException();
        }
    }
}
