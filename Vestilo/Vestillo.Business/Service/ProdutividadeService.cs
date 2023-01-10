using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.APP;
using Vestillo.Business.Service.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Service
{
    public class ProdutividadeService: GenericService<Produtividade, ProdutividadeRepository, ProdutividadeController>
    {
        public ProdutividadeService()
        {
            base.RequestUri = "Produtividade";
        }

        public new IProdutividadeService GetServiceFactory()
        {
            if (VestilloSession.TipoAcesso == VestilloSession.TipoAcessoDados.WebAPI)
            {
                return new ProdutividadeServiceWeb(this.RequestUri);
            }
            else
            {
                return new ProdutividadeServiceAPP();
            }
        }   
    }
}
