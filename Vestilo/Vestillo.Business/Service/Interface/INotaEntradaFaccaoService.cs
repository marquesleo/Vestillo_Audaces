
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
    public interface INotaEntradaFaccaoService : IService<NotaEntradaFaccao, NotaEntradaFaccaoRepository, NotaEntradaFaccaoController>
    {
        IEnumerable<NotaEntradaFaccaoView> GetCamposEspecificos(string parametrosDaBusca);
        IEnumerable<NotaEntradaFaccaoView> GetListPorReferencia(string referencia, string parametrosDaBusca);
        IEnumerable<NotaEntradaFaccaoView> GetListPorNumero(string Numero, string parametrosDaBusca);
        IEnumerable<NotaEntradaFaccaoView> GetListPorOrdem(int OrdemId);
    }

}