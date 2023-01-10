
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Controllers;

namespace Vestillo.Business.Service
{
    public class CatalogoService : GenericService<Catalogo, CatalogoRepository, CatalogoController>
    {
        public CatalogoService()
        {
            base.RequestUri = "Catalogo";
        }

        public IEnumerable<Catalogo> GetListPorDescricao(string Descricao)
        {
            var controller = new CatalogoRepository();
            return controller.GetListPorDescricao(Descricao);
        }
    }
}