
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class CceController : GenericController<Cce, CceRepository>
    {
        public int MaiorSeqNota(int Nota)
        {
            using (CceRepository repository = new CceRepository())
            {
                return repository.MaiorSeqNota(Nota);
            }
        }

        public IEnumerable<Cce> DadosDaUltimaCarta(int Nota, int seq)
        {
            using (CceRepository repository = new CceRepository())
            {
                return repository.DadosDaUltimaCarta(Nota, seq);  
            }
        }

    }
}