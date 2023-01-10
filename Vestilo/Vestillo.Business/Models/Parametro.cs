using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("Parametros", "Parâmetros")]
    public class Parametro
    {
        [Chave]
        public int Id { get; set; }
        [OrderByColumn]
        public string Chave { get; set; }
        public string Valor { get; set; }
        [FiltroEmpresa]
        public int EmpresaId { get; set; }
        public int VisaoCliente { get; set; }
    }
}
