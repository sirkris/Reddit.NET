using Newtonsoft.Json;
using Reddit.Models.Converters;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class OverviewData : BaseData
    {
        [JsonProperty("children")]
        [JsonConverter(typeof(UserOverviewConverter))]
        public List<CommentOrPost> Children { get; set; }
    }
}
