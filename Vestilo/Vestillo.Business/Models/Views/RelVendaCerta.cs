
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models.Views
{

    public class RelVendaCerta
    {
        public int ClienteId { get; set; }
        public string RazaoCliente { get; set; }
        public String CnpjCpf { get; set; }
        public String Endereco { get; set; }
        public int? IdEstado { get; set; }
        public int? IdMunicipio { get; set; }
        public String Ddd { get; set; }
        public String Telefone { get; set; }
        public int ProdutoId { get; set; }
        public string RefProduto { get; set; }
        public string DescProduto { get; set; }
        public string segmento { get; set; }
        public string catalogo { get; set; }
        public string colecao { get; set; }
        public string CorProduto { get; set; }
    }
}
