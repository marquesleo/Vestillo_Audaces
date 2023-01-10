
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class SerieNfceController : GenericController<SerieNfce, SerieNfceRepository>
    {

        public SerieNfce GetByNumeracao(int SerieNota)
        {

            using (SerieNfceRepository repository = new SerieNfceRepository())
            {
                return repository.GetByNumeracao(SerieNota);
            }
        }
    }
}
