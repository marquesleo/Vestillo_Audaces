using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("BoletosGerados", "BoletosGerados")]
    public class BoletosGerados
    {
        [Chave]
        public int Id { get; set; }
        [FiltroEmpresa]
        public int IdEmpresa { get; set; }
        public DateTime DataEmissaoBoleto { get; set; }
        public int idBanco { get; set; }
        public int idTitulo { get; set; }
        public int idCliente { get; set; }
        public string Carteira { get; set; }
        public int Variacao { get; set; }
        public int DiasProtesto { get; set; }
        public string NossoNumero { get; set; }
        public string DvNossoNumero { get; set; }       
        public decimal Juros { get; set; }
        public int DiasBaixaDevolucao { get; set; }
        public int TipoProtesto { get; set; }
        public string NumDocumento { get; set; }
        
    }
}
