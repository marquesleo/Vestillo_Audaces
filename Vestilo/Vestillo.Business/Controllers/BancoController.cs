using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Controllers
{
    public class BancoController : GenericController<Banco, BancoRepository>
    {

        public IEnumerable<Banco> GetPorNumBanco(string numBanco)
        {
            using (BancoRepository repository = new BancoRepository())
            {
                return repository.GetPorNumBanco(numBanco);
            }
        }

        public IEnumerable<Banco> GetPorDescricao(string desc)
        {
            using (BancoRepository repository = new BancoRepository())
            {
                return repository.GetPorDescricao(desc);
            }
        }

        public IEnumerable<Banco> GetByIdList(int id)
        {
            using (BancoRepository repository = new BancoRepository())
            {
                return repository.GetByIdList(id);
            }
        }

        public IEnumerable<Banco> GetAllAtivos()
        {
            using (BancoRepository repository = new BancoRepository())
            {
                return repository.GetAllAtivos();
            }
        }

        public IEnumerable<Banco> GetAllParaBoleto()
        {

            using (BancoRepository repository = new BancoRepository())
            {
                return repository.GetAllParaBoleto();
            }
           
        }

        public Banco GetPadraoVenda()
        {

            using (BancoRepository repository = new BancoRepository())
            {
                return repository.GetPadraoVenda();
            }

        }
    }
}
