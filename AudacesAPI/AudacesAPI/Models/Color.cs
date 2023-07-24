using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TemplateAudacesApi.Models
{
    public class Color
    {
        public string code { get; set; }
        public string description { get; set; }
        public string uid { get; set; }
       // public string rgb { get; set; }
        public string value { get; set; }
        public ICollection<string> Options { get; set; }  = new List<string>();

     
    }
}
