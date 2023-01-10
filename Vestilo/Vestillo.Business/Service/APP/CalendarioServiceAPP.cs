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
    public class CalendarioServiceAPP : GenericServiceAPP<Calendario, CalendarioRepository, CalendarioController>, ICalendarioService
    {

        public CalendarioServiceAPP() : base(new CalendarioController())
        {
        }

    }
}



