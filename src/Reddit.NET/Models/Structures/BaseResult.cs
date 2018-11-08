using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public abstract class BaseResult
    {
        [JsonProperty("errors")]
        public List<List<string>> Errors;
    }
}
