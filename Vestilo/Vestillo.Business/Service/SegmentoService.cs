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
    public class SegmentoService: GenericService<Segmento, SegmentoRepository, SegmentoController>
    {
        public SegmentoService()
        {
            base.RequestUri = "Segmento";
        }

        public IEnumerable<Segmento> GetListPorDescricao(string Descricao)
        {
            var controller = new SegmentoController();
            return controller.GetListPorDescricao(Descricao);
        }
    }
}
