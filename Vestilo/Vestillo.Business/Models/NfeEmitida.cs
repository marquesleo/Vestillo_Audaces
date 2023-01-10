


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("Nfe", "Nfe")]    
    public class NfeEmitida
    {
        [Chave]
        public int Id { get; set; }            
        public string Serie { get; set; }       
        public string Numero { get; set; }
        public DateTime? DataEmissao { get; set; } 
        public int StatusNota { get; set; } 
        public string Xmlassinado { get; set; }
        public string Idnotaassinada { get; set; }
        public string Recibonota { get; set; }
        public string Datarecibo { get; set; }       
        public string Recebidasefaz { get; set; }
        public string Protocolosefaz { get; set; }
        public string LogDeEnvio { get; set; }
        public int? IdPedido { get; set; }
        public int? idDocEntrada { get; set; }
        public int? idNfe { get; set; }
        public int? idNfce { get; set; }
        public int Denegada { get; set; }
        public int EmpresaTrocada { get; set; }
        public string NomeEmpresaTrocada { get; set; }

    }
}