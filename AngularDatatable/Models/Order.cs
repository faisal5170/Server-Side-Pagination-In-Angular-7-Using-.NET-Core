using Newtonsoft.Json;

namespace AngularDatatable.Models
{
    public class Order
    {
        [JsonProperty(PropertyName = "column")]
        public int Column { get; set; }

        [JsonProperty(PropertyName = "dir")]
        public string Dir { get; set; }
    }
}
