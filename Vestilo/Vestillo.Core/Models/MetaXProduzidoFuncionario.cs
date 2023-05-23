using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Core.Models
{ 
    public class MetaXProduzidoFuncionario
    {
        public decimal Meta { get; set; }
        public decimal MinProduzidos { get; set; }
        public decimal Realizado
        {
            get
            {
                if (Meta > 0)
                    return (MinProduzidos / Meta) * 100;
                return 0;
            }
        }
        public decimal Inativo
        {
            get
            {
                if (Meta > MinProduzidos)
                    return (Meta - MinProduzidos);
                return 0;
            }
        }
        
    }
}
