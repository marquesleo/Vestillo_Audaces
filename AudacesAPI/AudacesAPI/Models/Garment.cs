using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace TemplateAudacesApi.Models
{
    public class Garment 
    {
        public Garment()
        {
            type = "finished_product";
        }

        public string type { get; set; }
        public string uid { get; set; }
        public string name { get; set; }
        public string reference { get; set; }
        public string description { get; set; }
        public double value { get; set; }
        public string measure_unit { get; set; }
        public string last_modified { get; set; }
        public string filename { get; set; }
        public int revision { get; set; }
        public string author { get; set; }
        public string collection { get; set; }
        public string notes { get; set; }
        public string product_group { get; set; }
        public string supplier { get; set; }
        public string usage { get; set; }
       
        public ICollection<Image> images { get; set; } = new List<Image>();
        public string currency { get; set; }
        public ICollection<string> composition { get; set; }
        public string responsible { get; set; }
        
        [JsonPropertyName("tamanho")]
        public CustomFields Tamanho { get; set; } = new CustomFields();
        
        [JsonPropertyName("Catálogo")]
        public CustomFields Catalogo { get; set; } = new CustomFields();
        
        [JsonPropertyName("Cor")]
        public CustomFields Cor { get; set; } = new CustomFields();
       
        [JsonPropertyName("Entrega")]
        public CustomFields Entrega { get; set; } = new CustomFields();
        
        [JsonPropertyName("Almoxarifado")]
        public CustomFields Almoxarifado { get; set; } = new CustomFields();
        
        [JsonPropertyName("Subgrupo")]
        public CustomFields Subgrupo { get; set; } = new CustomFields();
        
        [JsonPropertyName("Família")]
        public CustomFields Familia { get; set; } = new CustomFields();
       
        [JsonPropertyName("Segmento")]
        public CustomFields Segmento { get; set; } = new CustomFields();
        
        [JsonPropertyName("Ano")]
        public CustomFields Ano { get; set; } = new CustomFields();
       
        [JsonPropertyName("Categoria")]
        public CustomFields Categoria { get; set; } = new CustomFields();
      
        [JsonPropertyName("SubCategoria")]
        public CustomFields SubCategoria { get; set; } = new CustomFields();
        
        [JsonPropertyName("Griffe")]
        public CustomFields Griffe { get; set; } = new CustomFields();
      
        [JsonPropertyName("Origem")]
        public CustomFields Origem { get; set; } = new CustomFields();
        
        [JsonPropertyName("Estilista")]
        public CustomFields Estilista { get; set; } = new CustomFields();
        
        [JsonPropertyName("Modelista")]
        public CustomFields Modelista { get; set; } = new CustomFields();
        
        [JsonPropertyName("NCM")]
        public CustomFields NCM { get; set; } = new CustomFields();
        
        [JsonPropertyName("Descrição somente para FE-e")]
        public CustomFields DescFe { get; set; } = new CustomFields();
        
        [JsonPropertyName("Referência do cliente")]
        public CustomFields RefCli { get; set; } = new CustomFields();
    
        public ICollection<Price> prices { get; set; } = new List<Price>();
        public List<Variant> variants { get; set; } = new List<Variant>();
      
        public ICollection<Item> items { get; set; } = new List<Item>();
       
    }
}
