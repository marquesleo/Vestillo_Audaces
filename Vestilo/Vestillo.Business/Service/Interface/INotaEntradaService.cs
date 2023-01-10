
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
    public interface INotaEntradaService : IService<NotaEntrada , NotaEntradaRepository , NotaEntradaController >
    {
        IEnumerable<NotaEntradaView> GetCamposEspecificos(string parametrosDaBusca);
        IEnumerable<NotaEntradaView> GetListPorReferencia(string referencia, string parametrosDaBusca);
        IEnumerable<NotaEntradaView> GetListPorNumero(string Numero, string parametrosDaBusca);
        IEnumerable<NotaEntradaView> GetListPorPedido(int pedidoId);

    }

}