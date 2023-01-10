using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public enum TipoEndereco
    {
        Principal = 1,
        Cobranca = 2,
        Entrega = 3
    }

    [Tabela("Enderecos", "Enderecos")]
    public class Endereco
    {
        [Chave]
        public int Id { get; set; }
        public TipoEndereco TipoEndereco { get; set; }
        public string DDD { get; set; }
        public string Telefone { get; set; }
        public string Fax { get; set; }
        public int EmpresaId { get; set; }
        public string Logradouro { get; set; }
        public string CEP { get; set; }
        public int EstadoId { get; set; }
        public int MunicipioId { get; set; }
        public string Bairro { get; set; }
        public string Numero { get; set; }
    
    }
}
