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
    public interface IPlanoAnualService : IService<PlanoAnual, PlanoAnualRepository, PlanoAnualController>
    {
        List<PlanoAnualDetalhesView> GetPlanoAnualDetalhesTotal(int codigo); 
        List<PlanoAnualDetalhesView> GetPlanoAnualDetalhes(int codigo);
        List<GrupProduto> GetGrupos(int codigo);
    }
}
