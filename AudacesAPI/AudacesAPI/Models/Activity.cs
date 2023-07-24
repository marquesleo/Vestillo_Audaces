using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TemplateAudacesApi.Models
{
    public class Activity
    {
        public Activity()
        {
            type = "activity";
        }
        public string type { get; set; }
        public string uid { get; set; }
        public string name { get; set; }
        public string reference { get; set; }
        public string description { get; set; }
        public double value { get; set; }
        public string measure_unit { get; set; }
        public string last_modified { get; set; }
        public double time { get; set; }
        public string notes { get; set; }
        public string sector { get; set; }
        public string machine { get; set; }
        public string collection { get; set; }
        public string product_group { get; set; }
        public string currency { get; set; }
        public string supplier { get; set; }
        public ICollection<CustomFields> custom_fields { get; set; }
        public ICollection<Color> colors { get; set; }
        public ICollection<Size> sizes { get; set; }
        public ICollection<Price> prices { get; set; }
    }
}
