
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
    public class PercentuaisEmpresasAPP : GenericServiceAPP<PercentuaisEmpresas, PercentuaisEmpresasRepository, PercentuaisEmpresasController>, IPercentuaisEmpresasService
    {
        public PercentuaisEmpresasAPP() : base(new PercentuaisEmpresasController())
        {

        }
        public PercentuaisEmpresasView GetEmpresaLogada(int Id)
        {
            return controller.GetEmpresaLogada(Id);
        }


        public PercentuaisEmpresas GetByEmpresaLogada(int Id)
        {
            return controller.GetByEmpresaLogada(Id);
        }
    }
}



