
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;

namespace Vestillo.Business.Service.APP
{
    public class CaixasServiceAPP : GenericServiceAPP<Caixas, CaixasRepository, CaixasController>, ICaixasService
    {

        public CaixasServiceAPP()  : base(new CaixasController())
        {
        }

        public IEnumerable<Caixas> GetByAtivos(int AtivoInativo)
        {
            return controller.GetByAtivos(AtivoInativo);
        }

        public IEnumerable<Caixas> GetListPorDescricao(string desc)
        {
            CaixasController controller = new CaixasController();
            return controller.GetListPorDescricao(desc);
        }

        public IEnumerable<Caixas> GetListPorReferencia(string Abreviatura)
        {
            CaixasController controller = new CaixasController();
            return controller.GetListPorReferencia(Abreviatura);
        }

        public IEnumerable<Caixas> GetListById(int id)
        {
            CaixasController controller = new CaixasController();
            return controller.GetListById(id);
        }

        public IEnumerable<Caixas> GetAllTrataHoras()
        {
            CaixasController controller = new CaixasController();
            return controller.GetAllTrataHoras();
        }


        public Caixas GetByIdTrataHoras(int id)
        {
            CaixasController controller = new CaixasController();
            return controller.GetByIdTrataHoras(id);
        }
    }
}



