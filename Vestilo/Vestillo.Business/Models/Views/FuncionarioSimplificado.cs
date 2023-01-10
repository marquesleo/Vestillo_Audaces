using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Service;

namespace Vestillo.Business.Models
{
    [Tabela("Funcionarios", "Funcionários")]
    public class FuncionarioSimplificado: Funcionario
    {
        [NaoMapeado]
        public decimal? INSS
        {

            get
            {
                if (SalarioBase == null)
                {
                    return null;
                }
                var gratif = Gratificacao != null ? Gratificacao : 0;
                var empresa = new EmpresaService().GetServiceFactory().GetById(VestilloSession.EmpresaLogada.Id);

                return ((decimal)empresa.inss * ((decimal)SalarioBase + gratif)/100);
            }
        }
        [NaoMapeado]
        public decimal? FGTS
        {
            get
            {
                if (SalarioBase == null)
                {
                    return null;
                }
                var gratif = Gratificacao != null ? Gratificacao : 0;
                var empresa = new EmpresaService().GetServiceFactory().GetById(VestilloSession.EmpresaLogada.Id);
                return ((decimal)empresa.fgts * ((decimal)SalarioBase + gratif)/100);
            }
        }
        [NaoMapeado]
        public decimal? DecimoTerceiro
        {
            get
            {
                if (SalarioBase == null)
                {
                    return null;
                }
                var gratif = Gratificacao != null ? Gratificacao : 0;
                return (decimal)1 / 12 * ((decimal)SalarioBase + gratif);
            }
        }

        [NaoMapeado]
        public decimal? Ferias
        {
            get
            {
                if (DecimoTerceiro == null)
                {
                    return null;
                }
                return (decimal)1 / 3 * DecimoTerceiro;
            }
        }

        [NaoMapeado]
        public string CargoDescricao { get; set; }
    }
}
