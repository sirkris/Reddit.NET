using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

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
