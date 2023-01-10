﻿using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.APP;
using Vestillo.Business.Service.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Service
{
    public class OcorrenciaFuncionarioService: GenericService<OcorrenciaFuncionario, OcorrenciaFuncionarioRepository, OcorrenciaFuncionarioController>
    {
        public OcorrenciaFuncionarioService()
        {
            base.RequestUri = "OcorrenciaFuncionario";
        }

        public new IOcorrenciaFuncionarioService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new OcorrenciaFuncionarioServiceWeb(this.RequestUri);
            }
            else
            {
                return new OcorrenciaFuncionarioServiceAPP();
            }
        }   
    }
}
