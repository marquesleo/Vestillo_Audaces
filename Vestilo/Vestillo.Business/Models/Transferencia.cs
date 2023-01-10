
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("Transferencia", "Transferência")]
    public class Transferencia
    {
        [Chave]
        public int Id { get; set; }
        [FiltroEmpresa]
        public int IdEmpresa { get; set; }                
        public int? idAlmoxarifadoOrigem { get; set; }
        public int idAlmoxarifadoDestino { get; set; }
        public int? IdCliente { get; set; }
        public int TipoTransferencia { get; set; }
        public String NotasTransferidas { get; set; }
        public String IdsDasNotas { get; set; }            
        [Contador("Transferencia")]
        [RegistroUnico]
        public String Referencia { get; set; }
        public DateTime? DataInclusao { get; set; }
        public int totalitens { get; set; }
        public String Usuario { get; set; }
        public String Obs { get; set; }


        [NaoMapeado]
        public IEnumerable<TransferenciaItens> ItensTransferencia { get; set; }

        [NaoMapeado]
        public IEnumerable<MovimentacaoEstoque> ItensMovimentacaoEstoque { get; set; }        

    }


    public class TransferenciaView
    {


        public int Id { get; set; }

        public int IdEmpresa { get; set; }       
        public int idAlmoxarifadoOrigem { get; set; }
        public int idAlmoxarifadoDestino { get; set; }
        public int? IdCliente { get; set; }
        public String Referencia { get; set; }
        public DateTime? DataInclusao { get; set; }
        public String NomeCliente { get; set; }       
        public int totalitens { get; set; }
        public String Usuario { get; set; }
        public String Obs { get; set; }
        public int TipoTransferencia { get; set; }
        public String NotasTransferidas { get; set; }
        public String IdsDasNotas { get; set; }

        public String AlmoxarifadoOrigem { get; set; }
        public String AlmoxarifadoDestino { get; set; }
        public String DescricaoTipo { get; set; }
    }
}