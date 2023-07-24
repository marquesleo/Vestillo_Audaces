using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TemplateAudacesApi.Models.Interface
{
    public interface IProdutoAcabado : IProduto
    {
        string filename { get; set; }
        int revision { get; set; }
        string collection { get; set; }
        string author { get; set; }
        ICollection<string> composition { get; set; }
        string currency { get; set; }
        ICollection<Color> colors { get; set; } 
        CustomFields custom_fields { get; set; }
        ICollection<Size> sizes { get; set; }
        List<Variant> variants { get; set; }
        ICollection<Price> prices { get; set; }
        ICollection<Image> images { get; set; }
        string usage { get; set; }
    }
}
