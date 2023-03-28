using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TemplateAudacesApi.Models
{
    public class CustomFields
    {
        public string name { get; set; }
        public string type { get; set; }
        public string value { get; set; }
        public string editable { get; set; }
        public List<string> options { get; set; } = new List<string>();

        [JsonPropertyName("ref")]
        public Ref referencia { get; set; }
        public Size size { get; set; }
        public Color color { get; set; }
        
         

    }


    public class Ref
    {
        public string type { get; set; }
        public string value { get; set; }
    }
}
