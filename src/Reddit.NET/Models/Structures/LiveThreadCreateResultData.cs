using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class LiveThreadCreateResultData
    {
        [JsonProperty("id")]
        public string Id;
    }
}
