using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("TabelasPreco", "Tabela de Preços")]
    public class TabelaPrecoView : TabelaPreco
    {

        [NaoMapeado]
        public string TabelaPrecoBaseDescricao { get; set; }

        [NaoMapeado]
        public string FatorDescricao
        {
            get
            {
                return this.Fator.ToString();
            }
        }

        [NaoMapeado]
        public string DescricaoMetodoArredondamento
        {
            get
            {
                switch (this.MetodoArredondamento)
                {
                    case 0:
                        return "Sem arredondamento";
                    case 1:
                        return "Arredondar para nº inteiro";
                    case 2:
                        return "Arredondar para dezenas inteiras";
                    default:
                        return "Sem arredondamento";
                }
            }
        }
    }
}
