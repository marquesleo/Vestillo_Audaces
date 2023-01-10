
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service
{
    public interface ICceService : IService<Cce, CceRepository, CceController>
    {
        int MaiorSeqNota(int Nota);
        IEnumerable<Cce> DadosDaUltimaCarta(int Nota, int seq);

    }
}
