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
    public class FichaFaccaoServiceApp : GenericServiceAPP<FichaFaccao, FichaFaccaoRepository, FichaFaccaoController>, IFichaFaccaoService
    {
        public FichaFaccaoServiceApp() : base(new FichaFaccaoController())
        {

        }

        public IEnumerable<FichaFaccao> VerificaFaccao(List<int> idPacote, List<int> idProduto, List<int> idFaccao)
        {
            FichaFaccaoController controller = new FichaFaccaoController();
            return controller.VerificaFaccao(idPacote, idProduto, idFaccao);
        }
        public IEnumerable<FichaFaccao> GetByIdFicha(int idFicha)
        {
            FichaFaccaoController controller = new FichaFaccaoController();
            return controller.GetByIdFicha(idFicha);            
        }
        public IEnumerable<FichaFaccao> GetByIdFaccao(int idFaccao)
        {
            FichaFaccaoController controller = new FichaFaccaoController();
            return controller.GetByIdFaccao(idFaccao);            
        }

        public IEnumerable<FichaFaccaoView> GetByIdFichaView(int idFicha)
        {
            FichaFaccaoController controller = new FichaFaccaoController();
            return controller.GetByIdFichaView(idFicha);
        }
    }
}