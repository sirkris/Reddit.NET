using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class UserKarmaContainer
    {
        [JsonProperty("data")]
        public List<UserKarma> Data;
    }
}
