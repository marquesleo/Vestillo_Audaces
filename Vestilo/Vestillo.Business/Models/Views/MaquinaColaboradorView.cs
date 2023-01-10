using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class MaquinaColaboradorView
    {
        public int Id { get; set; }
        public int IdTipoMaquina { get; set; }
        public int IdColaborador { get; set; }
        public string Descricao { get; set; }
        public int Quantidade { get; set; }
        
    }
}
