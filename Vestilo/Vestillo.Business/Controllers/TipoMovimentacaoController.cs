using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class TipoMovimentacaoController : GenericController<TipoMovimentacao, TipoMovimentacaoRepository>
    {

        public IEnumerable<TipoMovimentacao> GetListByTipoEId(int tipo, int id, int SomenteAtivo)
        {
            using (TipoMovimentacaoRepository repository = new TipoMovimentacaoRepository())
            {
                return repository.GetListByTipoEId(tipo, id, SomenteAtivo);

            }
        }

        public IEnumerable<TipoMovimentacao> GetPorReferencia(int tipo, string referencia, int SomenteAtivo)
        {
            using (TipoMovimentacaoRepository repository = new TipoMovimentacaoRepository())
            {
                return repository.GetPorReferencia(tipo, referencia,SomenteAtivo);
            }
        }

        public IEnumerable<TipoMovimentacao> GetPorDescricao(int tipo, string desc, int SomenteAtivo)
        {
            using (TipoMovimentacaoRepository repository = new TipoMovimentacaoRepository())
            {
                return repository.GetPorDescricao(tipo, desc, SomenteAtivo);
            }
        }

        public int GetCountUso(int IdTipoMovimentacao)
        {
            using (TipoMovimentacaoRepository repository = new TipoMovimentacaoRepository())
            {
                return repository.GetCountUso(IdTipoMovimentacao);
            }

        }
    }
}
