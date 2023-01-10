using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vestillo;

namespace Vestillo.Business.Models
{
    [Vestillo.Tabela("fichatecnicadomaterialrelacao", "fichatecnicadomaterialrelacao")]
    public class FichaTecnicaDoMaterialRelacao
    {
       [Vestillo.Chave]
       public int Id {get;set;}

       public int FichaTecnicaId { get; set; }
       public int MateriaPrimaId { get; set; }
       public int Tamanho_Materiaprima_Id { get; set; }
       public int cor_materiaprima_Id { get; set; }
       public int ProdutoId { get; set; }
       public int Tamanho_Produto_Id { get; set; }
       public int Cor_Produto_Id { get; set; }
       public int FichaTecnicaItemId { get; set; }
      
    }
}
