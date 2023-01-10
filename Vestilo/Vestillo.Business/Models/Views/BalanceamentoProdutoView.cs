using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models.Views
{
    [Tabela("BalanceamentoProduto", "Balanceamento de Produto")]
    public class BalanceamentoProdutoView : BalanceamentoProduto
    {
        [NaoMapeado]
        public string Setor { get; set; }
        [NaoMapeado]
        public string Produto { get; set; }
        [NaoMapeado]
        public decimal? Coeficiente { get; set; }
        [NaoMapeado]
        public decimal? MinProducao
        {
            get
            {
                if (Quantidade != 0)
                    return (Quantidade * Tempo) + TempoPacote;
                else
                    return 0;
            }
        }
        [NaoMapeado]
        public decimal? Operadoras
        {
            get
            {
                if (Coeficiente > 0)
                    return MinProducao / Coeficiente;
                else
                    return null;
            }
        }
    }
}
