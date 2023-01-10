using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class ContadorCodigoController: GenericController<ContadorCodigo, ContadorCodigoRepository>
    {
        public string GetProximo(string nomeContador)
        {
            using (ContadorCodigoRepository repository = new ContadorCodigoRepository())
            {
                bool transacao = false;

                if (repository._cn.Provider.TransactionCount == 0)
                {
                    repository.BeginTransaction();
                    transacao = true;
                }

                var ret = repository.GetProximo(nomeContador);

                if (transacao)
                {
                    repository.CommitTransaction();
                }

                return ret;
            }
        }

        public ContadorCodigo GetByNome(string nome)
        {
            using (ContadorCodigoRepository repository = new ContadorCodigoRepository())
            {
                return repository.GetByNome(nome);
            }
        }
    }
}
