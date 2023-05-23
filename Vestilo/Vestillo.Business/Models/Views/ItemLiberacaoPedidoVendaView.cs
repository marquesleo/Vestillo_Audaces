using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Lib;

namespace Vestillo.Business.Models
{
    public class ItemLiberacaoPedidoVendaView
    {
        private int _sequencia = 1;
        public int Id { get; set; }
        public int iditem { get; set; }
        public int IdTipoMov { get; set; }
        public int? idcor { get; set; }
        public int? idtamanho { get; set; }
        public string RefProduto { get; set; }
        public string DescProduto { get; set; }
        public string DescCor { get; set; }
        public string DescTamanho { get; set; }
        public decimal Preco { get; set; }
        public int ItemPedidoVendaId { get; set; }
        public int AlmoxarifadoId { get; set; }
        public decimal QtdLiberada { get; set; }
        public decimal QtdFaturada { get; set; }
        public decimal QtdEstoque { get; set; }
        public decimal QtdEmpenhada { get; set; }
        public decimal QtdConferencia { get; set; }
        public decimal QtdConferida { get; set; }
        public decimal Qtd { get; set; }
        public DateTime DataLiberacao { get; set; }
        public int PedidoVendaId { get; set; }
        public string ReferenciaPedidoVenda { get; set; }
        public int SeqLibPedVenda { get; set; }
        public string ReferenciaPedidoCliente { get; set; }
        public string SeqPedCliente { get; set; }
        public int Status { get; set; }
        public int SemEmpenho { get; set; }
        public bool Conferencia
        {
            get
            {
                if (QtdConferencia > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public string Sequencia
        {
            get
            {
                return _sequencia.ToString("000");
            }
            set
            {
                _sequencia = value.ToInt();
            }
        }

        public string DescRefProduto
        {
            get
            {
                return string.Concat(RefProduto, " - ", DescProduto);
            }
        }

        public decimal Conferido { get; set; }
        public string CodigoBarras { get; set; }
        public decimal Diferenca { get; set; }
        public decimal DiferencaOld { get; set; }
    }
}
