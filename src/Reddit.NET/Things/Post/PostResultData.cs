using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class PostResultData
    {
        [JsonProperty("things")]
        public List<PostChild> Things { get; set; }
    }
}
