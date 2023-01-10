using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TemplateAudacesApi.Models
{
    public class CustomField
    {
        public string name { get; set; }
        public string type { get; set; }
        public string value { get; set; }
        public string editable { get; set; }
        public ICollection<string> options { get; set; }
    }
}
