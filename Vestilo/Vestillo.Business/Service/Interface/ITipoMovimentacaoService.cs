using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Controllers;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service
{
    public interface ITipoMovimentacaoService : IService<TipoMovimentacao, TipoMovimentacaoRepository, TipoMovimentacaoController>
    {
        IEnumerable<TipoMovimentacao> GetListByTipoEId(int tipo, int id, int SomenteAtivo);
        IEnumerable<TipoMovimentacao> GetPorDescricao(int tipo, String desc, int SomenteAtivo);
        IEnumerable<TipoMovimentacao> GetPorReferencia(int tipo, String referencia, int SomenteAtivo);
        int GetCountUso(int IdTipoMovimentacao);

    }
}
