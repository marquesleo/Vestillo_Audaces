

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
    public class SerieNfceServiceAPP : GenericServiceAPP<SerieNfce , SerieNfceRepository, SerieNfceController>, ISerieNfceService 
    {

        public SerieNfceServiceAPP() : base(new SerieNfceController())
        {
        }

        public SerieNfce GetByNumeracao(int SerieNota)
        {
            return controller.GetByNumeracao(SerieNota);
        }
    }
}


