
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class NotaEntradaItensController : GenericController<NotaEntradaItens, NotaEntradaItensRepository>
    {
        public IEnumerable<NotaEntradaItens> GetListByNotaEntrada(int IdNota)
        {
            using (NotaEntradaItensRepository repository = new NotaEntradaItensRepository())
            {
                return repository.GetListByNotaEntradaItens(IdNota);
            }
        }




        public IEnumerable<NotaEntradaItensView> GetListByNotaEntradaItensViewItem(int IdNota)
        {
            using (NotaEntradaItensRepository repository = new NotaEntradaItensRepository())
            {
                return repository.GetListByNotaEntradaItensView(IdNota);
            }
        }

        public IEnumerable<NotaEntradaItensView> GetListByNfeItensViewAgrupados(int IdNota)
        {
            using (NotaEntradaItensRepository repository = new NotaEntradaItensRepository())
            {
                return repository.GetListByNotaEntradaItensViewAgrupados(IdNota);
            }
        }

        
    }
}
