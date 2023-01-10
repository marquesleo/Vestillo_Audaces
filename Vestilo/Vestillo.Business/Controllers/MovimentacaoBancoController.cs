
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class MovimentacaoBancoController : GenericController<MovimentacaoBanco, MovimentacaoBancoRepository>
    {
        public IEnumerable<MovimentacaoBancoView> GetCamposBrowse()
        {
            using (MovimentacaoBancoRepository repository = new MovimentacaoBancoRepository())
            {
                return repository.GetCamposBrowse();
            }
        }


        public IEnumerable<MovimentacaoBancoView> GetRelExtratoBancarioBrowse(FiltroExtratoBancarioRel filtro)
        {

            using (MovimentacaoBancoRepository repository = new MovimentacaoBancoRepository())
            {
                return repository.GetRelExtratoBancarioBrowse(filtro);
            }
        }

    }
}
