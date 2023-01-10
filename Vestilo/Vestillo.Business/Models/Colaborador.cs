using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("Colaboradores","Colaborador")]
    public class Colaborador
    {

        public enum enumTipoCliente
        {
            Cliente = 0,
            Lead = 1,
            Funcionario = 2
        }

        [Chave]
        public int Id { get; set; }
        [Contador("Colaborador")]
        [RegistroUnico]
        public String Referencia { get; set; }

        [NaoMapeado]
        public string DescTipoCLiente
        {
            get
            {
                return TipoCliente.ToString();
            }
        }

        [FiltroEmpresa]
        public int IdEmpresa { get; set; }
        public bool Cliente { get; set; }
        public bool Faccao { get; set; }
        public bool Vendedor { get; set; }
        public bool Fornecedor { get; set; }
        public bool OrgaoPublico { get; set; }
        public bool Transportadora { get; set; }
        public bool Guia { get; set; }
        public bool DevolucaoNfce { get; set; }        
        public String Nome { get; set; }       
        public String RazaoSocial { get; set; }        
        public String CnpjCpf { get; set; }
        public String Pix { get; set; }
        public String Endereco { get; set; }
        public String Complemento { get; set; }
        public int IdPais { get; set; }
        public int? IdEstado { get; set; }
        public int? IdMunicipio { get; set; }
        public String Cep { get; set; }
        public String CaixaPostal { get; set; }
        public int Pessoa { get; set; }
        public int TipoPix { get; set; }
        public String RegistroGeral { get; set; }
        public String OrgaoExpelidor { get; set; }
        public String Ddd { get; set; }
        public String Telefone { get; set; }
        public String Fax { get; set; }
        public String InscEstadual { get; set; }
        public String InscMunicipal { get; set; }
        public String Email { get; set; }
        public String HomePage { get; set; }
        public String Celular { get; set; }
        public String Numero { get; set; }
        public String Bairro { get; set; }       
        public bool Ativo { get; set; }
        public int Suframa { get; set; }
        public String ExtrangeiroIdent { get; set; }
        public decimal LimiteCredito { get; set; }
        public decimal LimiteCompra { get; set; }
        public decimal DebitoAntigo { get; set; }
        public String ContatoCobranca { get; set; }
        public String TelefoneCobranca { get; set; }
        public String EmailCobranca { get; set; }
        public int VinculaPortal { get; set; }

        public decimal Bonificado { get; set; }
        public decimal Pis { get; set; }
        public decimal Cofins { get; set; }
        public decimal Sefaz { get; set; }

        [NaoMapeado]
        public String NomeVendedor { get; set; }

         [NaoMapeado]
        public String NomeVendedor2 { get; set; }       
        
        //Colaborador Fornecedor
        public int? IdCondPagamento { get; set; }
        public int? idTabelaPreco { get; set; } 
        public String Contato { get; set; }
        public DateTime? PriCompra { get; set; }
        public DateTime? UltCompra { get; set; }
        public String Observacao { get; set; }
        public decimal ValorMinimo { get; set; }

        //Colaborador Transportadora
        public String Rntc { get; set; }
        public String ViaTransporte { get; set; }

        //Colaborador Facçao
        public String Banco { get; set; }
        public String Agencia { get; set; }
        public String Conta { get; set; }
        public int Funcionarios { get; set; }
        public DateTime? HorasDia { get; set; }
        public decimal MinutosDia { get; set; }
        public decimal ValorMinuto { get; set; }

        //Vendedor
        public int TipoVendedor { get; set; }
        public decimal PercentComissao { get; set; }
        public bool ObrigaComissao { get; set; }
        public bool EnviarDanfe { get; set; }
        public String ObsVendedor { get; set; }
        public int TipoComissao { get; set; }
        
        //Cliente
        public int ConsFinal { get; set; }
        public int ContIcms { get; set; }
        public int PartIcms { get; set; }
        public int Pfcp { get; set; }
        public DateTime? DataCadastro { get; set; }       
        public DateTime? DataNascimento { get; set; }
        public int Sexo { get; set; }
        public int EstadoCivil  { get; set; }
        public int? IdEstadoRg { get; set; }
        public DateTime? DataEmissaoRg { get; set; }
        public String Pai { get; set; }
        public String Mae { get; set; }
        public String Conjuge { get; set; }
        public String Profissao { get; set; }
        public String ContatoCliente { get; set; }
        public int RiscoCliente { get; set; }
        public int? IdVendedor { get; set; }
        public int? IdVendedor2 { get; set; }
        public int? IdRota { get; set; }
        public int? IdTransportadora { get; set; }
        public int? IdCondPagCliente { get; set; }
        public int? IdTipoPagCliente { get; set; }
        public String EntCep { get; set; }
        public String EntEndereco { get; set; }
        public String EntNumero { get; set; }
        public String EntBairro { get; set; }
        public String EntComplemento { get; set; }
        public int? IdEntEstado { get; set; }
        public int? IdEntMunicipio { get; set; }
        public String CobCep { get; set; }
        public String CobEndereco { get; set; }
        public String CobNumero { get; set; }
        public String CobBairro { get; set; }
        public String CobComplemento { get; set; }
        public int? IdCobEstado { get; set; }
        public int? IdCobMunicipio { get; set; }
        public string obscliente { get; set; }
        public int? UsuarioId { get; set; }
        public enumTipoCliente TipoCliente { get; set; }
        public DateTime? DataLead { get; set; }
        public DateTime? DataPrimeiraCompra { get; set; }
        public int DiaVencimento { get; set; }
        public decimal ValorMensalidade { get; set; }

        //Funcionario
        public int? IdFuncionario { get; set; }

        //Guia
        public decimal ComissaoGuia { get; set; }
        public String ObsGuia { get; set; }
        public int TipoComissaoGuia { get; set; }

        [NaoMapeado]
        public IEnumerable<MaquinaColaborador> Grade { get; set; }

        [NaoMapeado]
        public byte[] imgFaccao { get; set; }

        [NaoMapeado]
        public byte[] ImagemVendedor { get; set; }

        [Vestillo.NaoMapeado]
        public List<ParcelaPadraoCliente> Parcelas { get; set; }
    }
}
