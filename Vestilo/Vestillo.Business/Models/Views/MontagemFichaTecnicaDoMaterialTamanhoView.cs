using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class MontagemFichaTecnicaDoMaterialTamanhoView
    {

        public MontagemFichaTecnicaDoMaterialTamanhoView()
        {

        }
        private FichaTecnicaDoMaterialRelacao fichaTecnicaDoMaterialRelacao;
        public MontagemFichaTecnicaDoMaterialTamanhoView(FichaTecnicaDoMaterialRelacao fichaTecnicaDoMaterialRelacao)
        {
            this.fichaTecnicaDoMaterialRelacao = fichaTecnicaDoMaterialRelacao;
        }

        public bool TamanhoDiferente
        {
            get
            {
                if (!TamanhoUnicoDaMateriaPrima)
                {
                    return (TamanhoDoProduto != TamanhoDaMateriaPrima);
                }
                else
                {
                    return false;
                }
            }
        }


        public int TamanhoDoProduto { get; set; }

        public int TamanhoDaMateriaPrima { get; set; }

        public bool TamanhoUnicoDaMateriaPrima { get; set; }
    }
}
