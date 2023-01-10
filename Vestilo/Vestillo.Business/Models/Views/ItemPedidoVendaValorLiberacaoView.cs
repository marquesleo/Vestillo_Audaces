using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class ItemPedidoVendaValorLiberacaoView
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public decimal Total { get; set; }
        public decimal QtdEmpenhada { get; set; }
    }
}