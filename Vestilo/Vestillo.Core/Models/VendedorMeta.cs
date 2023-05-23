using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Core.Models
{
    [Tabela("MetaVendedores")]
    public class VendedorMeta : IModel
    {
        [Chave]
        public int Id { get; set; }
        public int VendedorId { get; set; }
        public Meses Mes { get; set; }
        public decimal Meta { get; set; }
        public decimal MetaCliente { get; set; }
    }
}
