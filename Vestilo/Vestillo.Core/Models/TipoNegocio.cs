using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Core.Models
{
    public class TipoNegocio : IModel
    {

        public enum enumTipo 
        {
            TipoSaidaSemVenda = 1,
            TipoSaidaComVenda = 2
        }

        [Chave]
        public int Id { get; set; }
        [FiltroEmpresa]
        public int? EmpresaId { get; set; }
        [RegistroUnico]
        public string Nome { get; set; }
        [OrderByColumn()]
        public enumTipo Tipo { get; set; }
    }
}
