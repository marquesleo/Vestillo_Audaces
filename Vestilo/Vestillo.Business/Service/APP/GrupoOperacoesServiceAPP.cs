using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Service.APP
{
    public class GrupoOperacoesServiceAPP : GenericServiceAPP<GrupoOperacoes, GrupoOperacoesRepository, GrupoOperacoesController>, IGrupoOperacoesService
    {
        public GrupoOperacoesServiceAPP()
            : base(new GrupoOperacoesController())
        {

        }

        public List<GrupoOperacoesView> GetListByGrupoPacote(int grupoPacoteId)
        {
            GrupoOperacoesController controller = new GrupoOperacoesController();
            return controller.GetListByGrupoPacote(grupoPacoteId);
        }


        public List<GrupoOperacoesView> GetListByPacoteView(string pacoteRef)
        {
            GrupoOperacoesController controller = new GrupoOperacoesController();
            return controller.GetListByPacoteView(pacoteRef);
        }


        public List<GrupoOperacoesView> GetListByPacotesView(List<int> pacotesId)
        {
            GrupoOperacoesController controller = new GrupoOperacoesController();
            return controller.GetListByPacotesView(pacotesId);
        }


        public GrupoOperacoesView GetListByPacoteESequneciaView(string pacoteRef, string sequencia)
        {
            GrupoOperacoesController controller = new GrupoOperacoesController();
            return controller.GetListByPacoteESequneciaView(pacoteRef, sequencia);
        }


        public List<GrupoOperacoesView> GetListByPacoteView(string pacoteRef, int setorId)
        {
            GrupoOperacoesController controller = new GrupoOperacoesController();
            return controller.GetListByPacoteView(pacoteRef, setorId);
        }

        public List<GrupoOperacoesView> GetListByPacoteView(string pacoteRef, string sequencia)
        {
            GrupoOperacoesController controller = new GrupoOperacoesController();
            return controller.GetListByPacoteView(pacoteRef, sequencia);
        }


        public List<GrupoOperacoesView> GetListByGrupoPacoteVisualizar(int grupoPacoteId, int pacoteId)
        {
            GrupoOperacoesController controller = new GrupoOperacoesController();
            return controller.GetListByGrupoPacoteVisualizar(grupoPacoteId, pacoteId);
        }


        public List<GrupoOperacoesView> GetListByPacotesViewSemFicha(List<int> pacotesId)
        {
            GrupoOperacoesController controller = new GrupoOperacoesController();
            return controller.GetListByPacotesViewSemFicha(pacotesId);
        }
    }
}
