using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TemplateAudacesApi.Models
{
    public class StatusRetorno
    {
        public string uid { get; set; }
        public string status { get; set; }
        public string message { get; set; }
        public string error { get; set; }
    }
}
