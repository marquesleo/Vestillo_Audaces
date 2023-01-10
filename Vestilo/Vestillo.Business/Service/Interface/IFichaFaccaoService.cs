using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Controllers;
using Vestillo.Business.Repositories;
using Vestillo.Business.Models.Views;

namespace Vestillo.Business.Service
{
    public interface IFichaFaccaoService : IService<FichaFaccao, FichaFaccaoRepository, FichaFaccaoController>
    {
        IEnumerable<FichaFaccao> VerificaFaccao(List<int> idPacote, List<int> idProduto, List<int> idFaccao);
        IEnumerable<FichaFaccao> GetByIdFicha(int idFicha);
        IEnumerable<FichaFaccao> GetByIdFaccao(int idFaccao);
        IEnumerable<FichaFaccaoView> GetByIdFichaView(int idFicha);
    }
}