using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class ActionResult : BaseResult
    {
        [JsonProperty("status")]
        public string Status;

        [JsonProperty("ok")]
        public bool Ok;

        [JsonProperty("warnings")]
        public object Warnings;
    }
}
