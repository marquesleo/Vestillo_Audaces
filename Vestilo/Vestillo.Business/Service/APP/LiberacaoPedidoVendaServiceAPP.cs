using Vestillo.Business.Controllers;
using Vestillo.Business.Models;
using Vestillo.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Service.APP
{
    public class LiberacaoPedidoVendaServiceAPP: GenericServiceAPP<LiberacaoPedidoVenda, LiberacaoPedidoVendaRepository, LiberacaoPedidoVendaController>, ILiberacaoPedidoVendaService
    {
        public LiberacaoPedidoVendaServiceAPP()
            : base(new LiberacaoPedidoVendaController())
        {

        }

        public List<LiberacaoPedidoVendaView> GetByPedidoIdView(int pedidoId)
        {
            LiberacaoPedidoVendaController controller = new LiberacaoPedidoVendaController();
            return controller.GetByPedidoIdView(pedidoId);
        }


        public void Save(ref LiberacaoPedidoVenda entity)
        {
            throw new NotImplementedException();
        }

        public new LiberacaoPedidoVenda GetById(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<LiberacaoPedidoVenda> IService<LiberacaoPedidoVenda, LiberacaoPedidoVendaRepository, LiberacaoPedidoVendaController>.GetById(int[] ids)
        {
            throw new NotImplementedException();
        }

        public new IEnumerable<LiberacaoPedidoVenda> GetAll()
        {
            throw new NotImplementedException();
        }

        public int GetIdPropertyModel(LiberacaoPedidoVenda entity)
        {
            throw new NotImplementedException();
        }
    }
}
