namespace TemplateAudacesApi.Services.Models
{
    public class ItemPraGravar
    {
        public int item { get; set; }
        public string corProduto { get; set; }
        public string tamanhoProduto { get; set; }
        public decimal quantidade { get; set; }
        public decimal valor { get; set; }
        public string uidMateriaPrima { get; set; }
        public string Destino { get; set; }
        public string materiaprimaTamanhoDoProduto => uidMateriaPrima + "|" + tamanhoProduto;
        public string descCorMateriaPrima { get; set; }
        public string descTamanhoMateriaPrima { get; set; }

        public string descricaoToda
        {
            get
            {

                return uidMateriaPrima + "|" + descCorMateriaPrima + "|"+ descTamanhoMateriaPrima;
            }
        }


    }
}
