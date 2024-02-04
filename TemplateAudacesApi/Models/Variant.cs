using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TemplateAudacesApi.Models
{
    [Serializable]
    public class Variant
    {
        public Variant()
        {
            CarregarCamposCustomizaveis();
        }

        public string name { get; set; }
        public string description { get; set; }
        public decimal value { get; set; }
        public Garment garment { get; set; }
        public string notes { get; set; }
        //[JsonPropertyName("Observação")]
        //public string observacao { get; set; } 
        public List<Material> materials { get; set; } = new List<Material>();
        public ICollection<Measure> measures { get; set; }
        public ICollection<Activity> activity { get; set; }
        public string label { get; set; }
        [JsonPropertyName("Cor")]
        public Object Cor { get; set; }


        [JsonPropertyName("Destinos")]
        public Object Destinos { get; set; }

        public Object Color { get; set; }

        public string size { get; set; }
        public string composition { get; set; }
       public List<Item> items { get; set; } = new List<Item>();
        public Item item { get; set; }
        //public Material material { get; set; }
        public object custom_fields { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public string Ano { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public string Colecao { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public string MinhaCor { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public string Grupo { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public string Referencia { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public string Segmento { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public string MeuTamanho { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public string Destino { get; set; }

        public void CarregarCamposCustomizaveis()
        {
            if (custom_fields != null)
            {
                var temp = System.Text.Json.JsonSerializer.Serialize(custom_fields);
                var doc = JsonDocument.Parse(temp);
                foreach (var itemObject in doc.RootElement.EnumerateObject())
                {
                    try
                    {
                        string customName = itemObject.Name;
                        JsonElement valueObject = itemObject.Value.GetProperty("value");
                        switch (customName.ToUpper())
                        {
                            case "ANO":
                                Ano = valueObject.GetString();
                                break;

                            case "COLECAO":
                                Colecao = valueObject.GetString();
                                break;
                            case "COR":
                                MinhaCor = valueObject.GetString();
                                break;
                            case "GRUPO":
                                Grupo = valueObject.GetString();
                                break;
                            case "REFERENCIA":
                                Referencia = valueObject.GetString();
                                break;
                            case "SEGMENTO":
                                Segmento = valueObject.GetString();
                                break;
                            case "TAMANHO":
                                MeuTamanho = valueObject.GetString();
                                break;
                            case "SIZE":
                                MeuTamanho = valueObject.GetString();
                                break;
                            case "DESTINOS":
                                Destino = valueObject.GetString();
                                break;

                        };


                    }
                    catch (Exception ex)
                    {

                        throw;
                    }
                }
            }


        }

        public string RetornarTamanhoVariant()
        {

            string tamanho = this.MeuTamanho;
         
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


                        if (elements[0].ToUpper() == "TAMANHO")
                        {
                            tamanho = elements[1];
                            if (string.IsNullOrEmpty(tamanho))
                            {
                                tamanho = this.MeuTamanho;
                            }
                        }
                    }
                }
            }
            
            else
            {
                lines = name.Split(new[] { "-" }, StringSplitOptions.None);

                if (lines.Length == 3)
                {
                    tamanho = lines[2];
                }
            }

            return tamanho;

        }

        public string RetornarCorDaVariant()
        {
            
            string cor = this.MinhaCor;
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


                    if (elements[0].ToUpper() == "COR")//codigo junto com descricao
                        cor = elements[1];
                }
            }
            else if (name.Contains('-'))
            {
                lines = name.Split(new[] { "-" }, StringSplitOptions.None);

                if (lines.Length == 2 || lines.Length == 3)
                { //codigo - descricao
                    cor = lines[0] + "-" + lines[1];
                }

            }
          

            return cor;


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
