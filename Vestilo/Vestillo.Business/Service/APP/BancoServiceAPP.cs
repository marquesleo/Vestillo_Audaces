using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using Vestillo.Business.Service.Interface;

namespace Vestillo.Business.Service.APP
{
    public class BancoServiceAPP: GenericServiceAPP<Banco, BancoRepository, BancoController>, IBancoService
    {
        public BancoServiceAPP()
            : base(new BancoController())
        {
        }
        
        public IEnumerable<Banco> GetPorNumBanco(string numBanco)
        {
            BancoController controller = new BancoController();
            return controller.GetPorNumBanco(numBanco);
        }

        public IEnumerable<Banco> GetPorDescricao(string desc)
        {
            BancoController controller = new BancoController();
            return controller.GetPorDescricao(desc);
        }

        public IEnumerable<Banco> GetByIdList(int id)
        {
            BancoController controller = new BancoController();
            return controller.GetByIdList(id);
        }

        public IEnumerable<Banco> GetAllAtivos()
        {
            BancoController controller = new BancoController();
            return controller.GetAllAtivos();
        }

        public IEnumerable<Banco> GetAllParaBoleto()
        {
            BancoController controller = new BancoController();
            return controller.GetAllParaBoleto();

        }

        public Banco GetPadraoVenda()
        {
            BancoController controller = new BancoController();
            return controller.GetPadraoVenda();
        }
    }
}
