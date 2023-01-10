
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
    public class NotaEntradaFaccaoItensServiceAPP : GenericServiceAPP<NotaEntradaFaccaoItens, NotaEntradaFaccaoItensRepository, NotaEntradaItensFaccaoController>, INotaEntradaFaccaoItensService
    {
        public NotaEntradaFaccaoItensServiceAPP() : base(new NotaEntradaItensFaccaoController())
        {
        }

        public IEnumerable<NotaEntradaFaccaoItens> GetListByNotaEntradaItens(int IdNota)
        {
            return controller.GetListByNotaEntrada(IdNota);
        }



        public IEnumerable<NotaEntradaFaccaoItensView> GetListByNotaEntradaItensView(int IdNota)
        {
            return controller.GetListByNotaEntradaItensViewItem(IdNota);
        }


        public IEnumerable<NotaEntradaFaccaoItensView> GetListByNotaEntradaItensViewAgrupados(int IdNota)
        {
            return controller.GetListByNfeItensViewAgrupados(IdNota);
        }

    }
}

