
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class ContadorRemessaController : GenericController<ContadorRemessa, ContadorRemessaRepository>
    {
        public int GetProximo(int IdBanco)
        {
            using (ContadorRemessaRepository repository = new ContadorRemessaRepository())
            {
                bool transacao = false;

                if (repository._cn.Provider.TransactionCount == 0)
                {
                    repository.BeginTransaction();
                    transacao = true;
                }

                var ret = repository.GetProximo(IdBanco);

                if (transacao)
                {
                    repository.CommitTransaction();
                }

                return ret;
            }
        }

        public ContadorRemessa GetByBanco(int IdBanco)
        {
            using (ContadorRemessaRepository repository = new ContadorRemessaRepository())
            {
                return repository.GetByBanco(IdBanco);
            }
        }
    }
}
