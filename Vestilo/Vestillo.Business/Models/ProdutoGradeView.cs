using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class ProdutoGradeView
    {
        public bool Checked { get; set; }
        public int GradeId { get; set; }
        public int ProdutoId { get; set; }
        public string ProdutoReferencia { get; set; }
        public string ProdutoDescricao { get; set; }
        public int TamanhoId { get; set; }
        public string TamanhoAbreviatura { get; set; }
        public int CorId { get; set; }
        public string CorAbreviatura { get; set; }
    }
}
