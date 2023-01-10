using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("Empresas", "Empresas")]
    public class Empresa
    {
        public enum TipoRegimeTributario
        {
            SimplesNacional = 1,
            SimplesNacionalExcedente = 2,
            Normal = 3
        }

        public enum TipoRegimeEnum
        {
            LucroReal = 1,
            LucroPresumido = 2,
            Simples = 3
        }

        public enum TipoICMSEnum
        {
            Apuracao = 1,
            Estimativa = 2
        }


        [Chave]
        public int Id { get; set; }
        [RegistroUnico]
        public string RazaoSocial { get; set; }
        public string NomeFantasia { get; set; }
        [RegistroUnico]
        public string CNPJ { get; set; }
        public TipoRegimeTributario RegimeTributario { get; set; }
        public string InscricaoEstadual { get; set; }
        public string InscricaoMunicipal { get; set; }
        public string NomeContato { get; set; }
        public string fuso { get; set; }
        public int AmbienteNfe { get; set; }
        public int TipoCertificado { get; set; }
        public decimal pis { get; set; }
        public decimal cofins { get; set; }
        public string IdToken { get; set; }
        public string Token { get; set; }
        public decimal TributosFederais { get; set; }
        public decimal tributosestaduais { get; set; }
        public decimal pispasepinter { get; set; }
        public decimal cofinsinter { get; set; }
        public string emailCopias { get; set; }
        public string CpfContador { get; set; }
        public string CnpjContador { get; set; }

        public decimal simples { get; set; }
        public decimal fgts { get; set; }
        public decimal inss { get; set; }
        public decimal irpj { get; set; }
        public decimal csll { get; set; }
        public decimal ipi { get; set; }
        public decimal iss { get; set; }
        public TipoRegimeEnum TipoRegime { get; set; }

        public TipoICMSEnum TipoICMS { get; set; }
        public decimal icms { get; set; }


        public string DDDContador { get; set; }
        public string TelefoneContador { get; set; }
        public string FaxContador { get; set; }        
        public string EnderecoContador { get; set; }
        public string CEPContador { get; set; }
        public int EstadoIdContador { get; set; }
        public int MunicipioIdContador { get; set; }
        public string BairroContador { get; set; }
        public string NumeroContador { get; set; }
        public string NomeContador { get; set; }
        public string CrcContador { get; set; }
        public string EmailContador { get; set; }
        public string CaminhoImagemProduto { get; set; }
        public DateTime DataInadimplencia {get; set;} 


        public string email { get; set; }   
        public string smtp  { get; set; }   
        public int porta    { get; set; }        
        public int habilitassl  { get; set; }       
        public int habilitacredenciais  { get; set; }
        public string senhaemail { get; set; }
        public string RedeSocial { get; set; }
        public string PoliticaTroca { get; set; }

        public string CertificadoDigital { get; set; }
        public string SerieCertificado { get; set; }


        [NaoMapeado]
        public IEnumerable<Endereco> Enderecos { get; set; }
    }
}
