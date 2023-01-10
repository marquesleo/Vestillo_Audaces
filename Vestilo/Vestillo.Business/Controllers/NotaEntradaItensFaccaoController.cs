
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class NotaEntradaItensFaccaoController : GenericController<NotaEntradaFaccaoItens, NotaEntradaFaccaoItensRepository>
    {
        public IEnumerable<NotaEntradaFaccaoItens> GetListByNotaEntrada(int IdNota)
        {
            using (NotaEntradaFaccaoItensRepository repository = new NotaEntradaFaccaoItensRepository())
            {
                return repository.GetListByNotaEntradaItens(IdNota);
            }
        }


        public IEnumerable<NotaEntradaFaccaoItensView> GetListByNotaEntradaItensViewItem(int IdNota)
        {
            using (NotaEntradaFaccaoItensRepository repository = new NotaEntradaFaccaoItensRepository())
            {
                return repository.GetListByNotaEntradaItensView(IdNota);
            }
        }

        public IEnumerable<NotaEntradaFaccaoItensView> GetListByNfeItensViewAgrupados(int IdNota)
        {
            using (NotaEntradaFaccaoItensRepository repository = new NotaEntradaFaccaoItensRepository())
            {
                return repository.GetListByNotaEntradaItensViewAgrupados(IdNota);
            }
        }


    }
}
