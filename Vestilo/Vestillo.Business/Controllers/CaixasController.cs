using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class CaixasController : GenericController<Caixas, CaixasRepository>
    {
        public IEnumerable<Caixas> GetByAtivos(int AtivoInativo)
        {
            using (CaixasRepository repository = new CaixasRepository())
            {
                return repository.GetByAtivos(AtivoInativo);
            }
        }

        public IEnumerable<Caixas> GetListPorReferencia(string Abreviatura)
        {
            using (CaixasRepository repository = new CaixasRepository())
            {
                return repository.GetListPorReferencia(Abreviatura);
            }
        }

        public IEnumerable<Caixas> GetListPorDescricao(string desc)
        {
            using (CaixasRepository repository = new CaixasRepository())
            {
                return repository.GetListPorDescricao(desc);
            }
        }

        public IEnumerable<Caixas> GetListById(int id)
        {
            using (CaixasRepository repository = new CaixasRepository())
            {
                return repository.GetListById(id);
            }
        }

        public IEnumerable<Caixas> GetAllTrataHoras()
        {

            using (CaixasRepository repository = new CaixasRepository())
            {
                return repository.GetAllTrataHoras();
            }
        }


        public Caixas GetByIdTrataHoras(int id)
        {
            using (CaixasRepository repository = new CaixasRepository())
            {
                return repository.GetByIdTrataHoras(id);
            }
        }

    }
}


