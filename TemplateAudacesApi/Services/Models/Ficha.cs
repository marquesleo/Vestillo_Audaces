using System.Collections.Generic;

namespace TemplateAudacesApi.Services.Models
{
    public class Ficha
    {
        public int idFicha { get; set; }
        public string tamanhoProduto { get; set; }
        public string corProduto { get; set; }
        public string uidMateriaPrima { get; set; }
        public List<ItemPraGravar> Itens { get; set; }
        public decimal quantidade { get; set; }
        public decimal valor { get; set; }
        public short sequencia { get; set; }
    }

}
