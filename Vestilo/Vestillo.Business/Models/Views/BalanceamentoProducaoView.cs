using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models.Views
{
    [Tabela("BalanceamentoProducao", "Balanceamento de Produção")]
    public class BalanceamentoProducaoView : BalanceamentoProducao
    {
        [NaoMapeado]
        public string Setor { get; set; }
        [NaoMapeado]
        public string StatusDesc
        {
            get
            {
                switch (Status)
                {
                    case 0:
                        return "Lib";
                    case 1:
                        return "Prod";
                    case 2:
                        return "Geral";
                    default:
                        return "Lib";
                }
            }
        }

        [NaoMapeado]
        public decimal? CapacidadeGeral { get; set; }

        [NaoMapeado]
        public decimal? Capacidade
        {
            get
            {
                if (Status != 2)
                    return Operadoras * MinutoProducao * Eficiencia / 100;
                else
                    return CapacidadeGeral;
            }
        }

        [NaoMapeado]
        public decimal? Disponivel
        {
            get
            {
                if (Capacidade != null)
                    return Capacidade - Total;
                else
                    return -Total;
            }
        }
        [NaoMapeado]
         public decimal? Coeficiente
        {
            get
            {
                if (Status != 2)
                    return MinutoProducao * Eficiencia / 100;
                else if (Operadoras != 0)
                    return CapacidadeGeral / Operadoras;
                else
                    return 0;
            }
        }
        [NaoMapeado]
        public decimal? TotalNovos { get; set; }
        [NaoMapeado]
        public decimal? OperadorasNecessarias
        {
            get
            {
                if (Status == 2)
                    if (Coeficiente > 0)
                        return (Total + TotalNovos) / Coeficiente;
                    else
                        return 0;
                else
                    return 0;
            }
        }
        [NaoMapeado]
        public decimal? CapacidadeRestante
        {
            get
            {
                if (Status == 2)
                    return Disponivel - TotalNovos;
                else
                    return 0;
            }
        }
        [NaoMapeado]
        public decimal? Sobra
        {
            get
            {
                if (Status == 2)
                    if (Coeficiente > 0)
                        return CapacidadeRestante / Coeficiente;
                    else
                        return 0;
                else
                    return 0;
            }
        }
    }
}
