using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("TransferenciaItens", "TransferenciaItens")]
    public class TransferenciaItens
    {
        [Chave]
        public int Id { get; set; }
        public int Idtransferencia { get; set; }
        public int iditem { get; set; }
        public int? idcor { get; set; }
        public int? idtamanho { get; set; }
        public decimal Quantidade { get; set; }      

    }


    public class TransferenciaItensView
    {
        public int Id { get; set; }
        public int IdIdtransferencia { get; set; }
        public int iditem { get; set; }
        public int? idcor { get; set; }
        public int? idtamanho { get; set; }
        public decimal Quantidade { get; set; }
        public string Referencia { get; set; }
        public string Descricao { get; set; }       
        public string Tamanho { get; set; }       
        public string Cor { get; set; }          

    }
}