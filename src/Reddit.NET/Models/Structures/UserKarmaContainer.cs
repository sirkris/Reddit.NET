using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class UserKarmaContainer
    {
        [JsonProperty("data")]
        public List<UserKarma> Data;
    }
}
