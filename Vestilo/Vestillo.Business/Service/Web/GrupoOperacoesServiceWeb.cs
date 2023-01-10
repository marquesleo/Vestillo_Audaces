using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Service.Web
{
    public class GrupoOperacoesServiceWeb : GenericServiceWeb<GrupoOperacoes, GrupoOperacoesRepository, GrupoOperacoesController>, IGrupoOperacoesService
    {
        public GrupoOperacoesServiceWeb(string requestUri)
            : base(requestUri)
        {
            this.RequestUri = requestUri;
        }

        public List<GrupoOperacoesView> GetListByGrupoPacote(int grupoPacoteId)
        {
            throw new NotImplementedException();
        }

        public List<GrupoOperacoesView> GetListByPacoteView(string pacoteRef)
        {
            throw new NotImplementedException();
        }


        public List<GrupoOperacoesView> GetListByPacotesView(List<int> pacotesId)
        {
            throw new NotImplementedException();
        }


        public GrupoOperacoesView GetListByPacoteESequneciaView(string pacoteRef, string sequencia)
        {
            throw new NotImplementedException();
        }


        public List<GrupoOperacoesView> GetListByPacoteView(string pacoteRef, int setorId)
        {
            throw new NotImplementedException();
        }

        public List<GrupoOperacoesView> GetListByPacoteView(string pacoteRef, string sequencia)
        {
            throw new NotImplementedException();
        }

        public List<GrupoOperacoesView> GetListByGrupoPacoteVisualizar(int grupoPacoteId, int pacoteId)
        {
            throw new NotImplementedException();
        }


        public List<GrupoOperacoesView> GetListByPacotesViewSemFicha(List<int> pacotesId)
        {
            throw new NotImplementedException();
        }
    }
}
