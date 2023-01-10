using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class NfceItensController : GenericController<NfceItens, NfceItensRepository>
    {
        public IEnumerable<NfceItens> GetListByNfce(int IdNfce)
        {
            using (NfceItensRepository repository = new NfceItensRepository())
            {
                return repository.GetListByNfce(IdNfce);
            }
        }

        public IEnumerable<NfceItensView> GetListViewItensNfce(int IdNfce, bool emissao = false)
        {
            using (NfceItensRepository repository = new NfceItensRepository())
            {
                return repository.GetListViewItensNfce(IdNfce, emissao);
            }
        }

        public IEnumerable<NfceItensView> GetListViewItensNfceAgrupado(int IdNfce, bool emissao = false)
        {
            using (NfceItensRepository repository = new NfceItensRepository())
            {
                return repository.GetListViewItensNfceAgrupado(IdNfce, emissao);
            }
        }
        
    }
}
