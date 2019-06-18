using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class LiveThreadCreateResultData
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
