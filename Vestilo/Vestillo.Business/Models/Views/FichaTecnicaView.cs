using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class FichaTecnicaRelatorio
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public int TamanhoId { get; set; }
        public int CorId { get; set; }
        public int FichaId { get; set; }
        public int FichaMaterialId { get; set; }
        public int QtdOperacoes { get; set; }

        public string DescTamanho { get; set; }
        public string DescCor { get; set; }
        public string AbvCor { get; set; }
        public string AbvTamanho { get; set; }
        public string RefProduto { get; set; }
        public string DescProduto { get; set; }
        public string ObsFicha { get; set; }
        public string Quebra { get; set; }
        public string QuebraMaterial { get; set; }

        public decimal QtdPacote { get; set; }
        public decimal TempoPacote { get; set; }
        public decimal TempoTotal { get; set; }
        public decimal CustoMaterial { get; set; }

        public decimal TempoPadrao
        {
            get
            {
                if (QtdPacote == 0)
                    QtdPacote = 1;
                return TempoTotal - ((TempoPacote / QtdPacote) * QtdOperacoes);
            }
        }

        public byte[] imagem { get; set; }
    }
}
