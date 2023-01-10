using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Controllers;
using Vestillo.Business.Repositories;
using Vestillo.Lib;
using Vestillo.Business.Service.Interface;

namespace Vestillo.Business.Service.Web
{
    public class FichaTecnicaDoMaterialItemWeb : GenericServiceWeb<FichaTecnicaDoMaterialItem,
                                                                     FichaTecnicaDoMaterialItemRepository,
                                                                     FichaTecnicaDoMaterialItemController>, IFichaTecnicaDoMaterialItemService
    {
        public FichaTecnicaDoMaterialItemWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public IEnumerable<FichaTecnicaDoMaterialItem> GetAllViewByFichaTecnica(int FichaTecnicaId)
        {
            throw new NotImplementedException();
        }


        public void ExcluirRelacao(int FichaTecnicaId)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<FichaTecnicaDoMaterialItem> GetAllViewByProdutoId(int produtoId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FichaTecnicaDoMaterialItem> GetListByProduto(int produto)
        {
            throw new NotImplementedException();
        }

        public List<MontagemDaFichaTecnicaDoMaterialView> getMontagemDaFichaTecnicaDoMaterial(FichaTecnicaDoMaterial fichaTecnica)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FichaTecnicaDoMaterialItem> GetListByMateriaPrima(int materiaId)
        {
            throw new NotImplementedException();
        }
    }
}
