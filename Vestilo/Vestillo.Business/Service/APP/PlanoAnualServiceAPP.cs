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
    public class PlanoAnualServiceAPP: GenericServiceAPP<PlanoAnual, PlanoAnualRepository, PlanoAnualController>, IPlanoAnualService
    {
        public PlanoAnualServiceAPP()
            : base(new PlanoAnualController())
        {

        }

        public List<PlanoAnualDetalhesView> GetPlanoAnualDetalhesTotal(int codigo)
        {
            return controller.GetPlanoAnualDetalhesTotal(codigo);
        }


        public List<PlanoAnualDetalhesView> GetPlanoAnualDetalhes(int codigo)
        {
            return controller.GetPlanoAnualDetalhes(codigo);
        }


        public List<GrupProduto> GetGrupos(int codigo)
        {
            return controller.GetGrupos(codigo);
        }
    }
}
