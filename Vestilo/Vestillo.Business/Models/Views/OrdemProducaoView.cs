using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo;

namespace Vestillo.Business.Models.Views
{
    public class OrdemProducaoView : OrdemProducao
    {
        [Vestillo.NaoMapeado]
        public string DescricaoAlmoxarifado { get; set; }

        [Vestillo.NaoMapeado]
        public decimal TotalItens { get; set; }

        [Vestillo.NaoMapeado]
        public decimal quantidadeCuringa { get; set; }

        [Vestillo.NaoMapeado]
        public string colaborador { get; set; }

        [Vestillo.NaoMapeado]
        public decimal Aberto { get; set; }
        [Vestillo.NaoMapeado]
        public decimal Producao { get; set; }
        [Vestillo.NaoMapeado]
        public decimal Concluido { get; set; }

        [Vestillo.NaoMapeado]
        public decimal QtdProduzida { get; set; }

        [Vestillo.NaoMapeado]
        public string DescricaoStatus
        {
            get
            {
                switch (this.Status)
                {
                    case (int)enumStatusOrdemProducao.Aberto:
                        return "Aberto";
                    case (int)enumStatusOrdemProducao.Apenas_Liberado:
                        return "Apenas Liberada";
                    case (int)enumStatusOrdemProducao.Atendido:
                        return "Atendido";
                    case (int)enumStatusOrdemProducao.Atendido_Parcial:
                        return "Parcialmente Atendida";
                    case (int)enumStatusOrdemProducao.Em_Corte:
                        return "Liberada em Corte";
                    case (int)enumStatusOrdemProducao.Em_producao:
                        return "Em Produção";
                    case (int)enumStatusOrdemProducao.Finalizado:
                        return "Finalizado";
                    case (int)enumStatusOrdemProducao.Novo:
                        return "Novo";
                    case (int)enumStatusOrdemProducao.Producao_Parcial:
                        return "Parcialmente em Produção";
                    case (int)enumStatusOrdemProducao.Liberado_Parcial:
                        return "Parcialmente Liberada";
                    case (int)enumStatusOrdemProducao.Enviado_Corte:
                        return "Em Corte";
                    default:
                        return "";
                }
            }
        }
    }
}
