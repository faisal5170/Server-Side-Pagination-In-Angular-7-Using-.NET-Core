using Newtonsoft.Json;

namespace AngularDatatable.Models
{
    public class Search
    {
        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }

        [JsonProperty(PropertyName = "regex")]
        public bool Regex { get; set; }
    }
}
