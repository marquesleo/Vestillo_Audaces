
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
    public class SetoresServiceAPP : GenericServiceAPP<Setores, SetoresRepository, SetoresController>, ISetoresService
    {

        public SetoresServiceAPP()
            : base(new SetoresController())
        {
        }

        public IEnumerable<Setores> GetByAtivos(int AtivoInativo)
        {
            return controller.GetByAtivos(AtivoInativo);
        }

        public IEnumerable<Setores> GetListPorDescricao(string desc)
        {
            SetoresController controller = new SetoresController();
            return controller.GetListPorDescricao(desc);
        }

        public IEnumerable<Setores> GetListPorReferencia(string Abreviatura)
        {
            SetoresController controller = new SetoresController();
            return controller.GetListPorReferencia(Abreviatura);
        }

        public IEnumerable<Setores> GetListById(int id)
        {
            SetoresController controller = new SetoresController();
            return controller.GetListById(id);
        }


        public IEnumerable<Setores> GetBalanceamentos()
        {
            return controller.GetByBalanceamentos();
        }
        

        public IEnumerable<Setores> GetByAtivosBalanceamento(int AtivoInativo)
        {
            return controller.GetByAtivosBalanceamento(AtivoInativo);
        }


        public IEnumerable<Setores> GetListPorReferenciaBalanceamento(string Abreviatura)
        {
            SetoresController controller = new SetoresController();
            return controller.GetListPorReferenciaBalanceamento(Abreviatura);
        }

        public IEnumerable<Setores> GetListPorDescricaoBalanceamento(string desc)
        {
            SetoresController controller = new SetoresController();
            return controller.GetListPorDescricaoBalanceamento(desc);
        }

        public IEnumerable<Setores> GetListByIdBalanceamento(int id)
        {
            SetoresController controller = new SetoresController();
            return controller.GetListByIdBalanceamento(id);
        }

    }
}



