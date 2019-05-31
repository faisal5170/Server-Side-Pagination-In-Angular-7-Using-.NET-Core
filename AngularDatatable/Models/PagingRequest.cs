using Newtonsoft.Json;
using System.Collections.Generic;

namespace AngularDatatable.Models
{
    public class PagingRequest
    {
        [JsonProperty(PropertyName = "draw")]
        public int Draw { get; set; }

        [JsonProperty(PropertyName = "columns")]
        public IList<Column> Columns { get; set; }

        [JsonProperty(PropertyName = "order")]
        public IList<Order> Order { get; set; }

        [JsonProperty(PropertyName = "start")]
        public int Start { get; set; }

        [JsonProperty(PropertyName = "length")]
        public int Length { get; set; }

        [JsonProperty(PropertyName = "search")]
        public Search Search { get; set; }

        [JsonProperty(PropertyName = "searchCriteria")]
        public SearchCriteria SearchCriteria { get; set; }
    }
}
