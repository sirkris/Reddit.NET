using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class LiveThreadCreateResultData
    {
        [JsonProperty("id")]
        public string Id;
    }
}
