using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class CreditosClientesView
    {
        
        public int Id { get; set; }        
        public int IdEmpresa { get; set; }
        public int Status { get; set; }
        public string DescricaoStatus
        {
            get
            {
                switch (this.Status)
                {
                    case (int)enumStatusCreditoCliente.Aberto:
                        return "Aberto";
                    case (int)enumStatusCreditoCliente.Quitado:
                        return "Quitado";                  
                    default:
                        return "";
                }
            }
        }

        public string RefCliente { get; set; }
        public string NomeCliente { get; set; }
        public DateTime dataemissao { get; set; }
        public decimal valor { get; set; }
        public int? idNotaFat { get; set; }
        public int? idnotaconsumidor { get; set; }
        public int idcolaborador { get; set; }
        public string ObsInclusao { get; set; }
        public string ObsQuitacao { get; set; } 
        public bool Ativo { get; set; }
        public string ObsGeral { get; set; }
    }
}
