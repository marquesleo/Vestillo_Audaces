using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("Nfce", "Nfce")]
    public class NfceEmitida
    {
        [Chave]
        public int Id { get; set; }
        public string Serie { get; set; }
        public string NumeroNfce { get; set; }
        public int StatusNota { get; set; }
        public string XmlAssinado { get; set; }
        public string NotaAssinadaId { get; set; }
        public string ReciboNota { get; set; }
        public string DataRecibo { get; set; }        
        public int RecebidaSefaz { get; set; }
        public string ProtocoloSefaz { get; set; }
        //public decimal Troco { get; set; }
        //public decimal TotalOriginal { get; set; }
        //public decimal Total { get; set; }
        public string NomeCliente { get; set; }
        public string CpfCnpj { get; set; }
        public int Pessoa { get; set; }
        public int EmitidaContingencia { get; set; }
        public DateTime DataFinalizacao { get; set; }
        public int TipoNfce { get; set; }
       
    }
}
