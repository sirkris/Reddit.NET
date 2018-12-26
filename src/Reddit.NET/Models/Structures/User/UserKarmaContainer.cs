using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Reddit.Models.Structures
{
    [Serializable]
    public class UserKarmaContainer
    {
        [JsonProperty("data")]
        public List<UserKarma> Data;
    }
}
