using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class PacoteFaccao
    {
        public string Pacote { get; set; }
        public DateTime DataEmissao { get; set; }
        public string ProdutoReferencia { get; set; }
        public string NomeFaccao { get; set; }
        public string Operacao { get; set; }
        public DateTime? DataEntrada { get; set; }
        public DateTime? DataSaida { get; set; }

        public string OrdemProducao { get; set; }
        public string Cor { get; set; }
        public string Tamanho { get; set; }
        public Decimal Pecas { get; set; }
        public Decimal Defeito { get; set; }
        public Decimal TotalPecas { get; set; }
        public Decimal TempoPecas { get; set; }
        public Decimal TempoDefeito { get; set; }
        public Decimal TempoTotal { get; set; }
        public Decimal ValorTotal { get; set; }
        public Decimal PrecoUnitario { get; set; }
        public string Preco
        {
            get
            {
                return "R$ " + Math.Round(PrecoUnitario, VestilloSession.QtdCasasPreco);
            }
        }
        public string Valor
        {
            get
            {
                return "R$ "+ Math.Round(ValorTotal, VestilloSession.QtdCasasPreco);
            }
        }


    }
}
