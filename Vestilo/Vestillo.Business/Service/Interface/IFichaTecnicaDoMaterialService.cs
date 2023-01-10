using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Models.Views;

namespace Vestillo.Business.Service.Interface
{
    public interface IFichaTecnicaDoMaterialService : IService< FichaTecnicaDoMaterial,
                                                                FichaTecnicaDoMaterialRepository, 
                                                                FichaTecnicaDoMaterialController>
    {

        IEnumerable<FichaTecnicaDoMaterialView> GetAllView();

        IEnumerable<FichaTecnicaDoMaterial> GetAllView(List<int> lstProdutos);

        FichaTecnicaDoMaterial GetByProduto(int produtoId);

        IEnumerable<FichaTecnicaDoMaterial> GetAllViewByFiltro(FiltroCustoProdutoAnalitico filtro);
        IEnumerable<FichaTecnicaDoMaterial> GetAllViewByFiltro(FiltroCustoProdutoSintetico filtro);
        IEnumerable<FichaTecnicaDoMaterialView> GetAllViewByFiltro(FiltroFichaTecnica filtro);

    }
}
