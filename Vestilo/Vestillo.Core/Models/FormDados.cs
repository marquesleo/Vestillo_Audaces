using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Vestillo.Core.Models
{
    [Tabela("FormDados")]
    public class FormDados : IModel
    {
        [Chave]
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Form { get; set; }
        [FiltroEmpresa]
        public int EmpresaId { get; set; }
        public string DadosTela { get; set; }
    }
}
