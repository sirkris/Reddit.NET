using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Things
{
    [Serializable]
    public class UserKarmaContainer
    {
        [JsonProperty("data")]
        public List<UserKarma> Data { get; set; }
    }
}
