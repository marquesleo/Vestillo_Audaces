using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.Interface;

namespace Vestillo.Business.Service.APP
{
    public class FichaTecnicaDoMaterialServiceAPP : GenericServiceAPP<FichaTecnicaDoMaterial, 
                                                                      FichaTecnicaDoMaterialRepository, 
                                                                      FichaTecnicaDoMaterialController>, IFichaTecnicaDoMaterialService
    {
        public FichaTecnicaDoMaterialServiceAPP()
            : base(new FichaTecnicaDoMaterialController())
        {

        }

        public IEnumerable<FichaTecnicaDoMaterialView> GetAllView()
        {
            return controller.GetAllView();
        }


        public IEnumerable<FichaTecnicaDoMaterial> GetAllView(List<int> lstProdutos)
        {
            return controller.GetAllView(lstProdutos);
        }


        public IEnumerable<FichaTecnicaDoMaterial> GetAllViewByFiltro(Models.Views.FiltroCustoProdutoAnalitico filtro)
        {
            return controller.GetAllViewByFiltro(filtro);
        }

        public IEnumerable<FichaTecnicaDoMaterial> GetAllViewByFiltro(Models.Views.FiltroCustoProdutoSintetico filtro)
        {
            return controller.GetAllViewByFiltro(filtro);
        }

        public FichaTecnicaDoMaterial GetByProduto(int produtoId)
        {
            return controller.GetByProduto(produtoId);
        }


        public IEnumerable<FichaTecnicaDoMaterialView> GetAllViewByFiltro(Models.Views.FiltroFichaTecnica filtro)
        {
            return controller.GetAllViewByFiltro(filtro);
        }
    }
}
