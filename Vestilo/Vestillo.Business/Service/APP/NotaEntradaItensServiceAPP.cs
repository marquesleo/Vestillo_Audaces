

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service.APP
{
    public class NotaEntradaItensServiceAPP: GenericServiceAPP<NotaEntradaItens, NotaEntradaItensRepository, NotaEntradaItensController>, INotaEntradaItensService
    {
        public NotaEntradaItensServiceAPP() : base(new NotaEntradaItensController())
        {
        }

        public IEnumerable<NotaEntradaItens> GetListByNotaEntradaItens(int IdNota)
        {
            return controller.GetListByNotaEntrada(IdNota);
        }



        public IEnumerable<NotaEntradaItensView> GetListByNotaEntradaItensView(int IdNota)
        {
            return controller.GetListByNotaEntradaItensViewItem(IdNota);
        }


        public IEnumerable<NotaEntradaItensView> GetListByNotaEntradaItensViewAgrupados(int IdNota)
        {
            return controller.GetListByNfeItensViewAgrupados(IdNota);
        }
        
    }
}
