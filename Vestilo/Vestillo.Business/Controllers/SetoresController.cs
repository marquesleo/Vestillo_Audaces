
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class SetoresController : GenericController<Setores, SetoresRepository>
    {
        public IEnumerable<Setores> GetByAtivos(int AtivoInativo)
        {
            using (SetoresRepository repository = new SetoresRepository())
            {
                return repository.GetByAtivos(AtivoInativo);
            }
        }


        public IEnumerable<Setores> GetByBalanceamentos()
        {
            using (SetoresRepository repository = new SetoresRepository())
            {
                return repository.GetByBalanceamentos();
            }
        }

        public IEnumerable<Setores> GetListPorReferencia(string Abreviatura)
        {
            using (SetoresRepository repository = new SetoresRepository())
            {
                return repository.GetListPorReferencia(Abreviatura);
            }
        }

        public IEnumerable<Setores> GetListPorDescricao(string desc)
        {
            using (SetoresRepository repository = new SetoresRepository())
            {
                return repository.GetListPorDescricao(desc);
            }
        }

        public IEnumerable<Setores> GetListById(int id)
        {
            using (SetoresRepository repository = new SetoresRepository())
            {
                return repository.GetListById(id);
            }
        }



        //balanceamento

        public IEnumerable<Setores> GetByAtivosBalanceamento(int AtivoInativo)
        {
            using (SetoresRepository repository = new SetoresRepository())
            {
                return repository.GetByAtivosBalanceamento(AtivoInativo);
            }
        }


        public IEnumerable<Setores> GetListPorReferenciaBalanceamento(string Abreviatura)
        {
            using (SetoresRepository repository = new SetoresRepository())
            {
                return repository.GetListPorReferenciaBalanceamento(Abreviatura);
            }
        }

        public IEnumerable<Setores> GetListPorDescricaoBalanceamento(string desc)
        {
            using (SetoresRepository repository = new SetoresRepository())
            {
                return repository.GetListPorDescricaoBalanceamento(desc);
            }
          
        }

        public IEnumerable<Setores> GetListByIdBalanceamento(int id)
        {
            using (SetoresRepository repository = new SetoresRepository())
            {
                return repository.GetListByIdBalanceamento(id);
            }
        }



    }
}

