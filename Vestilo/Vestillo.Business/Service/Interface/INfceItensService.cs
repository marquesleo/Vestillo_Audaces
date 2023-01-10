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
    public interface INfceItensService : IService<NfceItens, NfceItensRepository, NfceItensController>
    {
        IEnumerable<NfceItens> GetListByNfce(int IdNfce);
        IEnumerable<NfceItensView> GetListViewItensNfce(int IdNfce, bool emissao = false);
        IEnumerable<NfceItensView> GetListViewItensNfceAgrupado(int IdNfce, bool emissao = false);
        
    }
}
