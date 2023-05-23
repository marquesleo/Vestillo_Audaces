
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("ContadorRemessa", "ContadorRemessa")]
    public class ContadorRemessa
    {
        [Chave]
        public int Id { get; set; }
        [RegistroUnico]
        public int IdBanco { get; set; }
        [RegistroUnico]
        public int NumeracaoAtual { get; set; }
        public string UltimoArquivoGerado { get; set; }
        public string Prefixo { get; set; }
    }
}