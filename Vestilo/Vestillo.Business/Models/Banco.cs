using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("Bancos","Banco")]
    public class Banco
    {
        [Chave]
        public int Id { get; set; }
        [FiltroEmpresa]
        public int IdEmpresa { get; set; }
        public string Descricao { get; set; }
        public string NumBanco { get; set; }       
        public string Agencia { get; set; }
        [RegistroUnico]
        public string Conta { get; set; }        
        public double Saldo { get; set; }
        public bool Ativo { get; set; }
        public bool PadraoParaVendas { get; set; }

        /*boleto*/

        public int NomeBanco { get; set; }
        public int carteira { get; set; }
        public int remessa { get; set; }
        public int retorno { get; set; }
        public string contabanco { get; set; }
        public string DigitoContabanco { get; set; }
        public string Agenciabanco { get; set; }
        public string DigitoAgenciabanco { get; set; }
        public string Convenio { get; set; }
        public string CodigoTransmissao { get; set; }
        public string DiretorioRemessa { get; set; }
        public decimal Multa { get; set; }
        public decimal Juros { get; set; }
        public decimal TarifaBoleto { get; set; }
        public int DiasTolerancia { get; set; }
        public int DiasBaixa { get; set; }
        public int TipoProtesto { get; set; }
        public int DiasParaProtesto { get; set; }
        public string MensagemCaixa { get; set; }
        [NaoMapeado]
        public int ProxNossoNumero { get; set; }
        public bool InstrucaoNaBaixa { get; set; }
        


    }
}
