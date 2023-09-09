﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TemplateAudacesApi.Models
{
    public class Item : Interface.IProduto
    {
        public string type { get; set; }
        public string uid { get; set; }
        public string name { get; set; }
        public string reference { get; set; }
        public string description { get; set; }
        public double value { get; set; }
        public decimal amount { get; set; }
        public double cost { get; set; }
        public string measure_unit { get; set; }
        public string last_modified { get; set; }
        public string collection { get; set; }
        public string product_group { get; set; }
        public string currency { get; set; }
        public string supplier { get; set; }
        public string notes { get; set; }
        public double time { get; set; }
        public string sector { get; set; }
        public string machine { get; set; }
        public List<Variant> variants { get; set; } = new List<Variant>();
        public ICollection<CustomFields> custom_fields { get; set; } = new List<CustomFields>();
        public ICollection<Color> colors { get; set; } = new List<Color>();
        public ICollection<Size> sizes { get; set; } = new List<Size>();
        public ICollection<Price> prices { get; set; } = new List<Price>();
        public ICollection<Image> images { get; set; } = new List<Image>();
        public string gender { get; set; }
        public string grid_size { get; set; }
        public string griffe { get; set; }
        public string sub_group { get; set; }
        public string designer { get; set; }
        public string date_register { get; set; }
        public string usage { get; set; }
        //public string composicao { get; set; }
        //public string Tamanho { get; set; }
    }
}
