
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("AdmCartao", "Adm Cartão")]
    public class AdmCartao
    {
        [Chave]
        public int Id { get; set; }
        [Contador("AdmCartao")]
        [RegistroUnico]
        public string referencia { get; set; }
        public string nome { get; set; }
        public string razaosocial { get; set; }
        public decimal taxacobcredito { get; set; }
        public decimal taxacobdebito { get; set; }
        public int prazopagcredito { get; set; }
        public int prazopagdebito { get; set; }
        public string endereco { get; set; }
        public string complemento { get; set; }
        public string bairro { get; set; }
        public int? Idestado { get; set; }
        public int? idmunicipio { get; set; }
        public int idpais { get; set; }
        public string cnpjcpf { get; set; }
        public string inscestadual { get; set; }        
        public string cep { get; set; }
        public string ddd { get; set; }
        public string telefone { get; set;}   
        public string numero { get; set; }
        public bool Ativo { get; set; }
    }
}
