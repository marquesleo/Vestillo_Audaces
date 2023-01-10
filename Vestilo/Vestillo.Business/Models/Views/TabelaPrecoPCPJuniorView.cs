using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("TabelaPrecoPCPJunior", "Tabela de Preco")]
    public class TabelaPrecoPCPJuniorView : TabelaPrecoPCPJunior 
    {
        [NaoMapeado]
        public string Referencia { get; set; }
        [NaoMapeado]
        public string Descricao { get; set; }
        [NaoMapeado]
        public string Grupo { get; set; }
        [NaoMapeado]
        public decimal Custo { get; set; }

        [NaoMapeado]
        public decimal Sugerido
        {
            get
            {
                return Custo / (1 - ((Lucro + Simples + Comissao) / 100));
            }
        }

        [NaoMapeado]
        public decimal SimplesCalc
        {
            get
            {
                return Sugerido * Simples/100;
            }
        }

        [NaoMapeado]
        public decimal LucroCalc
        {
            get
            {
                return Sugerido * Lucro / 100;
            }
        }

        [NaoMapeado]
        public decimal ComissaoCalc
        {
            get
            {
                return Sugerido * Comissao / 100;
            }
        }

        [NaoMapeado]
        public decimal SimplesTabela
        {
            get
            {
                return Tabela * Simples / 100;
            }
        }

        [NaoMapeado]
        public decimal ComissaoPercentual
        {
            get
            {
                return Comissao;
            }
        }

        [NaoMapeado]
        public decimal ComissaoTabela
        {
            get
            {
                return Tabela * ComissaoPercentual / 100;
            }
        }                

        [NaoMapeado]
        public decimal LucroTabela
        {
            get
            {
                return Tabela - Custo - ComissaoTabela - SimplesTabela;
            }
        }

        [NaoMapeado]
        public decimal LucroPercentualTabela
        {
            get
            {
                if (Tabela > 0)
                {
                    return (LucroTabela / Tabela) * 100;
                }
                else
                {
                    return 0;
                }
            }
        }

    }
}
