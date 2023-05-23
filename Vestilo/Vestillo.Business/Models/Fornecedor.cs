using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("Fornecedores")]
    public class Fornecedor
    {
        [Chave]
        public int Id { get; set; }
        public int IdEmpresa { get; set; }
        public int IdCondPagamento { get; set; }
        public String Referencia { get; set; }
        public String Nome { get; set; }
        public String RazaoSocial { get; set; }
        public String CnpjCpf { get; set; }
        public String Endereco { get; set; }
        public String Complemento { get; set; }
        public int IdMunicipio { get; set; }
        public String Cep { get; set; }
        public String CaixaPostal { get; set; }
        public int Pessoa { get; set; }
        public String RegistroGeral { get; set; }
        public String Ddd { get; set; }
        public String Telefone { get; set; }
        public String Fax { get; set; }
        public String InscEstadual { get; set; }
        public String InscMunicipal { get; set; }
        public String Email { get; set; }
        public String HomePage { get; set; }
        public String Contato { get; set; }
        public String PriCompra { get; set; }
        public String UltCompra { get; set; }
        public String Observacao { get; set; }
        public double ValorMinimo { get; set; }
        public String Celular { get; set; }
        public String Rntc { get; set; }
        public String Numero { get; set; }
        public String Bairro { get; set; }
        public int IdEstado { get; set; }


                                                                                      
   


    }
}
