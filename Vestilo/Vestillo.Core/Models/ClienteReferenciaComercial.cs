using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Core.Models
{
    [Tabela("ClienteReferenciasComerciais")]
    public class ClienteReferenciaComercial : IModel
    {
        public enum enumTipo
        {
            Instituicao = 1,
            Empresa = 2,
            Banco = 3,
            Serasa = 4
        }
        
        [Chave]
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public enumTipo Tipo { get; set; }
        public DateTime? DataReferencia { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Funcionario { get; set; }
        public string Contato { get; set; }
        public string Observacao  { get; set; }
        public DateTime? DataClienteDesde  { get; set; }
        public DateTime? DataUltimaCompra { get; set; }
        public DateTime? DataMaiorCompra { get; set; }
        public decimal? ValorMaiorCompra { get; set; }
        public decimal? ValorLimiteCredito { get; set; }
        public bool PagamentoPontual { get; set; }
        public bool Carteira { get; set; }
        public string OutrasOperacoes { get; set; }
        public bool? MovimentaCC { get; set; }
        public byte[] ArquivoSerasa { get; set; }
        public string NomeArquivo { get; set; }
    }
}
