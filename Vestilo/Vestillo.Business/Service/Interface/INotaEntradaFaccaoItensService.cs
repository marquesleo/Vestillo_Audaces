
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
    public interface INotaEntradaFaccaoItensService : IService<NotaEntradaFaccaoItens, NotaEntradaFaccaoItensRepository, NotaEntradaItensFaccaoController>
    {
        IEnumerable<NotaEntradaFaccaoItens> GetListByNotaEntradaItens(int IdNota);
        IEnumerable<NotaEntradaFaccaoItensView> GetListByNotaEntradaItensView(int IdNota);
        IEnumerable<NotaEntradaFaccaoItensView> GetListByNotaEntradaItensViewAgrupados(int IdNota);

    }
}
