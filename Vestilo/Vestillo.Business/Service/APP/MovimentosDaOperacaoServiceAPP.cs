using System;
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
    public class MovimentosDaOperacaoServiceAPP : GenericServiceAPP<MovimentosDaOperacao, MovimentosDaOperacaoRepository, MovimentosDaOperacaoController>, IMovimentosDaOperacaoService
    {

        public MovimentosDaOperacaoServiceAPP() : base(new MovimentosDaOperacaoController())
        {
        }

        public IEnumerable<MovimentosDaOperacao> GetByAtivos(int AtivoInativo)
        {
            return controller.GetByAtivos(AtivoInativo);
        }

        public IEnumerable<MovimentosDaOperacao> GetListPorDescricao(string desc)
        {
            MovimentosDaOperacaoController controller = new MovimentosDaOperacaoController();
            return controller.GetListPorDescricao(desc);
        }

        public IEnumerable<MovimentosDaOperacao> GetListPorReferencia(string referencia)
        {
            MovimentosDaOperacaoController controller = new MovimentosDaOperacaoController();
            return controller.GetListPorReferencia(referencia);
        }

        public IEnumerable<MovimentosDaOperacao> GetListById(int id)
        {
            MovimentosDaOperacaoController controller = new MovimentosDaOperacaoController();
            return controller.GetListById(id);
        }
    }
}



