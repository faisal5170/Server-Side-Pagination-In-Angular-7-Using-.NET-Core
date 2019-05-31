using Newtonsoft.Json;

namespace AngularDatatable.Models
{
    public class SearchCriteria
    {
        [JsonProperty(PropertyName = "filter")]
        public string Filter { get; set; }

        [JsonProperty(PropertyName = "isPageLoad")]
        public bool IsPageLoad { get; set; }
    }
}
