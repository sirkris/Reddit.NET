using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class PostResultData
    {
        [JsonProperty("things")]
        public List<PostChild> Things;
    }
}
