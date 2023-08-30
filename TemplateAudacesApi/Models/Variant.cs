using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace TemplateAudacesApi.Models
{
    [Serializable]
    public class Variant
    {
        [JsonProperty("Variante principal")]
        public VariantePrincipal VariantePrincipal { get; set; }


        public string name { get; set; }
        public string description { get; set; }
        public decimal value { get; set; }
        public Garment garment { get; set; }
        public string author { get; set; }
        public string notes { get; set; }
        public List<Material> materials { get; set; } = new List<Material>();
        public ICollection<Measure> measures { get; set; }
        public ICollection<Activity> activity { get; set; }
        public string label { get; set; }
        public Object color { get; set; }
        public string size { get; set; }
        public string composition { get; set; }
        public List<Item> items { get; set; } = new List<Item>();
        public Item item { get; set; }
        public Material material { get; set; }
        public CustomFields custom_fields { get; set; }
        //public string NomeDaCorDoProdutoAcabado { get; set; }

        public string TamanhoVariant
        {
            get
            {
                string tamanho = string.Empty;
                string[] lines = null;
                if (name.Contains(':'))
                {
                    lines = name.Split(new[] { " - " }, StringSplitOptions.None);

                    foreach (string line in lines)
                    {
                        if (line.Contains(':'))
                        {
                            string[] elements = line.Split(new[] { ": " }, StringSplitOptions.None);
                            string formattedLine = string.Join(":",
                                elements[0],
                                elements[1].Replace("-", "- ").Replace("Tamanho", " Tamanho"));


                            if (elements[0] == "TAMANHO")
                                tamanho = elements[1];
                        }
                    }
                }else
                {
                    lines = name.Split(new[] { "-" }, StringSplitOptions.None);
                    
                    if (lines.Length == 3)
                    {
                        tamanho = lines[2];
                    }
                }

                return tamanho;
            }
        }

        public string CorDaVariant
        {
            get
            {
                string cor = string.Empty;
                string[] lines = null;
                if (name.Contains(':'))
                {
                    lines = name.Split(new[] { " - " }, StringSplitOptions.None);

                    foreach (string line in lines)
                    {
                        string[] elements = line.Split(new[] { ": " }, StringSplitOptions.None);
                        string formattedLine = string.Join(":",
                            elements[0],
                            elements[1].Replace("-", "- ").Replace("Tamanho", " Tamanho"));


                        if (elements[0] == "COR")
                            cor = elements[1];
                    }
                }
                else
                {
                    lines = name.Split(new[] { "-" }, StringSplitOptions.None);
                    if (lines.Length == 3)
                    {
                        cor = lines[0] + "-" + lines[1];
                    }
                }
                return cor;
            }
              
        }



        public Variant Clone(Variant original)
        {

            using (MemoryStream memoryStream = new MemoryStream())
            {
                string serialized = JsonConvert.SerializeObject(original);
                return JsonConvert.DeserializeObject<Variant>(serialized);
            }

        }

    }
}
