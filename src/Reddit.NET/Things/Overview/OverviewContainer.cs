using Newtonsoft.Json;
using System;

namespace Reddit.Things

{
    [Serializable]
    public class OverviewContainer : BaseContainer
    {
        [JsonProperty("data")]
        public OverviewData Data { get; set; }
    }
}
