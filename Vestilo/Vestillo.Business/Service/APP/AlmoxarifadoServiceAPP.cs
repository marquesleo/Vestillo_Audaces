using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Service.APP
{
    public class AlmoxarifadoServiceAPP : GenericServiceAPP<Almoxarifado, AlmoxarifadoRepository, AlmoxarifadoController>, IAlmoxarifadoService
    {

        public AlmoxarifadoServiceAPP()
            : base(new AlmoxarifadoController())
        {
        }

        IEnumerable<Almoxarifado> IAlmoxarifadoService.GetListPorDescricao(string Descricao)
        {
            return controller.GetListPorDescricao(Descricao);
        }

        IEnumerable<Almoxarifado> IAlmoxarifadoService.GetByEmpresa()
        {
            return controller.GetByEmpresa();
        }
    }
}
