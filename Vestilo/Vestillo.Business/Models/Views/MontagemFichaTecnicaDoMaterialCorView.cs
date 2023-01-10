using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
    public class MontagemFichaTecnicaDoMaterialCorView
    {
        private FichaTecnicaDoMaterialRelacao fichaTecnicaDoMaterialRelacao;
        public MontagemFichaTecnicaDoMaterialCorView(FichaTecnicaDoMaterialRelacao fichaTecnicaDoMaterialRelacao)
        {
            this.fichaTecnicaDoMaterialRelacao = fichaTecnicaDoMaterialRelacao;
        }

        public MontagemFichaTecnicaDoMaterialCorView()
        {

        }

        public FichaTecnicaDoMaterialRelacao getFichaTecnicaDoMaterialRelacao
        {
            get
            {
                return fichaTecnicaDoMaterialRelacao;
            }
        }

        public bool CorDiferente
        {
            get
            {
                if (!CorUnicaDaMateriaPrima)
                    return (CorDoProduto != CorDaMateriaPrima);
                else
                    return false;
            }
        }


        public int CorDoProduto { get; set; }

        public int CorDaMateriaPrima { get; set; }

        public bool CorUnicaDaMateriaPrima { get; set; }
    }
}
