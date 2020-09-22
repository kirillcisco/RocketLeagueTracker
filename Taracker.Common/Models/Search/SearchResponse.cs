using Newtonsoft.Json;
using System.Collections.Generic;

namespace Common.Models.Search
{
    public partial class SearchResponse
    {
        [JsonProperty("data")]
        public List<SearchData> Data { get; set; }
    }
}
