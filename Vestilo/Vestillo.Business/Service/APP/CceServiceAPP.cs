
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
    public class CceServiceAPP: GenericServiceAPP<Cce, CceRepository, CceController>, ICceService
    {
        public CceServiceAPP()   : base(new CceController())
        {
        }

        public int MaiorSeqNota(int Nota)
        {
            CceController controller = new CceController();
            return controller.MaiorSeqNota(Nota);
        }

        public IEnumerable<Cce> DadosDaUltimaCarta(int Nota, int seq)
        {
            CceController controller = new CceController();
            return controller.DadosDaUltimaCarta(Nota,seq);
        }


    }
}
