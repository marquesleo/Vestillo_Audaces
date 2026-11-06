using System.Text.Json;

namespace TemplateAudacesApi.Models
{
    public class GarmentRequest
    {
        public string instance_uid { get; set; }
        public string filename { get; set; }
        public int revision { get; set; }
        public string author { get; set; }
        public string sender { get; set; }
        public string last_modified { get; set; }
        public string name { get; set; }
        public string collection { get; set; }
        public string responsible { get; set; }
        public double value { get; set; }
        public List<VariantRequest> variants { get; set; } = new List<VariantRequest>();
        public List<VariationRequest> variations { get; set; } = new List<VariationRequest>();
    }

    public class VariantRequest
    {
        public string name { get; set; }
        public string description { get; set; }
        public string notes { get; set; }
        public double value { get; set; }
        public List<MaterialRequest> materials { get; set; } = new List<MaterialRequest>();
        public Dictionary<string, CustomFieldValue> custom_fields { get; set; } = new Dictionary<string, CustomFieldValue>();
    }

    public class MaterialRequest
    {
        public string uid { get; set; }
        public string instance_uid { get; set; }
        public double amount { get; set; }
        public string variant { get; set; }
        public ColorRequest color { get; set; }
        public double cost { get; set; }
        public string desc { get; set; }
        public string group { get; set; }
        public string obs { get; set; }
        public string supplier { get; set; }
        public double total { get; set; }
        public string um { get; set; }
        public Dictionary<string, CustomFieldValue> custom_fields { get; set; } = new Dictionary<string, CustomFieldValue>();
    }

    public class ColorRequest
    {
        public string value { get; set; }
        public string rgb { get; set; }
    }

    public class CustomFieldValue
    {
        public string type { get; set; }
        public JsonElement value { get; set; }
    }

    public class VariationRequest
    {
        public string name { get; set; }
        public string category { get; set; }
        public List<VariationRef> refs { get; set; } = new List<VariationRef>();
    }

    public class VariationRef
    {
        public string variant { get; set; }
        public string @ref { get; set; }
        public string instance_uid { get; set; }
        public string ref_variant { get; set; }
    }
}