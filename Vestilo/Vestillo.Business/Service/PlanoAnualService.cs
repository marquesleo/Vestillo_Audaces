﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.APP;
using Vestillo.Business.Service.Web;

namespace Vestillo.Business.Service
{
    public class PlanoAnualService: GenericService<PlanoAnual, PlanoAnualRepository, PlanoAnualController>
    {
        public PlanoAnualService()
        {
            base.RequestUri = "PlanoContas";
        }

        public new IPlanoAnualService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new PlanoAnualServiceWeb(this.RequestUri);
            }
            else
            {
                return new PlanoAnualServiceAPP();
            }
        }  
    }
}
