
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
    public interface ITipoOperacaoService : IService<TipoOperacao, TipoOperacaoRepository, TipoOperacaoController>
    {
        IEnumerable<TipoOperacao> GetByAtivos(int AtivoInativo);
    }
}



