using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("FichaFaccao", "Ficha na Facção")]
    public class FichaFaccao
    {
        [Chave]
        public int Id { get; set; }
        public int idFicha { get; set; }
        public int idFaccao { get; set; }
        public decimal valorPeca { get; set; }
        public decimal precoCompra { get; set; }
    }
}