

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
    public class SerieNfeServiceAPP : GenericServiceAPP<SerieNfe , SerieNfeRepository, SerieNfeController>, ISerieNfeService 
    {

        public SerieNfeServiceAPP() : base(new SerieNfeController())
        {
        }

        public SerieNfe GetByNumeracao(int SerieNota)
        {
            return controller.GetByNumeracao(SerieNota);
        }
    }
}


