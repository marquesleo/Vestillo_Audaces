

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models.Views
{
    public class ItensOrdemFinalizacaoView
    {        
        public int IdAlmoxarifado { get; set; }
        public int IdOrdem { get; set; }
        public int IdProduto { get; set; }
        public int IdTamanho { get; set; }
        public int IdCor { get; set; }
        public int IdItemNaOp { get; set; }
        public int ItemFinalizado { get; set; }
        public string RefOrdem { get; set; }
        public string RefProduto { get; set; }
        public string DescCor { get; set; }
        public string DescTamanho { get; set; }
        public decimal Quantidade { get; set; }
        public decimal DefeitoItem { get; set; }
        public decimal Produzidas { get; set; }
        public decimal Total
        {
            get
            {
                //Ajustar
                if (DefeitoItem > 0)
                {
                    return (Quantidade - DefeitoItem);
                }
                else
                {
                    return Quantidade;
                }

            }
        }

    }
}
