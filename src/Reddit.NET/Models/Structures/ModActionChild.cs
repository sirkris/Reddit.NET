using Newtonsoft.Json;
using System;

namespace Reddit.NET.Models.Structures
{
    [Serializable]
    public class ModActionChild : BaseContainer
    {
        [JsonProperty("data")]
        public ModAction Data;
    }
}
