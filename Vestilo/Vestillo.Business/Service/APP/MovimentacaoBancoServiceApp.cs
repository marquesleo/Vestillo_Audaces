
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Models.Views;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.Interface;

namespace Vestillo.Business.Service.APP
{
    public class MovimentacaoBancoServiceApp : GenericServiceAPP<MovimentacaoBanco, MovimentacaoBancoRepository, MovimentacaoBancoController>, IMovimentacaoBancoService
    {
        public MovimentacaoBancoServiceApp()  : base(new MovimentacaoBancoController())
        {
        }

        public IEnumerable<MovimentacaoBancoView> GetCamposBrowse()
        {
            return controller.GetCamposBrowse();
        }

        public IEnumerable<MovimentacaoBancoView> GetRelExtratoBancarioBrowse(FiltroExtratoBancarioRel filtro)
        {

            MovimentacaoBancoController controller = new MovimentacaoBancoController();
            return controller.GetRelExtratoBancarioBrowse(filtro);
        }

    }
}