using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TemplateAudacesApi.Models
{
    public class Material:Interface.IProduto
    {
        public Material()
        {
            type = "raw_material";
        }

        public string nome { get; set; }
        public string type { get; set; }
        public string uid { get; set; }
        public string name { get; set; }
        public string reference { get; set; }
        public string description { get; set; }
        public string last_modified { get; set; }
        public string measure_unit { get; set; }
        public string notes { get; set; }
        public string product_group { get; set; }
        public string supplier { get; set; }
        public double value { get; set; }
        public decimal amount { get; set; }
        public decimal cost { get; set; }
        public string size { get; set; }
        public decimal total { get; set; }
        public string group { get; set; }
        public string um { get; set; }
        public string desc { get; set; }
        public object color { get; set; }
        public string variant { get; set; }
        public CustomFields custom_fields { get; set; }
        public ICollection<Image> images { get; set; }
        public List<Variant> variants { get; set; } = new List<Variant>();
        public string NomeDaCorDoProdutoAcabado { get; set; }

        public string produto
        {
            get
            {
                return uid + "-" + description + "-" + variant;
            }
        }

        public ICollection<Size> sizes { get; set; } = new List<Size>();
        public ICollection<Color> colors { get; set; } = new List<Color>();
    }
}
