using System.Collections.Generic;

namespace TemplateAudacesApi.Models
{
    public class VariantePrincipal
    {
        public string description { get; set; }
        public string notes { get; set; }
        public int value { get; set; }
        public List<Material> materials { get; set; } = new List<Material>();
    }
}
