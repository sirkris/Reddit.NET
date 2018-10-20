using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
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
