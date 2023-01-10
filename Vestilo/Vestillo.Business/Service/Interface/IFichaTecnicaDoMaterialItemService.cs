using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Controllers;
using Vestillo.Business.Repositories;
using Vestillo.Lib;

namespace Vestillo.Business.Service.Interface
{
    public interface IFichaTecnicaDoMaterialItemService : IService<FichaTecnicaDoMaterialItem, 
                                                                     FichaTecnicaDoMaterialItemRepository, 
                                                                     FichaTecnicaDoMaterialItemController>
    {

        IEnumerable<FichaTecnicaDoMaterialItem> GetAllViewByFichaTecnica(int FichaTecnicaId);
        IEnumerable<FichaTecnicaDoMaterialItem> GetAllViewByProdutoId(int produtoId);
        IEnumerable<FichaTecnicaDoMaterialItem> GetListByProduto(int produto);
        List<MontagemDaFichaTecnicaDoMaterialView> getMontagemDaFichaTecnicaDoMaterial(FichaTecnicaDoMaterial fichaTecnica);

        void ExcluirRelacao(int FichaTecnicaId);
        IEnumerable<FichaTecnicaDoMaterialItem> GetListByMateriaPrima(int materiaId);
    }
}
