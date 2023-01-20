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
        public ICollection<Image> images { get; set; }
        public ICollection<Variant> variants { get; set; } = new List<Variant>();
    }
}
