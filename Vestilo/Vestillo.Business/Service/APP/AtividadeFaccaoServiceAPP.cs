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
    public class AtividadeFaccaoServiceAPP : GenericServiceAPP<AtividadeFaccao, AtividadeFaccaoRepository, AtividadeFaccaoController>, IAtividadeFaccaoService
    {

        public AtividadeFaccaoServiceAPP() : base(new AtividadeFaccaoController())
        {
        }

        public IEnumerable<AtividadeFaccao> GetByAtivos(int AtivoInativo)
        {
            return controller.GetByAtivos(AtivoInativo);
        }

        public IEnumerable<AtividadeFaccao> GetListPorDescricao(string desc)
        {
            AtividadeFaccaoController controller = new AtividadeFaccaoController();
            return controller.GetListPorDescricao(desc);
        }

        public IEnumerable<AtividadeFaccao> GetListPorReferencia(string referencia)
        {
            AtividadeFaccaoController controller = new AtividadeFaccaoController();
            return controller.GetListPorReferencia(referencia);
        }

        public IEnumerable<AtividadeFaccao> GetListById(int id)
        {
            AtividadeFaccaoController controller = new AtividadeFaccaoController();
            return controller.GetListById(id);
        }
    }
}



