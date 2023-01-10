
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
    public class ContadorRemessaServiceAPP : GenericServiceAPP<ContadorRemessa, ContadorRemessaRepository, ContadorRemessaController>, IContadorRemessaService
    {

        public ContadorRemessaServiceAPP() : base(new ContadorRemessaController())
        {
        }

        public int GetProximo(int IdBanco)
        {
            return controller.GetProximo(IdBanco);
        }

        public ContadorRemessa GetByBanco(int IdBanco)
        {
            return controller.GetByBanco(IdBanco);
        }
    }
}
