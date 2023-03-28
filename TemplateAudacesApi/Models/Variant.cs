using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TemplateAudacesApi.Models
{

    public class Variant
    {
        [JsonProperty("Variante principal")]
        public VariantePrincipal VariantePrincipal { get; set; }


        public string name { get; set; }
        public string description { get; set; }
        public decimal value { get; set; }
        public Garment garment { get; set; }
        public string author { get; set; }
        public string notes { get; set; }
        public List<Material> materials { get; set; } = new List<Material>();
        public ICollection<Measure> measures { get; set; }
        public ICollection<Activity> activity { get; set; }
        public string label { get; set; }
        public Object color { get; set; }
        public string size { get; set; }
        public string composition { get; set; }
        public List<Item> items { get; set; } = new List<Item>();
        public Item item { get; set; }
        public Material material { get; set; }
        public CustomFields custom_fields { get; set; }
    }
}
