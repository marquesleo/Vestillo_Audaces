using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Service
{
    public interface IGrupoOperacoesService : IService<GrupoOperacoes, GrupoOperacoesRepository, GrupoOperacoesController>
    {
        List<GrupoOperacoesView> GetListByGrupoPacote(int grupoPacoteId);
        List<GrupoOperacoesView> GetListByPacoteView(string pacoteRef);
        List<GrupoOperacoesView> GetListByPacoteView(string pacoteRef, int setorId);
        List<GrupoOperacoesView> GetListByPacoteView(string pacoteRef, string sequencia);
        GrupoOperacoesView GetListByPacoteESequneciaView(string pacoteRef, string sequencia);
        List<GrupoOperacoesView> GetListByPacotesView(List<int> pacotesId);
        List<GrupoOperacoesView> GetListByPacotesViewSemFicha(List<int> pacotesId);
        List<GrupoOperacoesView> GetListByGrupoPacoteVisualizar(int grupoPacoteId, int pacoteId);
    }
}
