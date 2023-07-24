using System.Collections.Generic;

namespace TemplateAudacesApi.Models
{
    public class Variation
    {
        public string name { get; set; }
        public string category { get; set; }
        public List<Ref> refs { get; set; }
    }
}
