
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("contaspagarfaccao")]
    public class ContasPagarFaccaoView 
    {
        public int Id { get; set; }
        public int IdContasPagar { get; set; }
        public int NumLinha { get; set; }
        public int IdAtividade { get; set; }
        public string DescAtividade { get; set; }
        public string RefAtividade { get; set; }
        public int Quantidade { get; set; }
        public decimal Preco { get; set; }
        public decimal Total { get; set; }
        public DateTime DataAtividade { get; set; }
    }
}
