
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class MovimentacaoBancoView
    {
        public string DescricaoTipo
        {
            get
            {
                switch (this.Tipo)
                {
                    case (int)enumTipoMovBanco.Credito:
                        return "Crédito";
                    case (int)enumTipoMovBanco.Debito:
                        return "Débito";
                    case (int)enumTipoMovBanco.Transferencia:
                        return "Transferência";
                    default:
                        return "";
                }
            }
        }

        public int Id { get; set; }        
        public int IdEmpresa { get; set; }
        public int IdBanco { get; set; }
        public int IdBancoDestino { get; set; }
        public DateTime DataMovimento { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
        public string NumBanco { get; set; }
        public string DescricaoBanco { get; set;}
        public string natureza { get; set; }       

        public int? IdContasReceber { get; set; }
        public int? IdContasPagar { get; set; }
        public int? IdCheque { get; set; }
        public int IdUsuario { get; set; }
       
        public int Tipo { get; set; }
        public decimal Valor { get; set; }
        public string Observacao { get; set; }
        
       
    }
}
