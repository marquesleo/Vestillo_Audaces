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
    public class FichaTecnicaDoMaterialItemServiceAPP : GenericServiceAPP<FichaTecnicaDoMaterialItem,
                                                                       FichaTecnicaDoMaterialItemRepository,
                                                                       FichaTecnicaDoMaterialItemController>, IFichaTecnicaDoMaterialItemService
    {

        public FichaTecnicaDoMaterialItemServiceAPP()
            : base(new FichaTecnicaDoMaterialItemController())
        {

        }

        public IEnumerable<FichaTecnicaDoMaterialItem> GetAllViewByFichaTecnica(int FichaTecnicaId)
        {
            return controller.GetAllViewByFichaTecnica(FichaTecnicaId);
        }


        public void ExcluirRelacao(int FichaTecnicaId)
        {
            controller.ExcluirRelacao(FichaTecnicaId);
        }


        public IEnumerable<FichaTecnicaDoMaterialItem> GetAllViewByProdutoId(int produtoId)
        {
            return controller.GetAllViewByProdutoId(produtoId);
        }

        public IEnumerable<FichaTecnicaDoMaterialItem> GetListByProduto(int produto)
        {
            return controller.GetListByProduto(produto);
        }

        public List<MontagemDaFichaTecnicaDoMaterialView> getMontagemDaFichaTecnicaDoMaterial(FichaTecnicaDoMaterial fichaTecnica)
        {
            return controller.getMontagemDaFichaTecnicaDoMaterial(fichaTecnica);
        }

        public IEnumerable<FichaTecnicaDoMaterialItem> GetListByMateriaPrima(int materiaId)
        {
            return controller.GetListByMateriaPrima(materiaId);
        }
    }
}
