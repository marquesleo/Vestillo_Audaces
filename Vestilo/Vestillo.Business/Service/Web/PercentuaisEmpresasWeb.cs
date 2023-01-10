
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
    public class PercentuaisEmpresasWeb : GenericServiceWeb<PercentuaisEmpresas, PercentuaisEmpresasRepository, PercentuaisEmpresasController>, IPercentuaisEmpresasService
    {

        public PercentuaisEmpresasWeb(string requestUri) : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public PercentuaisEmpresasView GetEmpresaLogada(int Id)
        {
            throw new NotImplementedException();
        }


        public PercentuaisEmpresas GetByEmpresaLogada(int Id)
        {
            throw new NotImplementedException();
        }
    }
}


