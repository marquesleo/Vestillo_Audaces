
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Models.Views;

namespace Vestillo.Business.Models
{
    public class ContasPagarRelNotinhaFaccao
    {
        public int NotinhaId { get; set; }
        public int RotaId { get; set; }
        public int FaccaoId { get; set; }
        public string DescFacao { get; set; }
        public string DescRota { get; set; }
        public string RefTitulo { get; set; }

        public DateTime DataAtividade { get; set; }
        public string DescAtividade { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
        public decimal Total { get; set; }
        public decimal Vale { get; set; }       
        public decimal TotalFator { get; set; }
        public decimal Saldo { get; set; }
    }
}

