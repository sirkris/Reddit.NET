using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class PostResult
    {
        [JsonProperty("errors")]
        public List<List<string>> Errors;

        [JsonProperty("data")]
        public PostResultData Data;
    }
}
