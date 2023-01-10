using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vestillo.Business.Models
{
     [Vestillo.Tabela("fichatecnicadomaterial")]
    public class FichaTecnicaDoMaterialView:FichaTecnicaDoMaterial
    {

        [Vestillo.NaoMapeado]
        public string Colecao { get; set; }

        [Vestillo.NaoMapeado]
        public string GrupoProdutoDescricao { get; set; }

        [Vestillo.NaoMapeado]
        public string Segmento { get; set; }

        [Vestillo.NaoMapeado]
        public string ProdutoDescricao { get; set; }

        [Vestillo.NaoMapeado]
        public string ProdutoReferencia { get; set; }

        [Vestillo.NaoMapeado]
        public string NomeUsuario { get; set; }

        [Vestillo.NaoMapeado]
        public string AtivoDescricao {
            get {
                if (this.Ativo)
                    return "Sim";
                else
                    return "Não";
            }
        }

    }
}
