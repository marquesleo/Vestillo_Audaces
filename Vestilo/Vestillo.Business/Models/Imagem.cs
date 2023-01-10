using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    [Tabela("Imagens", "Imagens")]
    public class Imagem
    {

        public enum TipoDeImagem
        {
            ProdutoAcabado = 0,
            MoldeDoProduto = 1,            
        }

        [Chave]
        public int Id { get; set; }
        public int? IdProduto { get; set; }
        public int? IdMateriaPrima { get; set; }
        public int? IdColaborador { get; set; }
        public int? IdFuncionario { get; set; }
        public int? IdVendedor { get; set; }
        public TipoDeImagem tipo { get; set; }
        public byte []  imagem { get; set; }


    }
}