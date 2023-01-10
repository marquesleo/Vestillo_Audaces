using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class FechamentoDoDiaView
    {  
        public int IdNota { get; set; }
        public int Tipo { get; set; }
        public int TP { get; set; }
        public string Referencia { get; set; }
        public string DescricaoTipo { get; set; }     
        public string RefColaborador { get; set; }
        public string NomeColaborador { get; set; }
        public string refvendedor { get; set; }
        public string NomeVendedor { get; set; }
        public string reftransportadora { get; set; }
        public string nometransportadora { get; set; }
        public int Serie { get; set; }
        public string Numero { get; set; }
        public DateTime? Inclusao { get; set; }
        public DateTime? Emissao { get; set; }
        public DateTime? DataSaida { get; set; }
        public DateTime? DataPrograma { get; set; }
        public DateTime? Horaemissao { get; set; }        
        public int StatusNota { get; set; }        
        public string Recebidasefaz { get; set; }
        public string DescRecebidasefaz { get; set; }
        public decimal total { get; set; }        
        public decimal ValorDinheiro { get; set; }
        public decimal ValorCartaoCredito { get; set; }
        public decimal ValorCartaoDebito { get; set; }
        public decimal ValorCheque { get; set; }
        public decimal ValorNcc { get; set; }
        public decimal ValorTroco { get; set; }
        public decimal Desconto { get; set; }  

    }
}

