using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class StatusResult
    {
        [JsonProperty("status")]
        public bool Status;
    }
}
