using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.Interface;

namespace Vestillo.Business.Service.APP
{
    public class TipoMovimentacaoServiceAPP: GenericServiceAPP<TipoMovimentacao, TipoMovimentacaoRepository, TipoMovimentacaoController>, ITipoMovimentacaoService
    {

        public TipoMovimentacaoServiceAPP()
            : base(new TipoMovimentacaoController())
        {
        }

        public IEnumerable<TipoMovimentacao> GetListByTipoEId(int tipo, int id, int SomenteAtivo)
        {
            TipoMovimentacaoController controller = new TipoMovimentacaoController();
            return controller.GetListByTipoEId(tipo, id,  SomenteAtivo);
        }

        public IEnumerable<TipoMovimentacao> GetPorDescricao(int tipo, string desc, int SomenteAtivo)
        {
            TipoMovimentacaoController controller = new TipoMovimentacaoController();
            return controller.GetPorDescricao(tipo,desc,  SomenteAtivo);
        }

        public IEnumerable<TipoMovimentacao> GetPorReferencia(int tipo, string referencia, int SomenteAtivo)
        {
            TipoMovimentacaoController controller = new TipoMovimentacaoController();
            return controller.GetPorReferencia(tipo,referencia,  SomenteAtivo);
        }

        public int GetCountUso(int IdTipoMovimentacao)
        {
            TipoMovimentacaoController controller = new TipoMovimentacaoController();
            return controller.GetCountUso(IdTipoMovimentacao);

        }
    }
}
