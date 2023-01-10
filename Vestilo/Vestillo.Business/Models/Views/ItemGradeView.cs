using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models.Views
{
    public class ItemGrade
    {

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            // VOU TESTAR

            if ((object)obj == null)
            {
                return false;
            }

            try
            {
                ItemGrade Item = obj as ItemGrade;
                if (Item == null)
                    return false;

                return Item.idCor == idCor && Item.idTamanho == idTamanho;
            }
            catch
            {
                return false;
            }
        }

        public int idCor { get; set; }
        public int idTamanho { get; set; }
        public decimal Qtd { get; set; }
        public decimal Preco { get; set; }
        public int? UnidadeMedida2Id { get; set; }
        public decimal? QtdUnidadeMedida2 { get; set; }
        public decimal? PrecoUnidadeMedida2 { get; set; }
        public string DescCor { get; set; }
        public string DescTamanho { get; set; }
        public string PedidoCliente { get; set; }
        public string SeqItem { get; set; }
        public decimal PerctIpi { get; set; }
        public decimal VrIpiDevolvido { get; set; }
        public DateTime DataPrevisao { get; set; }

        public override string ToString()
        {
            return "IdCor: " + idCor + "  IdTamanho: " + idTamanho;
        }
    }
}
