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
    public class ServicoServiceAPP: GenericServiceAPP<Servico, ServicoRepository, ServicoController>, IServicoService
    {
        public ServicoServiceAPP()
            : base(new ServicoController())
        {
        }

        public IEnumerable<Servico> GetPorReferencia(string referencia)
        {
            ServicoController controller = new ServicoController();
            return controller.GetPorReferencia(referencia);
        }

        public IEnumerable<Servico> GetPorDescricao(string desc)
        {
            ServicoController controller = new ServicoController();
            return controller.GetPorDescricao(desc);
        }

        public IEnumerable<Servico> GetByIdList(int id)
        {
            ServicoController controller = new ServicoController();
            return controller.GetByIdList(id);
        }
    }
}
