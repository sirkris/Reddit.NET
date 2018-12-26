using Newtonsoft.Json;
using System;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class StatusResult
    {
        [JsonProperty("status")]
        public bool Status;
    }
}
