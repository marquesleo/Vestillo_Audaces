using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TemplateAudacesApi.Models
{
    public class Measure
    {
        public Measure()
        {
            type = "measure";
        }
        public string type { get; set; }
        public string uid { get; set; }
        public string name { get; set; }
        public string reference { get; set; }
        public string description { get; set; }
        public decimal value { get; set; }
        public string measure_unit { get; set; }
        public string last_modified { get; set; }
        public string notes { get; set; }
        public MeasureValues values { get; set; }
    }

    public class MeasureValues
    {
        public double P { get; set; }
        public double M { get; set; }
        public double G { get; set; }
        public string order { get; set; }
    }
}
