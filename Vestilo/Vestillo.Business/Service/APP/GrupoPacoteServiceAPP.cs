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
    class GrupoPacoteServiceAPP : GenericServiceAPP<GrupoPacote, GrupoPacoteRepository, GrupoPacoteController>, IGrupoPacoteService
    {
        public GrupoPacoteServiceAPP()
            : base(new GrupoPacoteController())
        {

        }
    }
}
