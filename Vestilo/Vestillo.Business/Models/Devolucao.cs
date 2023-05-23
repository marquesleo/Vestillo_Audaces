
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("Devolucao", "Devolucao")]    
    public class Devolucao
    {
        [Chave]
        public int Id { get; set; }
        [FiltroEmpresa]
        public int IdEmpresa { get; set; }        
        public int? idTabela { get; set; }
        public int? idVendedor { get; set; } 
        public int idAlmoxarifado { get; set; }
        public int IdCliente { get; set; }
        public int IdMotivo { get; set; }
        [Contador("Devolucao")]
        [RegistroUnico]
        public String Referencia { get; set; }        
        public DateTime? DataInclusao { get; set; }
        public decimal totaldevolucao { get; set; }
        public int totalitens { get; set; }
        public String Usuario { get; set; }
        public String Obs { get; set; }          

        [NaoMapeado]
        public IEnumerable<DevolucaoItens> ItensDevolucao { get; set; }

        [NaoMapeado]
        public IEnumerable<MovimentacaoEstoque> ItensMovimentacaoEstoque { get; set; }

        [NaoMapeado]
        public IEnumerable<CreditosClientes> CreditoCliente { get; set; }
        
    }


    public class DevolucaoView
    {

       
        public int Id { get; set; }
       
        public int IdEmpresa { get; set; }
        public int? idTabela { get; set; }
        public int? idVendedor { get; set; }
        public int idAlmoxarifado { get; set; }
        public int IdCliente { get; set; }
        public String Referencia { get; set; }
        public DateTime? DataInclusao { get; set; }
        public String NomeCliente { get; set; }
        public String NomeVendedor { get; set; }      
        public String DescTabela { get; set; }        
        public decimal totaldevolucao { get; set; }
        public int totalitens { get; set; }
        public String Usuario { get; set; }
        public String Obs { get; set; }          

    }
}