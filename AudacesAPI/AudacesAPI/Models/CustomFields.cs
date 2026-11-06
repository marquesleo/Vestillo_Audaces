using System.Text.Json.Serialization;

namespace TemplateAudacesApi.Models
{
    public class CustomFields
    {
        public string name { get; set; }
        public string type { get; set; }
        public string value { get; set; }
        public string editable { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string> options { get; set; } 

        //[JsonPropertyName("ref")]
        //public Ref referencia { get; set; }
        //public Size size { get; set; }
       // public Color color { get; set; }
        
         

    }


    public class Ref
    {
        public string type { get; set; }
        public string value { get; set; }
    }
}
