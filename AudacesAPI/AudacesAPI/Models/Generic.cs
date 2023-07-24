using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TemplateAudacesApi.Models
{
    public class Generic
    {
        public Generic()
        {
            type = "generic";
        }
        public string type { get; set; }
        public string uid { get; set; }
        public string name { get; set; }
        public string reference { get; set; }
        public string description { get; set; }
        public double value { get; set; }
        public string measure_unit { get; set; }
        public string last_modified { get; set; }
        public string endereco { get; set; }
        public string telefone { get; set; }
    }
}
