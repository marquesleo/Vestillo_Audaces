
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
    public interface INotaEntradaItensService : IService<NotaEntradaItens, NotaEntradaItensRepository, NotaEntradaItensController>
    {
        IEnumerable<NotaEntradaItens> GetListByNotaEntradaItens(int IdNota);      
        IEnumerable<NotaEntradaItensView> GetListByNotaEntradaItensView(int IdNota);
        IEnumerable<NotaEntradaItensView> GetListByNotaEntradaItensViewAgrupados(int IdNota);
        
    }
}
