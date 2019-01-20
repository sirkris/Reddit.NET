using Newtonsoft.Json;
using System;

namespace Reddit.Things
{
    [Serializable]
    public class Snoomoji
    {
        [JsonProperty("url")]
        public string URL;

        [JsonProperty("created_by")]
        public string CreatedBy;
    }
}
