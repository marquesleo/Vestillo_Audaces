
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("Pendencias", "Pendencias")]
    public class Pendencias
    {
        [Chave]
        public int Id { get; set; }      
        public string Evento { get; set;}
        public string Tabela { get; set;}
        public int idItem { get; set; }   
        public int Status { get; set; }
        public int IdEmpresa { get; set; }
    }
}
