using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo.Business.Repositories;
using Vestillo.Business;

namespace Vestillo.Business.Models.Views
{
    public class PedidoCompraView : PedidoCompra
    {
        public string RefFornecedor { get; set; }
        public string NomeFornecedor { get; set; }
        public string DataFornecedor { get; set; }        
        public string RefVendedor { get; set; }
        public string NomeVendedor { get; set; }
        public string DescricaoStatus
        {
            get
            {
                switch (this.Status)
                {
                    case (int)enumStatusPedidoCompra.Finalizado:
                        return "Finalizado";
                    case (int)enumStatusPedidoCompra.Faturado_Parcial:
                        return "Atendido Parcial";
                    case (int)enumStatusPedidoCompra.Faturado_Total:
                        return "Atendido";
                    case (int) enumStatusPedidoCompra.Incluido:
                        return "Aberto";
                    default:
                        return "";
                }
            }
        }
        public string RefTabelaPreco { get; set; }
        public string DescricaoTabelaPreco { get; set; }
        public string RefTransportadora { get; set; }
        public string NomeTransportadora { get; set; }
        public string RefRotaVisita { get; set; }
        public string DescricaoRotaVisita { get; set; }
        public decimal ValorFaltante { get; set;}
        public int Prazo { get; set; }
    }
}
