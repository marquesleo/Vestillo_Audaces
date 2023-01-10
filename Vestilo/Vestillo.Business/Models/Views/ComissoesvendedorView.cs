using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class ComissoesvendedorView
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
                    case (int)enumStatusComissao.Aberto:
                        return "Aberto";
                    case (int)enumStatusComissao.Liberado:
                        return "Pagamento Liberado";                  
                    default:
                        return "";
                }
            }
        }
        public string Referencia { get; set; }
        public string RefComissao { get; set; }
        public string parcela { get; set; } 
        public string RefVendedor { get; set; }
        public string NomeVendedor { get; set; }
        public string RefGuia { get; set; }
        public string NomeGuia { get; set; }
        public DateTime dataemissao { get; set; }
        public DateTime? dataliberacao { get; set; }
        public decimal basecalculo { get; set; }
        public decimal percentual { get; set; }
        public string RefCliente { get; set; }
        public string NomeCliente { get; set; }
        public decimal valor { get; set; }
        public int? idNotaFat { get; set; }
        public int? idnotaconsumidor { get; set; }       
        public int idcontasreceber { get; set; }
        public int idvendedor { get; set; }
        public int idGuia { get; set; }
        public int? idcontaspagar { get; set; }
        public bool Ativo { get; set; }
        public string Obs { get; set; }
        public int ExibirTitulo { get; set; }
        public string ObsNFCe { get; set; }
      
    }
}
