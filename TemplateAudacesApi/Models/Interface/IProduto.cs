using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TemplateAudacesApi.Models.Interface
{
    public interface IProduto
    {
        string type { get; set; }
        string uid { get; set; }
        string name { get; set; }
        string reference { get; set; }
        string description { get; set; }
        string last_modified { get; set; }
        string product_group { get; set; }
        string supplier { get; set; }
        string measure_unit { get; set; }
        string notes { get; set; }

    }
}
