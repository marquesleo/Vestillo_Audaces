
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class ContadorNossoNumeroController : GenericController<ContadorNossoNumero, ContadorNossoNumeroRepository>
    {
        public string GetProximo(int IdBanco)
        {
            using (ContadorNossoNumeroRepository repository = new ContadorNossoNumeroRepository())
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

        public ContadorNossoNumero GetByBanco(int IdBanco)
        {
            using (ContadorNossoNumeroRepository repository = new ContadorNossoNumeroRepository())
            {
                return repository.GetByBanco(IdBanco);
            }
        }
    }
}
