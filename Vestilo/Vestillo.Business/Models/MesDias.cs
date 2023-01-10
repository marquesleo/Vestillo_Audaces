using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("mesdias", "mês dias")]  
    public class MesDias
    {
        [Chave]
        public int Id { get; set; }
        public int PremioId { get; set; }
        public int Mes { get; set; }
        public int Dias { get; set; }
        [NaoMapeado]
        public string MesDesc { get 
            {
                return CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(Mes);
            } 
        }
    }
}
