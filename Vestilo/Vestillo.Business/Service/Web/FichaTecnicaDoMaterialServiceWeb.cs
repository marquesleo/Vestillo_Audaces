using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.Interface;
using Vestillo.Lib;

namespace Vestillo.Business.Service.Web
{
    public class FichaTecnicaDoMaterialServiceWeb : GenericServiceWeb<FichaTecnicaDoMaterial, 
                                                                      FichaTecnicaDoMaterialRepository, 
                                                                      FichaTecnicaDoMaterialController>, 
                                                                      IFichaTecnicaDoMaterialService
    {

        public FichaTecnicaDoMaterialServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }
            

        public IEnumerable<FichaTecnicaDoMaterialView> GetAllView()
        {
            var c = new ConnectionWebAPI<FichaTecnicaDoMaterialView>(VestilloSession.UrlWebAPI);
            return c.Get(this.RequestUri);
        }


        public IEnumerable<FichaTecnicaDoMaterial> GetAllView(List<int> lstProdutos)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<FichaTecnicaDoMaterial> GetAllViewByFiltro(Models.Views.FiltroCustoProdutoAnalitico filtro)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<FichaTecnicaDoMaterial> GetAllViewByFiltro(Models.Views.FiltroCustoProdutoSintetico filtro)
        {
            throw new NotImplementedException();
        }

        public FichaTecnicaDoMaterial GetByProduto(int produtoId)
        {
            throw new NotImplementedException();
        }


        public IEnumerable<FichaTecnicaDoMaterialView> GetAllViewByFiltro(Models.Views.FiltroFichaTecnica filtro)
        {
            throw new NotImplementedException();
        }
    }
}
