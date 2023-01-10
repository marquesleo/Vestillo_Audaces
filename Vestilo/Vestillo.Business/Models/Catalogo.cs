
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("Catalogo", "Catalogo")]    
    public class Catalogo
    {
        [Chave]
        public int Id { get; set; }        
        [RegistroUnico]
        public string Abreviatura { get; set; }
        [RegistroUnico]
        [OrderByColumn]
        public string Descricao { get; set; }        
    }
}