using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class JQueryReturn
    {
        [JsonProperty("jquery")]
        public dynamic JQuery;

        [JsonProperty("success")]
        public bool Success;
    }
}
