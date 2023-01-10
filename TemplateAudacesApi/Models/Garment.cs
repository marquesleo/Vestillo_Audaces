using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TemplateAudacesApi.Models
{
    public class Garment :  Interface.IProdutoAcabado
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
        public ICollection<Color> colors { get; set; } = new List<Color>();
        public ICollection<Image> images { get; set; } = new List<Image>();
        public string currency { get; set; }
        public ICollection<string> composition { get; set; }
        public string responsible { get; set; }
        public ICollection<CustomField> custom_fields { get; set; } = new List<CustomField>();
        public ICollection<Size> sizes { get; set; } = new List<Size>();
        public ICollection<Price> prices { get; set; } = new List<Price>();
        public ICollection<Variant> variants { get; set; } = new List<Variant>();
        public ICollection<Item> items { get; set; } = new List<Item>();
        public string color_1 { get; set; }
        public string color_2 { get; set; }
        public string instance_uid { get; set; }
    }
}
